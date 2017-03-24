using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleEstimationApp
{
    public class Parameters
    {
        public double gyroOffX;
        public double gyroOffY;
        public double gyroOffZ;
        public double gyroVarX;
        public double gyroVarY;
        public double gyroVarZ;
        public double AHRSpar1;
        public double AHRSpar2;
        public int freq;
        public double magnSFX;
        public double magnSFY;
        public double magnSFZ;
        public double magnOffX;
        public double magnOffY;
        public double magnOffZ;
        public int porta;

        
        //public Parameters(double gyroOffX, double gyroOffY, double gyroOffZ, double gyroVarX, double gyroVarY, double gyroVarZ, double AHRSpar1, double AHRSpar2, int freq, double magnSFX, double magnSFY, double magnSFZ, double magnOffX, double magnOffY, double magnOffZ)
        //{
        //    this.AHRSpar1 = AHRSpar1;
        //    this.AHRSpar2 = AHRSpar2;
        //    this.freq = freq;
        //    this.gyroOffX = gyroOffX;
        //    this.gyroOffY = gyroOffY;
        //    this.gyroOffZ = gyroOffZ;
        //    this.gyroVarX = gyroVarX;
        //    this.gyroVarY = gyroVarY;
        //    this.gyroVarZ = gyroVarZ;
        //    this.magnOffX = magnOffX;
        //    this.magnOffY = magnOffY;
        //    this.magnOffZ = magnOffZ;
        //    this.magnSFX = magnSFX;
        //    this.magnSFY = magnSFY;
        //    this.magnSFZ=magnSFZ;
        //}
        public Parameters()
        {
        }
    }
}
