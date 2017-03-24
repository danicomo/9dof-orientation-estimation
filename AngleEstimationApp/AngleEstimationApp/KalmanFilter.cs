using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixLibrary;
using RPY;

namespace AngleEstimationApp
{
    class KalmanFilter
    {
        //private double gyroMeasError = 10;                                            // gyroscope measurement error (in degrees per second)
        //private double gyroBiasDrift = 0.1;                                           // gyroscope bias drift (in degrees per second per second)

        //public double SEq_1 = 1, SEq_2 = 0, SEq_3 = 0, SEq_4 = 0;                   // earth relative to sensor quaternion elements with initial conditions
        //private double beta;
        //private double zeta;
        //private double dt = 1.0 / 200.0;                                   // sampling period
        //private double b_x = 1, b_z = 0;                                            // reference direction of earth's magnetic field
        //private double w_bx = 0, w_by = 0, w_bz = 0;
        //-0.3866,-3.5042,2.7279
        private double gyroOffRoll = 0;//-0.3866;
        private double gyroOffPitch = 0;//-3.5042;
        private double gyroOffYaw = 0;//2.7279;

        private double dt;

        private Parameters param;
        //private double gyroOffRoll = -0.3866;
        //private double gyroOffPitch = -3.5042;
        //private double gyroOffYaw = 2.7279;


        //private double sigmaRoll = Math.Pow((0.5647 / 180 * Math.PI), 2);           //the variance of the roll measure
        //private double sigmaPitch = Math.Pow((0.5674 / 180 * Math.PI), 2);          //the variance of the pitch measure
        //private double sigmaYaw = Math.Pow((0.5394 / 180 * Math.PI), 2);            //the variance of the yaw measure

        //private double sigmaRoll = Math.Pow((0.3 / 180 * Math.PI), 2);           //the variance of the roll measure
        //private double sigmaPitch = Math.Pow((0.3 / 180 * Math.PI), 2);          //the variance of the pitch measure
        //private double sigmaYaw = Math.Pow((0.3 / 180 * Math.PI), 2);            //the variance of the yaw measure

        public double q_filt1, q_filt2, q_filt3, q_filt4;       //quaternioni filtrati

        Matrix weOld;

        public List<double> aAcc;
        public List<double> bAcc;
        public List<double> aMagn;
        public List<double> bMagn;

        public List<double> AccObservX;
        public List<double> AccObservY;
        public List<double> AccObservZ;

        public List<double> AccFiltX;
        public List<double> AccFiltY;
        public List<double> AccFiltZ;

        public List<double> MagnObservX;
        public List<double> MagnObservY;
        public List<double> MagnObservZ;

        public List<double> MagnFiltX;
        public List<double> MagnFiltY;
        public List<double> MagnFiltZ;


        //int dim = 4;
        double w_x_old = 0;
        double w_y_old = 0;
        double w_z_old = 0;


        Matrix F = new Matrix(4, 4);
        Matrix H = new Matrix(Matrix.Identity(4));
        Matrix Q = new Matrix(4, 4);
        Matrix R = new Matrix(Matrix.Identity(4));

        //the state of the filter is composed of a quaternion, where the real part is in the first position
        Matrix state_filtered = new Matrix(4, 1);           //output of the filter. This is the update of the state
        Matrix state_predicted = new Matrix(4, 1);          //the predictiion of the state x(k|k-1), calculated from the gyro
        Matrix state_observed = new Matrix(4, 1);           //the observation calculated from the accelerometer and the magnetometer y(k). 

        Matrix P_predicted = new Matrix(4, 4);             //the variance of prediction of the state
        Matrix P_Update = new Matrix(4, 4);                 //the variance of the update of the state
        Matrix K = new Matrix(4, 4);                        //the gain of the filter

        Stack<double[]> MagnOsserv = new Stack<double[]>(4);
        Stack<double[]> AccOsserv = new Stack<double[]>(4);

        int countdata = 0;
        Filter magnFilter;
        Filter accFilter;

        public KalmanFilter(Parameters param)
        {
            this.param = param;
            double sigmaRoll = Math.Pow((param.gyroVarX / 180 * Math.PI), 2);           //the variance of the roll measure
            double sigmaPitch = Math.Pow((param.gyroVarY / 180 * Math.PI), 2);          //the variance of the pitch measure
            double sigmaYaw = Math.Pow((param.gyroVarZ / 180 * Math.PI), 2);            //the variance of the yaw measure
            
            dt = 1.0 / param.freq;  

            //Compute matrix Q (costant if the time interval is costant)
            Q[0, 0] = sigmaRoll + sigmaPitch + sigmaYaw;
            Q[0, 1] = -sigmaRoll + sigmaPitch - sigmaYaw;
            Q[0, 2] = -sigmaRoll - sigmaPitch + sigmaYaw;
            Q[0, 3] = sigmaRoll - sigmaPitch - sigmaYaw;
            Q[1, 0] = -sigmaRoll + sigmaPitch - sigmaYaw;
            Q[1, 1] = sigmaRoll + sigmaPitch + sigmaYaw;
            Q[1, 2] = sigmaRoll - sigmaPitch - sigmaYaw;
            Q[1, 3] = -sigmaRoll - sigmaPitch + sigmaYaw;
            Q[2, 0] = -sigmaRoll - sigmaPitch + sigmaYaw;
            Q[2, 1] = sigmaRoll - sigmaPitch - sigmaYaw;
            Q[2, 2] = sigmaRoll + sigmaPitch + sigmaYaw;
            Q[2, 3] = -sigmaRoll + sigmaPitch - sigmaYaw;
            Q[3, 0] = sigmaRoll - sigmaPitch - sigmaYaw;
            Q[3, 1] = -sigmaRoll - sigmaPitch + sigmaYaw;
            Q[3, 2] = -sigmaRoll + sigmaPitch - sigmaYaw;
            Q[3, 3] = sigmaRoll + sigmaPitch + sigmaYaw;

            //double scalar=System.Math.Pow((dt/2),2);
            //Q=Matrix.ScalarMultiply(scalar,Q);

            //R = Matrix.ScalarMultiply(0.07, R);
            R = new Matrix(4, 4);
            R[0, 0] = 0.1;
            R[0, 1] = 0;
            R[0, 2] = 0;
            R[0, 3] = 0;

            R[1, 0] = 0;
            R[1, 1] = 0.1;
            R[1, 2] = 0;
            R[1, 3] = 0;

            R[2, 0] = 0;
            R[2, 1] = 0;
            R[2, 2] = 0.1;
            R[2, 3] = 0;

            R[3, 0] = 0;
            R[3, 1] = 0;
            R[3, 2] = 0;
            R[3, 3] = 0.1;



            //Initial conditions
            state_observed = newQuaternion(0.5, 0.5, 0.5, 0.5);
            state_filtered = newQuaternion(0.5, 0.5, 0.5, 0.5);
            state_predicted = newQuaternion(0.5, 0.5, 0.5, 0.5);
            P_Update = Matrix.ScalarMultiply(0.1, new Matrix(Matrix.Identity(4)));

            aAcc = new List<double>();
            bAcc = new List<double>();
            aMagn = new List<double>();
            bMagn = new List<double>();

            aAcc.Add(1);
            aAcc.Add(-2.9529);
            aAcc.Add(2.9069);
            aAcc.Add(-0.954);

            bAcc.Add(0.000001597);
            bAcc.Add(0.000004792);
            bAcc.Add(0.000004792);
            bAcc.Add(0.000001597);

            accFilter = new Filter(aAcc, bAcc);

            aMagn.Add(1);
            aMagn.Add(-1.73);
            aMagn.Add(0.76);

            bMagn.Add(0.0078);
            bMagn.Add(0.0156);
            bMagn.Add(0.0078);

            magnFilter = new Filter(aMagn, bMagn);

            AccObservX = new List<double>();
            AccObservY = new List<double>();
            AccObservZ = new List<double>();
            AccFiltX = new List<double>();
            AccFiltY = new List<double>();
            AccFiltZ = new List<double>();

            MagnObservX = new List<double>();
            MagnObservY = new List<double>();
            MagnObservZ = new List<double>();
            MagnFiltX = new List<double>();
            MagnFiltY = new List<double>();
            MagnFiltZ = new List<double>();

            weOld = new Matrix(4, 1);
            weOld = Matrix.ScalarMultiply(0, weOld);

            
        }

        public void filter_step(double w_x, double w_y, double w_z, double a_x, double a_y, double a_z, double m_x, double m_y, double m_z)
        {
            double norm;
            Matrix temp;//=new Matrix(4,4);
            Matrix dq = new Matrix(4, 1);
            double mu;

            // normalise the accelerometer measurement
            norm = Math.Sqrt(a_x * a_x + a_y * a_y + a_z * a_z);
            a_x /= norm;
            a_y /= norm;
            a_z /= norm;

            // normalise the magnetometer measurement
            norm = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);
            m_x /= norm;
            m_y /= norm;
            m_z /= norm;

            if (AccObservX.Count < 10)
            {
                AccObservX.Add(a_x);
                AccObservY.Add(a_y);
                AccObservZ.Add(a_z);
            }
            else
            {
                AccObservX.RemoveAt(0);
                AccObservY.RemoveAt(0);
                AccObservZ.RemoveAt(0);

                AccObservX.Add(a_x);
                AccObservY.Add(a_y);
                AccObservZ.Add(a_z);
            }
            if (MagnObservX.Count < 10)
            {
                MagnObservX.Add(m_x);
                MagnObservY.Add(m_y);
                MagnObservZ.Add(m_z);
            }
            else
            {
                MagnObservX.RemoveAt(0);
                MagnObservY.RemoveAt(0);
                MagnObservZ.RemoveAt(0);
                MagnObservX.Add(m_x);
                MagnObservY.Add(m_y);
                MagnObservZ.Add(m_z);

                //Filter stabilization
                accFilter.Applyfilter(AccObservX, out AccFiltX);
                accFilter.Applyfilter(AccObservY, out AccFiltY);
                accFilter.Applyfilter(AccObservZ, out AccFiltZ);

                magnFilter.Applyfilter(MagnObservX, out MagnFiltX);
                magnFilter.Applyfilter(MagnObservY, out MagnFiltY);
                magnFilter.Applyfilter(MagnObservZ, out MagnFiltZ);
            }

            if (countdata > 10)
            {
                a_x = AccFiltX.Last();
                a_y = AccFiltY.Last();
                a_z = AccFiltZ.Last();

                m_x = MagnFiltX.Last();
                m_y = MagnFiltY.Last();
                m_z = MagnFiltZ.Last();

                // normalise the accelerometer measurement
                norm = Math.Sqrt(a_x * a_x + a_y * a_y + a_z * a_z);
                a_x /= norm;
                a_y /= norm;
                a_z /= norm;

                // normalise the magnetometer measurement
                norm = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);
                m_x /= norm;
                m_y /= norm;
                m_z /= norm;
            }



            countdata++;

            //dtt = DateTime.Now;

            w_x = w_x - param.gyroOffX;
            w_y = -w_y + param.gyroOffY;
            w_z = w_z - param.gyroOffZ;

            w_x = w_x * Math.PI / 180;
            w_y = w_y * Math.PI / 180;
            w_z = w_z * Math.PI / 180;

            w_x = (w_x + w_x_old) / 2;
            w_y = (w_y + w_y_old) / 2;
            w_z = (w_z + w_z_old) / 2;


            /*if (countdata > 11)
            {
                Matrix qUpdateOld = newQuaternion(q_filt1, q_filt2, q_filt3, q_filt4);
                Matrix dq1 = Matrix.ScalarMultiply(0.5, QuaternionProduct(qUpdateOld, newQuaternion(0, w_x, w_y, w_z)));
                Matrix we = Matrix.ScalarMultiply(2, QuaternionProduct(qUpdateOld, dq1));
                we = (we*dt + weOld);
                weOld = we;
                w_x = w_x - we[1, 0];
                w_y = w_y - we[2, 0];
                w_z = w_z - we[3, 0];
            }*/


            //calculate the observation via the Gradient Descent method
            dq = 0.5 * (QuaternionProduct(state_observed, newQuaternion(0.0, w_x, w_y, w_z)));
            norm = Math.Sqrt(dq[0, 0] * dq[0, 0] + dq[1, 0] * dq[1, 0] + dq[2, 0] * dq[2, 0] + dq[3, 0] * dq[3, 0]);
            //dq[0,0]/=norm;
            //dq[1,0]/=norm;
            //dq[2,0]/=norm;
            //dq[3,0]/=norm;
            mu = 10 * norm * dt;
            state_observed = GradientDescent(a_x, a_y, a_z, m_x, m_y, m_z, mu);

            //KALMAN FILTERING

            //F matrix computing
            F[0, 0] = 1;
            F[0, 1] = -dt / 2 * w_x;
            F[0, 2] = -dt / 2 * w_y;
            F[0, 3] = -dt / 2 * w_z;
            F[1, 0] = dt / 2 * w_x;
            F[1, 1] = 1;
            F[1, 2] = dt / 2 * w_z;
            F[1, 3] = -dt / 2 * w_y;
            F[2, 0] = dt / 2 * w_y;
            F[2, 1] = -dt / 2 * w_z;
            F[2, 2] = 1;
            F[2, 3] = dt / 2 * w_x;
            F[3, 0] = dt / 2 * w_z;
            F[3, 1] = dt / 2 * w_y;
            F[3, 2] = -dt / 2 * w_x;
            F[3, 3] = 1;

            //state prediction
            //state_filtered was computed at the previous step(if step is the first, then are the intial values).
            state_predicted = Matrix.Multiply(F, state_filtered);

            //calculate the variance of the prediction
            P_predicted = Matrix.Multiply(F, P_Update);
            P_predicted = Matrix.Multiply(P_predicted, Matrix.Transpose(F)) + Q;

            //compute the gain of the filter K
            temp = Matrix.Add(Matrix.Multiply(Matrix.Multiply(H, P_predicted), Matrix.Transpose(H)), R);
            temp = Matrix.Inverse(temp);
            K = Matrix.Multiply(Matrix.Multiply(P_predicted, Matrix.Transpose(H)), temp);

            //update the state of the system (this is the output of the filter)
            temp = Matrix.Subtract(state_observed, Matrix.Multiply(H, state_predicted));
            state_filtered = Matrix.Add(state_predicted, Matrix.Multiply(K, temp));

            //compute the variance of the state filtered
            temp = Matrix.Subtract((new Matrix(Matrix.Identity(4))), Matrix.Multiply(K, H));
            P_Update = Matrix.Multiply(temp, P_predicted);

            norm = Math.Sqrt(state_filtered[0, 0] * state_filtered[0, 0] + state_filtered[1, 0] * state_filtered[1, 0] + state_filtered[2, 0] * state_filtered[2, 0] + state_filtered[3, 0] * state_filtered[3, 0]);
            state_filtered = Matrix.ScalarDivide(norm, state_filtered);
            q_filt1 = state_filtered[0, 0];
            q_filt2 = state_filtered[1, 0];
            q_filt3 = state_filtered[2, 0];
            q_filt4 = state_filtered[3, 0];

            //TimeSpan duration = DateTime.Now - dtt;
            //time = duration.Milliseconds;

            //Console.WriteLine(time);
            w_x_old = w_x;
            w_y_old = w_y;
            w_z_old = w_z;
        }

        private Matrix GradientDescent(double a_x, double a_y, double a_z, double m_x, double m_y, double m_z, double mu)
        {
            int i = 0;
            double q1, q2, q3, q4;
            Matrix h = new Matrix(4, 1);     //magnetic field
            Matrix f_obb = new Matrix(6, 1);
            Matrix Jacobian = new Matrix(6, 4);
            Matrix temp;
            Matrix Df = new Matrix(4, 1);
            double norm;
            double bx, bz, by;
            Matrix result = new Matrix(4, 1);
            //m_x = m_x + 0.32;
            //m_y = m_y + 0.1;
            //oss=sf*reading+offset
            m_x = (param.magnSFX * m_x) + param.magnOffX;
            m_y = (param.magnSFY * m_y) + param.magnOffY;
            m_z = (param.magnSFZ * m_z) + param.magnOffZ;

            q1 = state_observed[0, 0];
            q2 = state_observed[1, 0];
            q3 = state_observed[2, 0];
            q4 = state_observed[3, 0];

            while (i < 1)
            {
                //compute the direction of the magnetic field
                Matrix quaternion = newQuaternion(q1, q2, q3, q4);
                Matrix quaternion_conjugate = newQuaternion(q1, -q2, -q3, -q4);

                //magnetic field compensation
                temp = QuaternionProduct(quaternion, newQuaternion(0.0, m_x, m_y, m_z));
                h = QuaternionProduct(temp, quaternion_conjugate);
                bx = Math.Sqrt((h[1, 0] * h[1, 0] + h[2, 0] * h[2, 0]));
                bz = h[3, 0];


                //bx = h[1, 0];
                //by = h[2, 0];
                //bz = h[3, 0];

                norm = Math.Sqrt(bx * bx + bz * bz);
                bx /= norm;
                by = 0;
                bz /= norm;


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

                Df = Matrix.Multiply(Matrix.Transpose(Jacobian), f_obb);
                norm = Math.Sqrt(Df[0, 0] * Df[0, 0] + Df[1, 0] * Df[1, 0] + Df[2, 0] * Df[2, 0] + Df[3, 0] * Df[3, 0]);
                Df = Matrix.ScalarDivide(norm, Df);

                result = quaternion - mu * Df;

                q1 = result[0, 0];
                q2 = result[1, 0];
                q3 = result[2, 0];
                q4 = result[3, 0];

                norm = Math.Sqrt(q1 * q1 + q2 * q2 + q3 * q3 + q4 * q4);
                result = Matrix.ScalarDivide(norm, result);

                q1 = result[0, 0];
                q2 = result[1, 0];
                q3 = result[2, 0];
                q4 = result[3, 0];

                i = i + 1;
            }

            return result;
        }

        private Matrix QuaternionProduct(Matrix quaternion, Matrix matrix)
        {
            double a1 = quaternion[0, 0];
            double a2 = quaternion[1, 0];
            double a3 = quaternion[2, 0];
            double a4 = quaternion[3, 0];
            double b1 = matrix[0, 0];
            double b2 = matrix[1, 0];
            double b3 = matrix[2, 0];
            double b4 = matrix[3, 0];

            Matrix result = new Matrix(4, 1);
            result[0, 0] = a1 * b1 - a2 * b2 - a3 * b3 - a4 * b4;
            result[1, 0] = a1 * b2 + a2 * b1 + a3 * b4 - a4 * b3;
            result[2, 0] = a1 * b3 - a2 * b4 + a3 * b1 + a4 * b2;
            result[3, 0] = a1 * b4 + a2 * b3 - a3 * b2 + a4 * b1;

            return result;

        }

        private Matrix newQuaternion(double p, double m_x, double m_y, double m_z)
        {
            Matrix quaternion = new Matrix(4, 1);
            quaternion[0, 0] = p;
            quaternion[1, 0] = m_x;
            quaternion[2, 0] = m_y;
            quaternion[3, 0] = m_z;
            return quaternion;
        }

    }
}
