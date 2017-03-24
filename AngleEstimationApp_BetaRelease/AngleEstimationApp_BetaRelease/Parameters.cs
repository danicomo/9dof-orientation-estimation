using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AngleEstimationApp_BetaRelease
{
    public partial class ParametersForm : Form
    {
        ParametersHandler paramHandler;
        public ParametersForm()
        {
            InitializeComponent();
            paramHandler = ParametersHandler.getIntance();
            setUpTexts();
        }

        private void setUpTexts() {
            paramHandler.loadXMLParameters();
            paramHandler.loadMagnetometerParams();
            paramHandler.loadGyroscopeParams();
            double[] param=paramHandler.getMagnetometerOffsets();
            this.xOff.Text = param[0].ToString();
            this.yOff.Text = param[1].ToString();
            this.zOff.Text = param[2].ToString();
            param = paramHandler.getMagnetometerScaleFactors();
            this.xScale.Text = param[0].ToString();
            this.yScale.Text = param[1].ToString();
            this.zScale.Text = param[2].ToString();
            param = paramHandler.getGyroOffsets();
            this.gyroXOff.Text = param[0].ToString();
            this.gyroYOff.Text = param[1].ToString();
            this.gyroZOff.Text = param[2].ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double[] newParams = { Double.Parse(xOff.Text), Double.Parse(yOff.Text), Double.Parse(zOff.Text), Double.Parse(xScale.Text), Double.Parse(yScale.Text), Double.Parse(zScale.Text), Double.Parse(gyroXOff.Text), Double.Parse(gyroYOff.Text), Double.Parse(gyroZOff.Text) };
                paramHandler.setParams(newParams);
                paramHandler.saveDocument();
            }
            catch (Exception) {
                MessageBox.Show("Please fill the textfield with numbers only. Decimal separator is Comma");
            }
            
        }
    }
}
