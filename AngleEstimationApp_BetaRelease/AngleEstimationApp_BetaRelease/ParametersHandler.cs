using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;


namespace AngleEstimationApp_BetaRelease
{
    class ParametersHandler
    {
        private static ParametersHandler instance;
        private XmlDocument document;
        private double magnXOff;
        private double magnYOff;
        private double magnZOff;

        private double magnXSF;
        private double magnYSF;
        private double magnZSF;

        private double gyroXOff;
        private double gyroYOff;
        private double gyroZOff;

        private ParametersHandler() {
            document = new XmlDocument();
        }

        public static ParametersHandler getIntance(){
            if(instance==null)
                instance=new ParametersHandler();

            return instance;
        }

        public void loadXMLParameters(){
            document.PreserveWhitespace = false;
            document.Load("parameters.xml");
        }

        public void setParams(double[] parameters) {
            XmlNodeList nodeList = document.GetElementsByTagName("offset");
            nodeList.Item(0).FirstChild.Value = parameters[0].ToString();
            nodeList.Item(1).FirstChild.Value = parameters[1].ToString();
            nodeList.Item(2).FirstChild.Value = parameters[2].ToString();

            nodeList = document.GetElementsByTagName("scalefactor");
            nodeList.Item(0).FirstChild.Value = parameters[3].ToString();
            nodeList.Item(1).FirstChild.Value = parameters[4].ToString();
            nodeList.Item(2).FirstChild.Value = parameters[5].ToString();

            nodeList = document.GetElementsByTagName("offset");
            nodeList.Item(3).FirstChild.Value = parameters[6].ToString();
            nodeList.Item(4).FirstChild.Value = parameters[7].ToString();
            nodeList.Item(5).FirstChild.Value = parameters[8].ToString();
        }

        public void loadMagnetometerParams() {
            XmlNodeList nodeList = document.GetElementsByTagName("offset");
            magnXOff = Double.Parse(nodeList.Item(0).FirstChild.Value.ToString());
            magnYOff = Double.Parse(nodeList.Item(1).FirstChild.Value.ToString());
            magnZOff = Double.Parse(nodeList.Item(2).FirstChild.Value.ToString());

            nodeList = document.GetElementsByTagName("scalefactor");
            magnXSF = Double.Parse(nodeList.Item(0).FirstChild.Value.ToString());
            magnYSF = Double.Parse(nodeList.Item(1).FirstChild.Value.ToString());
            magnZSF = Double.Parse(nodeList.Item(2).FirstChild.Value.ToString());
        }

        public void loadGyroscopeParams() {
            XmlNodeList nodeList = document.GetElementsByTagName("offset");
            gyroXOff = Double.Parse(nodeList.Item(3).FirstChild.Value.ToString());
            gyroYOff = Double.Parse(nodeList.Item(4).FirstChild.Value.ToString());
            gyroZOff = Double.Parse(nodeList.Item(5).FirstChild.Value.ToString());
        }

        public double[] getMagnetometerOffsets() {
            return new double[] { magnXOff, magnYOff, magnZOff };
        }
        public double[] getMagnetometerScaleFactors() {
            return new double[] { magnXSF, magnYSF, magnZSF };
        }
        public double[] getGyroOffsets() {
            return new double[] { gyroXOff, gyroYOff, gyroZOff };
        }

        public void saveDocument() {
            document.Save("parameters.xml");
        }
    }
}
