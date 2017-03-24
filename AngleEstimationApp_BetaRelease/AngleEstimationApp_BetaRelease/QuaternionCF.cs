using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleEstimationApp_BetaRelease
{
    class QuaternionCF:Filter
    {

        private double k;

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

        int step = 0;

        IIRFilter magnFilter;
        IIRFilter accFilter;

        MatrixLibrary.Matrix qFilt;
        MatrixLibrary.Matrix qGyroFilt;
        MatrixLibrary.Matrix qObserv;

        public QuaternionCF() {
            k = 0.98;
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

            accFilter = new IIRFilter(aAcc, bAcc);

            aMagn.Add(1);
            aMagn.Add(-1.73);
            aMagn.Add(0.76);

            bMagn.Add(0.0078);
            bMagn.Add(0.0156);
            bMagn.Add(0.0078);

            magnFilter = new IIRFilter(aMagn, bMagn);

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

            qFilt = new MatrixLibrary.Matrix(4, 1);
            qGyroFilt = new MatrixLibrary.Matrix(4, 1);
            qObserv = new MatrixLibrary.Matrix(4, 1);

            qFilt[0, 0] = 1;
            qFilt[1, 0] = 0;
            qFilt[2, 0] = 0;
            qFilt[3, 0] = 0;

            qGyroFilt[0, 0] = 1;
            qGyroFilt[1, 0] = 0;
            qGyroFilt[2, 0] = 0;
            qGyroFilt[3, 0] = 0;

            qObserv[0, 0] = 1;
            qObserv[1, 0] = 0;
            qObserv[2, 0] = 0;
            qObserv[3, 0] = 0;
        }

        public override void filterStep(double w_x, double w_y, double w_z, double a_x, double a_y, double a_z, double m_x, double m_y, double m_z)
        {
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

            if (step < 10)
            {
                
            }
            else
            {
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

                //Magnetometer Calibration values
                //m_x =m_x*1.2+ 0.3;
                //m_y = m_y*1.1+0.04;
                //m_z += -0.08;

                double normA = Math.Sqrt(a_x * a_x + a_y * a_y + a_z * a_z);
                double normM = Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z);

                a_x /= normA;
                a_y /= normA;
                a_z /= normA;

                m_x /= normM;
                m_y /= normM;
                m_z /= normM;

                w_x = (w_x - gyroOffRoll)*Math.PI/180;
                w_y = (-w_y + gyroOffPitch) * Math.PI / 180;
                w_z = (w_z - gyroOffYaw)*Math.PI/180;

                MatrixLibrary.Matrix dq = 0.5 * (MyQuaternion.quaternionProduct(qFilt, new MyQuaternion(0, w_x, w_y, w_z).getQuaternionAsVector()));
                qGyroFilt=qFilt + dq * dt;

                norm = Math.Sqrt(qGyroFilt[0, 0] * qGyroFilt[0, 0] + qGyroFilt[1, 0] * qGyroFilt[1, 0] + qGyroFilt[2, 0] * qGyroFilt[2, 0] + qGyroFilt[3, 0] * qGyroFilt[3, 0]);
                qGyroFilt /= norm;

                double dqNorm = Math.Sqrt(dq[0, 0] * dq[0, 0] + dq[1, 0] * dq[1, 0] + dq[2, 0] * dq[2, 0] + dq[3, 0] * dq[3, 0]);

                double mu = 10 * dqNorm * dt;
                if(this.obsMethod==0)
                    qObserv = GradientDescent(a_x, a_y, a_z, m_x, m_y, m_z, mu,qFilt);
                else
                    qObserv = gaussNewtonMethod(a_x, a_y, a_z, m_x, m_y, m_z,qFilt);

                qFilt = qGyroFilt * k + qObserv * (1 - k);
                norm = Math.Sqrt(qFilt[0, 0] * qFilt[0, 0] + qFilt[1, 0] * qFilt[1, 0] + qFilt[2, 0] * qFilt[2, 0] + qFilt[3, 0] * qFilt[3, 0]);
                qFilt /= norm;
            }

            step++;

        }

        

        public override MatrixLibrary.Matrix getFilteredQuaternions() {
            return qFilt;
        }

        public override MatrixLibrary.Matrix getFilteredAngles() {
            return null;
        }
    }
}
