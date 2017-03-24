using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPY;
using MatrixLibrary;
using AngleEstimationApp;

namespace RPY
{
    

    class ComplementaryFilter
    {
        private double dt;// = 1.0 / 50.0;                                   // sampling period
        private double gyroOffRoll;// = -0.3866;
        private double gyroOffPitch;// = -3.5042;
        private double gyroOffYaw;// = -2.7279;

        private double k;
        
        //static double[] aAcc = new double[4] { 1, -2.9529, 2.9069, -0.954 };                                      //parameters a of the IIr filter for the accelerometer
        //static double[] bAcc = new double[4] { 0.000001597, 0.000004792, 0.000004792, 0.000001597 };              //parameters b of the IIr filter for the accelerometer

        //static double[] aMagn = new double[3] { 1, -1.7347, 0.766 };                      //parameters a of the IIr filter for the magnetometer
        //static double[] bMagn = new double[3] { 0.0078, 0.0156, 0.0078 };                //parameters b of the IIr filter for the magnetometer

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


        double w_x_old = 0;
        double w_y_old = 0;
        double w_z_old = 0;

        int step=0;

        double[] anglesGyro;
        double[] anglesEuler;
        double[] anglesFiltered;
        double[] anglesGyroFiltered;

        Filter magnFilter;
        Filter accFilter;

        private static Matrix calibrationMatrix;

        public ComplementaryFilter(Parameters param) {
            k = 0.98;
            anglesGyro = new double[3];
            anglesEuler = new double[3];
            anglesFiltered = new double[3];
            anglesGyroFiltered = new double[3];

            aAcc = new List<double>();
            bAcc = new List<double>();
            aMagn= new List<double>();
            bMagn= new List<double>();

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

            AccObservX=new List<double>();
            AccObservY=new List<double>();
            AccObservZ=new List<double>();
            AccFiltX=new List<double>();
            AccFiltY=new List<double>();
            AccFiltZ=new List<double>();
            
            MagnObservX = new List<double>();
            MagnObservY = new List<double>();
            MagnObservZ = new List<double>();
            MagnFiltX = new List<double>();
            MagnFiltY = new List<double>();
            MagnFiltZ = new List<double>();

            calibrationMatrix = new Matrix(4, 3);
            calibrationMatrix[0,0]=0.9874;
            calibrationMatrix[0,1]=0.00031803;
            calibrationMatrix[0,2]=-0.0110;
            calibrationMatrix[1,0]=0.0314;
            calibrationMatrix[1,1]=0.9933;
            calibrationMatrix[1,2]=0.0054;
            calibrationMatrix[2,0]=0.0402;
            calibrationMatrix[2,1]=-0.0277;
            calibrationMatrix[2,2]=0.9912;
            calibrationMatrix[3,0]=-0.0099;
            calibrationMatrix[3,1]=0.0133;
            calibrationMatrix[3, 2] = 0.0052;

            dt=1.0/param.freq;                                 // sampling period
            gyroOffRoll=param.gyroOffX;
            gyroOffPitch=param.gyroOffY;
            gyroOffYaw=param.gyroOffZ;
            
        }

        public void complementaryFiltering(double w_x, double w_y, double w_z, double a_x, double a_y, double a_z, double m_x, double m_y, double m_z)
        {
            a_x = a_x / 1000;
            a_y = a_y / 1000;
            a_z = a_z / 1000;

            Matrix observations = new Matrix(1, 4);
            observations[0,0]=a_x;
            observations[0,1]=a_y;
            observations[0,2]=a_z;
            observations[0, 3] = 1;

            Matrix realAcc = Matrix.Multiply(observations, calibrationMatrix);

            a_x = realAcc[0, 0];
            a_y = realAcc[0, 1];
            a_z = realAcc[0, 2];

            // normalise the accelerometer measurement
            double norm = Math.Sqrt(a_x * a_x + a_y * a_y + a_z * a_z);
            a_x /= norm;
            a_y /= norm;
            a_z /= norm;

            // normalise the magnetometer measurement
            norm = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);
            m_x /= norm;
            m_y /= norm;
            m_z /= norm;

            if (AccObservX.Count < 10) {
                AccObservX.Add(a_x);
                AccObservY.Add(a_y);
                AccObservZ.Add(a_z);
            }
            else {
                AccObservX.RemoveAt(0);
                AccObservY.RemoveAt(0);
                AccObservZ.RemoveAt(0);

                AccObservX.Add(a_x);
                AccObservY.Add(a_y);
                AccObservZ.Add(a_z);
            }
            if (MagnObservX.Count < 10) {
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

            if (step < 10)
            {
                anglesEuler = computeAnglesFromAccAndMagn(new double[] { a_x, a_y, a_z }, new double[] { m_x, m_y, m_z });
                anglesFiltered[0] = anglesEuler[0];
                anglesFiltered[1] = anglesEuler[1];
                anglesFiltered[2] = anglesEuler[2];
                anglesGyro[0] = anglesEuler[0];
                anglesGyro[1] = anglesEuler[1];
                anglesGyro[2] = anglesEuler[2];
                anglesGyroFiltered[0] = anglesEuler[0];
                anglesGyroFiltered[1] = anglesEuler[1];
                anglesGyroFiltered[2] = anglesEuler[2];

            }
            else
            {
                
                //Filtering
                accFilter.Applyfilter(AccObservX, out AccFiltX);
                accFilter.Applyfilter(AccObservY, out AccFiltY);
                accFilter.Applyfilter(AccObservZ, out AccFiltZ);

                magnFilter.Applyfilter(MagnObservX, out MagnFiltX);
                magnFilter.Applyfilter(MagnObservY, out MagnFiltY);
                magnFilter.Applyfilter(MagnObservZ, out MagnFiltZ);

                a_x = AccFiltX.Last();
                a_y = AccFiltY.Last();
                a_z = AccFiltZ.Last();

                m_x = MagnFiltX.Last();
                m_y = MagnFiltY.Last();
                m_z = MagnFiltZ.Last();

                double normA = Math.Sqrt(a_x * a_x + a_y * a_y + a_z * a_z);
                double normM = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);

                a_x /= normA;
                a_y /= normA;
                a_z /= normA;

                m_x /= normM;
                m_y /= normM;
                m_z /= normM;

                w_x = w_x - gyroOffRoll;
                w_y = w_y - gyroOffPitch;
                w_z = -w_z + gyroOffYaw;

                //w_x = (w_x + w_x_old) / 2;
                //w_y = (w_y + w_y_old) / 2;
                //w_z = (w_z + w_z_old) / 2;

                anglesGyro[0] = anglesGyro[0] + w_x * dt;
                anglesGyro[1] = anglesGyro[1] + w_y * dt;
                anglesGyro[2] = anglesGyro[2] + w_z * dt;

                double[] dEulerGyroFiltered = computeEulerFromGyro(new double[] { w_x, w_y, w_z }, anglesFiltered);

                dEulerGyroFiltered[0] = dEulerGyroFiltered[0] * dt;
                dEulerGyroFiltered[1] = dEulerGyroFiltered[1] * dt;
                dEulerGyroFiltered[2] = dEulerGyroFiltered[2] * dt;

                anglesGyroFiltered[0] = anglesFiltered[0] + dEulerGyroFiltered[0];
                anglesGyroFiltered[1] = anglesFiltered[1] + dEulerGyroFiltered[1];
                anglesGyroFiltered[2] = anglesFiltered[2] + dEulerGyroFiltered[2];

                anglesEuler = computeAnglesFromAccAndMagn(new double[] { a_x, a_y, a_z }, new double[] { m_x, m_y, m_z });

                for (int j = 0; j < 3; j++)
                {
                    if ((180 - Math.Abs(anglesEuler[j])) < 15 && (180 - Math.Abs(anglesGyroFiltered[j])) < 15)
                    {
                        if (Math.Sign(anglesEuler[j]) != Math.Sign(anglesGyroFiltered[j]))
                            anglesGyroFiltered[j] = Math.Sign(anglesEuler[j]) * (360 - Math.Abs(anglesGyroFiltered[j]));
                    }
                }

                anglesFiltered[0] = k * anglesGyroFiltered[0] + (1-k) * anglesEuler[0];
                anglesFiltered[1] = k * anglesGyroFiltered[1] + (1 - k) * anglesEuler[1];
                anglesFiltered[2] = k * anglesGyroFiltered[2] + (1 - k) * anglesEuler[2];

                anglesFiltered = limitRotations(anglesFiltered);
                anglesGyroFiltered = limitRotations(anglesGyroFiltered);
            }
            step++;
        }
        
        public double[] computeAnglesFromAccAndMagn(double[] acc, double[] magn)
        {
            double gamma=0;
            double phi=0;
            double psi=0;
            if (Math.Abs(acc[0]) > 0.99)
            {

            }
            else { 
                if(Math.Abs(acc[1])>0.99){}
                else{
                    
                        gamma=Math.Asin(acc[1]);
                        phi=Math.Asin(acc[0]);

                        double bx = magn[0] * Math.Cos(phi) + magn[2] * Math.Sin(-phi);
                        double by = magn[0] * Math.Sin(gamma) * Math.Sin(-phi) + magn[1] * Math.Cos(gamma) - magn[2] * Math.Sin(gamma) * Math.Cos(phi);

                        psi = Math.Atan2((by * 1.0672 + 0.1075), (bx + 0.3159));                        
                }
                
            }

            gamma = gamma * 180 / Math.PI;
            phi = phi * 180 / Math.PI;
            psi = psi * 180 / Math.PI;
            return new double[]{gamma,phi,psi};
        }

        public double[] computeEulerFromGyro(double[] w,double[] anglesEuler1) {
            
            for(int i=0;i<anglesEuler.Length;i++){
                anglesEuler1[i]=anglesEuler1[i]/180*Math.PI;
            }
            
            Matrix m = new Matrix(3, 3);

            m[0, 0] = 0;
            m[0, 1] = Math.Sin(anglesEuler1[0]);
            m[0, 2] = Math.Cos(anglesEuler1[0]);

            m[1, 0] = 0;
            m[1, 1] = Math.Cos(anglesEuler1[0])*Math.Cos(anglesEuler1[1]);
            m[1, 2] = -Math.Sin(anglesEuler1[0])*Math.Cos(anglesEuler1[1]);

            m[2, 0] = Math.Cos(anglesEuler1[1]);
            m[2, 1] = Math.Sin(anglesEuler1[0])*Math.Sin(anglesEuler1[1]);
            m[2, 2] = Math.Cos(anglesEuler1[0])*Math.Sin(anglesEuler1[1]);

            Matrix omega = new Matrix(3, 1);
            omega[0, 0] = w[0];
            omega[1, 0] = w[1];
            omega[2, 0] = w[2];
            Matrix dAnglesM = Matrix.ScalarMultiply(1 / Math.Cos(anglesEuler1[1]),Matrix.Multiply(m, omega));

            double[] dAngles = new double[3];

            dAngles[0] = dAnglesM[2, 0];
            dAngles[1] = dAnglesM[1, 0];
            dAngles[2] = dAnglesM[0, 0];

            for (int i = 0; i < anglesEuler.Length; i++)
            {
                anglesEuler1[i] = anglesEuler1[i] * 180 / Math.PI;
            }

            return dAngles;
        }

        public double[] getAnglesFiltered() {
            return anglesFiltered;
        }

        public double[] limitRotations(double[] angles)
        {
            for (int k = 0; k < 3; k++)
            {
                if (angles[k] < 0)
                {
                    if (angles[k] < -180)
                    {
                        angles[k] = 360 + angles[k];
                    }
                }
                else
                {
                    if (angles[k] >= 180)
                    {
                        angles[k] = 360 - angles[k];
                    }
                }
            }
            return angles;
        }
        
    }

    
}
