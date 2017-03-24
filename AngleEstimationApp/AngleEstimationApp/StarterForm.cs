using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AngleEstimationApp
{
    public partial class StarterForm : Form
    {
        private Parameters param;
        OptionsForm opt;

        int chosenAlgorithm;
        /*
         * 1 --> Complementary filter
         * 2 --> AHRS
         * 3 --> Kalman filter
         *
         * */
        public StarterForm()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CompButton_CheckedChanged(object sender, EventArgs e)
        {
            this.chosenAlgorithm = 1;
            if (!(this.ComPort.Text.Equals(" ") || this.ComPort.Equals("")))
                this.StartButton.Enabled = true; 
            
        }

        private void AHRSButton_CheckedChanged(object sender, EventArgs e)
        {
            this.chosenAlgorithm = 2;
            if (!(this.ComPort.Text.Equals(" ") || this.ComPort.Equals("")))
                this.StartButton.Enabled = true;
            
        }

        private void KalmanButton_CheckedChanged(object sender, EventArgs e)
        {
            this.chosenAlgorithm = 3;
            if (!(this.ComPort.Text.Equals(" ") || this.ComPort.Equals("")))
                this.StartButton.Enabled = true; 
           
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                int.Parse(this.ComPort.Text);
            }
            catch (FormatException)
            {
                this.ErrorLabel.Visible = true;
                this.ErrorLabel.Text = "Inserire una porta valida";
            }
            //if (int.Parse(this.ComPort.Text))

            //using (Game game = new Game("5","0.1","4"))//args[1], args[2],args[3]))
            //{
            //    game.Run();
            //}
            if (!(this.ErrorLabel.Visible))
            {
                opt = new OptionsForm(this,int.Parse(this.ComPort.Text),this.chosenAlgorithm);
                this.opt.ShowDialog();
            }
        }

        private void ComPort_TextChanged(object sender, EventArgs e)
        {
            this.ErrorLabel.Visible = false;
        }




        internal void setParam(Parameters paramaters)
        {
            this.param = paramaters;
        }
    }
}
