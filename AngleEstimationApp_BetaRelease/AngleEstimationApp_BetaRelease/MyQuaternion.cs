using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleEstimationApp_BetaRelease
{
    class MyQuaternion
    {
        private double q1;
        private double q2;
        private double q3;
        private double q4;

        public MyQuaternion(double real, double i, double j, double k) {
            this.q1 = real;
            this.q2 = i;
            this.q3 = j;
            this.q4 = k;
        }

        public MatrixLibrary.Matrix getQuaternionAsVector() {
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            result[0, 0] = q1;
            result[1, 0] = q2;
            result[2, 0] = q3;
            result[3, 0] = q4;
            return result;
        }

        public static MatrixLibrary.Matrix quaternionProduct(MatrixLibrary.Matrix quaternion,MatrixLibrary.Matrix matrix){
            double a1 = quaternion[0, 0];
            double a2 = quaternion[1, 0];
            double a3 = quaternion[2, 0];
            double a4 = quaternion[3, 0];
            double b1 = matrix[0, 0];
            double b2 = matrix[1, 0];
            double b3 = matrix[2, 0];
            double b4 = matrix[3, 0];

            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            result[0, 0] = a1 * b1 - a2 * b2 - a3 * b3 - a4 * b4;
            result[1, 0] = a1 * b2 + a2 * b1 + a3 * b4 - a4 * b3;
            result[2, 0] = a1 * b3 - a2 * b4 + a3 * b1 + a4 * b2;
            result[3, 0] = a1 * b4 + a2 * b3 - a3 * b2 + a4 * b1;

            return result;
        }

        public static MatrixLibrary.Matrix quaternionProduct(MyQuaternion q_a, MyQuaternion q_b) {
            MatrixLibrary.Matrix v1 = q_a.getQuaternionAsVector();
            MatrixLibrary.Matrix v2 = q_b.getQuaternionAsVector();
            return quaternionProduct(v1, v2);
        }

        public double getNorm() {
            double norm = Math.Sqrt((this.q1 * this.q1) + (this.q2 * this.q2) + (this.q3 * this.q3) + (this.q4 * this.q4));
            return norm;
        }

        public double get(int index) {
            if (index == 0)
                return q1;
            else
                if (index == 1)
                    return q2;
                else
                    if (index == 2)
                        return q3;
                    else
            return q4;
        }

        public MatrixLibrary.Matrix getConjugate(){
            return new MyQuaternion(this.q1,-this.q2,-this.q3,-this.q4).getQuaternionAsVector();
        }

        public static MatrixLibrary.Matrix getAnglesFromQuaternion(MatrixLibrary.Matrix q) {
            double q0 = q[0, 0];
            double q1 = q[1, 0];
            double q2 = q[2, 0];
            double q3 = q[3, 0];

            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(3, 1);

            result[0, 0] = Math.Atan2((2 * q2 * q3 + 2 * q0 * q1), (1 - 2 * q1 *q1 - 2 * q2*q2))*180.0/Math.PI;
            result[1, 0] = Math.Asin(-2 * q1 * q3 + 2 * q0 * q2)*180/Math.PI;
            result[2, 0] = Math.Atan2((2 * q1 * q2 + 2 * q0 * q3),( 1 - 2 * (q2*q2 + q3*q3))) * 180.0 / Math.PI;
            return result;
        }

        public static MatrixLibrary.Matrix getQuaternionFromAngles(MatrixLibrary.Matrix angle) {
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            angle=(angle*Math.PI)/180.0;
            double c1=Math.Cos(angle[0,0]/2);
            double c2=Math.Cos(angle[1,0]/2);
            double c3=Math.Cos(angle[2,0]/2);

            double s1=Math.Sin(angle[0,0]/2);
            double s2=Math.Sin(angle[1,0]/2);
            double s3=Math.Sin(angle[2,0]/2);

            result[0, 0] = c1 * c2 * c3 + s1 *s2 *s3;
            result[1, 0] = s1 * c2 * c3 - c1 * s2 * s3;
            result[2, 0] = c1 * s2 * c3 + s1 * c2 * s3;
            result[3, 0] = c1 * c2 * s3 - s1 * s2 * c3;

            return result;
        }
    }
}
