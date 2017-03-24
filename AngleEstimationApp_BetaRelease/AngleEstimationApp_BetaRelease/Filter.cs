using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleEstimationApp_BetaRelease
{
    abstract class Filter
    {
        protected int obsMethod;

        protected double dt = 1.0 / 100.0;                                   // sampling period
        protected double gyroOffRoll = -3.6982;
        protected double gyroOffPitch = -3.3570;
        protected double gyroOffYaw = -2.5909;

        protected double magnOffX=0.40;
        protected double magnOffY=0.07;
        protected double magnOffZ=-0.11;
        protected double magnSFX=1.1;
        protected double magnSFY=1;
        protected double magnSFZ=1.04;

        public void loadParameters() {
            ParametersHandler paramHandler = ParametersHandler.getIntance();

            paramHandler.loadXMLParameters();
            paramHandler.loadMagnetometerParams();
            paramHandler.loadGyroscopeParams();

            double[] param = paramHandler.getMagnetometerOffsets();
            this.magnOffX = param[0];
            this.magnOffY = param[1];
            this.magnOffZ = param[2];
            param = paramHandler.getMagnetometerScaleFactors();
            this.magnSFX = param[0];
            this.magnSFY = param[1];
            this.magnSFZ = param[2];
            param = paramHandler.getGyroOffsets();
            this.gyroOffRoll = param[0];
            this.gyroOffPitch = param[1];
            this.gyroOffYaw = param[2];
        }
        
        protected MatrixLibrary.Matrix GradientDescent(double a_x, double a_y, double a_z, double m_x, double m_y, double m_z, double mu,MatrixLibrary.Matrix qObserv)
        {
            int i = 0;
            double q1, q2, q3, q4;
            MatrixLibrary.Matrix f_obb = new MatrixLibrary.Matrix(6, 1);
            MatrixLibrary.Matrix Jacobian = new MatrixLibrary.Matrix(6, 4);
            MatrixLibrary.Matrix Df = new MatrixLibrary.Matrix(4, 1);
            double norm;
            double bx, bz, by;
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);

            q1 = qObserv[0, 0];
            q2 = qObserv[1, 0];
            q3 = qObserv[2, 0];
            q4 = qObserv[3, 0];

            //Magnetometer Calibration values
            m_x = m_x *magnSFX+magnOffX;
            m_y = m_y*magnSFY+magnOffY;
            m_z=m_z*magnSFZ+magnOffZ;

            norm = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);
            m_x /= norm;
            m_y /= norm;
            m_z /= norm;

            while (i < 10)
            {
                //compute the direction of the magnetic field
                MatrixLibrary.Matrix quaternion = new MyQuaternion(q1, q2, q3, q4).getQuaternionAsVector();
                MatrixLibrary.Matrix bRif = magneticCompensation(new MyQuaternion(q1, q2, q3, q4), m_x, m_y, m_z);
                bx = bRif[0, 0];
                by = bRif[1, 0];
                bz = bRif[2, 0];


                //compute the objective functions
                f_obb[0, 0] = 2 * (q2 * q4 - q1 * q3) - a_x;
                f_obb[1, 0] = 2 * (q1 * q2 + q3 * q4) - a_y;
                f_obb[2, 0] = 2 * (0.5 - q2 * q2 - q3 * q3) - a_z;
                f_obb[3, 0] = 2 * bx * (0.5 - q3 * q3 - q4 * q4) + 2 * by * (q1 * q4 + q2 * q3) + 2 * bz * (q2 * q4 - q1 * q3) - m_x;
                f_obb[4, 0] = 2 * bx * (q2 * q3 - q1 * q4) + 2 * by * (0.5 - q2 * q2 - q4 * q4) + 2 * bz * (q1 * q2 + q3 * q4) - m_y;
                f_obb[5, 0] = 2 * bx * (q1 * q3 + q2 * q4) + 2 * by * (q3 * q4 - q1 * q2) + 2 * bz * (0.5 - q2 * q2 - q3 * q3) - m_z;

                //compute the jacobian
                Jacobian[0, 0] = -2 * q3;
                Jacobian[0, 1] = 2 * q4;
                Jacobian[0, 2] = -2 * q1;
                Jacobian[0, 3] = 2 * q2;
                Jacobian[1, 0] = 2 * q2;
                Jacobian[1, 1] = 2 * q1;
                Jacobian[1, 2] = 2 * q4;
                Jacobian[1, 3] = 2 * q3;
                Jacobian[2, 0] = 0;
                Jacobian[2, 1] = -4 * q2;
                Jacobian[2, 2] = -4 * q3;
                Jacobian[2, 3] = 0;

                Jacobian[3, 0] = 2 * by * q4 - 2 * bz * q3;
                Jacobian[3, 1] = 2 * by * q3 + 2 * bz * q4;
                Jacobian[3, 2] = -4 * bx * q3 + 2 * by * q2 - 2 * bz * q1;
                Jacobian[3, 3] = -4 * bx * q4 + 2 * by * q1 + 2 * bz * q2;
                Jacobian[4, 0] = -2 * bx * q4 + 2 * bz * q2;
                Jacobian[4, 1] = 2 * bx * q3 - 4 * by * q2 + 2 * bz * q1;
                Jacobian[4, 2] = 2 * bx * q2 + 2 * bz * q4;
                Jacobian[4, 3] = -2 * bx * q1 - 4 * by * q4 + 2 * bz * q3;
                Jacobian[5, 0] = 2 * bx * q3 - 2 * by * q2;
                Jacobian[5, 1] = 2 * bx * q4 - 2 * by * q1 - 4 * bz * q2;
                Jacobian[5, 2] = 2 * bx * q1 + 2 * by * q4 - 4 * bz * q3;
                Jacobian[5, 3] = 2 * bx * q2 + 2 * by * q3;

                Df = MatrixLibrary.Matrix.Multiply(MatrixLibrary.Matrix.Transpose(Jacobian), f_obb);
                norm = Math.Sqrt(Df[0, 0] * Df[0, 0] + Df[1, 0] * Df[1, 0] + Df[2, 0] * Df[2, 0] + Df[3, 0] * Df[3, 0]);
                Df = MatrixLibrary.Matrix.ScalarDivide(norm, Df);

                result = quaternion - mu * Df;

                q1 = result[0, 0];
                q2 = result[1, 0];
                q3 = result[2, 0];
                q4 = result[3, 0];

                norm = Math.Sqrt(q1 * q1 + q2 * q2 + q3 * q3 + q4 * q4);
                result = MatrixLibrary.Matrix.ScalarDivide(norm, result);

                q1 = result[0, 0];
                q2 = result[1, 0];
                q3 = result[2, 0];
                q4 = result[3, 0];

                i = i + 1;
            }

            return result;
        }

        protected MatrixLibrary.Matrix gaussNewtonMethod(double a_x, double a_y, double a_z, double m_x, double m_y, double m_z,MatrixLibrary.Matrix qObserv)
        {
            double norm;
            MatrixLibrary.Matrix n = new MatrixLibrary.Matrix(4, 1);
            MatrixLibrary.Matrix bRif = new MatrixLibrary.Matrix(3, 1);
            MatrixLibrary.Matrix jacobian = new MatrixLibrary.Matrix(6, 4);
            MatrixLibrary.Matrix R = new MatrixLibrary.Matrix(6, 6);
            MatrixLibrary.Matrix y_e = new MatrixLibrary.Matrix(6, 1);
            MatrixLibrary.Matrix y_b = new MatrixLibrary.Matrix(6, 1);

            double a = qObserv[1, 0];
            double b = qObserv[2, 0];
            double c = qObserv[3, 0];
            double d = qObserv[0, 0];

            int i = 0;

            MatrixLibrary.Matrix n_k = new MatrixLibrary.Matrix(4, 1);
            n_k[0, 0] = a;
            n_k[1, 0] = b;
            n_k[2, 0] = c;
            n_k[3, 0] = d;

            //Magnetometer Calibration values
            m_x = m_x * magnSFX + magnOffX;
            m_y = m_y * magnSFY + magnOffY;
            m_z = m_z * magnSFZ + magnOffZ;

            norm = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);
            m_x /= norm;
            m_y /= norm;
            m_z /= norm;


            while (i < 3)
            {
                MyQuaternion q = new MyQuaternion(d, a, b, c);

                bRif = magneticCompensation(q, m_x, m_y, m_z);
                double bx = bRif[0, 0];
                double by = bRif[1, 0];
                double bz = bRif[2, 0];

                //Jacobian Computation
                double j11 = (2 * a * a_x + 2 * b * a_y + 2 * c * a_z);
                double j12 = (-2 * b * a_x + 2 * a * a_y + 2 * d * a_z);
                double j13 = (-2 * c * a_x - 2 * d * a_y + 2 * a * a_z);
                double j14 = (2 * d * a_x - 2 * c * a_y + 2 * b * a_z);

                double j21 = (2 * b * a_x - 2 * a * a_y - 2 * d * a_z);
                double j22 = (2 * a * a_x + 2 * b * a_y + 2 * c * a_z);
                double j23 = (2 * d * a_x - 2 * c * a_y + 2 * b * a_z);
                double j24 = (2 * c * a_x + 2 * d * a_y - 2 * a * a_z);

                double j31 = (2 * c * a_x + 2 * d * a_y - 2 * a * a_z);
                double j32 = (-2 * d * a_x + 2 * c * a_y - 2 * b * a_z);
                double j33 = (2 * a * a_x + 2 * b * a_y + 2 * c * a_z);
                double j34 = (-2 * b * a_x + 2 * a * a_y + 2 * d * a_z);

                double j41 = (2 * a * m_x + 2 * b * m_y + 2 * c * m_z);
                double j42 = (-2 * b * m_x + 2 * a * m_y + 2 * m_z * d);
                double j43 = (-2 * c * m_x - 2 * d * m_y + 2 * a * m_z);
                double j44 = (2 * d * m_x - 2 * c * m_y + 2 * b * m_z);

                double j51 = (2 * b * m_x - 2 * a * m_y - 2 * d * m_z);
                double j52 = (2 * a * m_x + 2 * b * m_y + 2 * c * m_z);
                double j53 = (2 * d * m_x - 2 * c * m_y + 2 * b * m_z);
                double j54 = (2 * c * m_x + 2 * d * m_y - 2 * a * m_z);

                double j61 = (2 * c * m_x + 2 * d * m_y - 2 * a * m_z);
                double j62 = (-2 * d * m_x + 2 * c * m_y - 2 * b * m_z);
                double j63 = (2 * a * m_x + 2 * b * m_y + 2 * c * m_z);
                double j64 = (-2 * b * m_x + 2 * a * m_y + 2 * d * m_z);


                jacobian[0, 0] = j11;
                jacobian[0, 1] = j12;
                jacobian[0, 2] = j13;
                jacobian[0, 3] = j14;

                jacobian[1, 0] = j21;
                jacobian[1, 1] = j22;
                jacobian[1, 2] = j23;
                jacobian[1, 3] = j24;

                jacobian[2, 0] = j31;
                jacobian[2, 1] = j32;
                jacobian[2, 2] = j33;
                jacobian[2, 3] = j34;

                jacobian[3, 0] = j41;
                jacobian[3, 1] = j42;
                jacobian[3, 2] = j43;
                jacobian[3, 3] = j44;

                jacobian[4, 0] = j51;
                jacobian[4, 1] = j52;
                jacobian[4, 2] = j53;
                jacobian[4, 3] = j54;

                jacobian[5, 0] = j61;
                jacobian[5, 1] = j62;
                jacobian[5, 2] = j63;
                jacobian[5, 3] = j64;
                jacobian = -1 * jacobian;
                //End Jacobian Computation

                //DCM Rotation Matrix

                R[0, 0] = d * d + a * a - b * b - c * c;
                R[0, 1] = 2 * (a * b - c * d);
                R[0, 2] = 2 * (a * c + b * d);
                R[1, 0] = 2 * (a * b + c * d);
                R[1, 1] = d * d + b * b - a * a - c * c;
                R[1, 2] = 2 * (b * c - a * d);
                R[2, 0] = 2 * (a * c - b * d);
                R[2, 1] = 2 * (b * c + a * d);
                R[2, 2] = d * d + c * c - b * b - a * a;

                R[3, 3] = d * d + a * a - b * b - c * c;
                R[3, 4] = 2 * (a * b - c * d);
                R[3, 5] = 2 * (a * c + b * d);
                R[4, 3] = 2 * (a * b + c * d);
                R[4, 4] = d * d + b * b - a * a - c * c;
                R[4, 5] = 2 * (b * c - a * d);
                R[5, 3] = 2 * (a * c - b * d);
                R[5, 4] = 2 * (b * c + a * d);
                R[5, 5] = d * d + c * c - b * b - a * a;

                R[3, 0] = 0;
                R[3, 1] = 0;
                R[3, 2] = 0;
                R[4, 0] = 0;
                R[4, 1] = 0;
                R[4, 2] = 0;
                R[5, 0] = 0;
                R[5, 1] = 0;
                R[5, 2] = 0;

                R[0, 3] = 0;
                R[0, 4] = 0;
                R[0, 5] = 0;
                R[1, 3] = 0;
                R[1, 4] = 0;
                R[1, 5] = 0;
                R[2, 3] = 0;
                R[2, 4] = 0;
                R[2, 5] = 0;
                //End DCM

                //Reference Vector

                y_e[0, 0] = 0;
                y_e[1, 0] = 0;
                y_e[2, 0] = 1;
                y_e[3, 0] = bx;
                y_e[4, 0] = by;
                y_e[5, 0] = bz;
                //Body frame Vector

                y_b[0, 0] = a_x;
                y_b[1, 0] = a_y;
                y_b[2, 0] = a_z;
                y_b[3, 0] = m_x;
                y_b[4, 0] = m_y;
                y_b[5, 0] = m_z;

                //Gauss Newton Step
                n = n_k - MatrixLibrary.Matrix.Inverse((MatrixLibrary.Matrix.Transpose(jacobian) * jacobian)) * MatrixLibrary.Matrix.Transpose(jacobian) * (y_e - R * y_b);

                double normGauss = Math.Sqrt(n[0, 0] * n[0, 0] + n[1, 0] * n[1, 0] + n[2, 0] * n[2, 0] + n[3, 0] * n[3, 0]);

                n /= normGauss;
                //Console.Out.WriteLine(n + " "+ norm);

                a = n[0, 0];
                b = n[1, 0];
                c = n[2, 0];
                d = n[3, 0];

                n_k[0, 0] = a;
                n_k[1, 0] = b;
                n_k[2, 0] = c;
                n_k[3, 0] = d;

                i++;
            }
            return new MyQuaternion(d, a, b, c).getQuaternionAsVector();


        }

        protected MatrixLibrary.Matrix magneticCompensation(MyQuaternion q, double m_x, double m_y, double m_z)
        {

            MatrixLibrary.Matrix h = new MatrixLibrary.Matrix(4, 1);
            MatrixLibrary.Matrix temp;
            //compute the direction of the magnetic field
            MatrixLibrary.Matrix quaternion = q.getQuaternionAsVector();
            MatrixLibrary.Matrix quaternion_conjugate = q.getConjugate();

            //magnetic field compensation
            temp = MyQuaternion.quaternionProduct(quaternion, new MyQuaternion(0.0, m_x, m_y, m_z).getQuaternionAsVector());
            h = MyQuaternion.quaternionProduct(temp, quaternion_conjugate);
            double bx = Math.Sqrt((h[1, 0] * h[1, 0] + h[2, 0] * h[2, 0]));
            double bz = h[3, 0];

            double norm = Math.Sqrt(bx * bx + bz * bz);
            bx /= norm;
            bz /= norm;
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(3, 1);
            result[0, 0] = bx;
            result[1, 0] = 0;
            result[2, 0] = bz;
            return result;
        }

        public abstract void filterStep(double w_x, double w_y, double w_z, double a_x, double a_y, double a_z, double m_x, double m_y, double m_z);

        public abstract MatrixLibrary.Matrix getFilteredQuaternions();
        public abstract MatrixLibrary.Matrix getFilteredAngles();
        public void setObsMethod(int param){
            this.obsMethod = param;
        }
    }
}
