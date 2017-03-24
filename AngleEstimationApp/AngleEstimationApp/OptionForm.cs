using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna;
using System.Threading;

namespace AngleEstimationApp
{
    public partial class OptionsForm : Form
    {

        public Parameters param=new Parameters();
        //int port;
        int alg;
        StarterForm st;
        bool err;

        public OptionsForm(StarterForm st,int port,int alg)
        {
            this.st = st;
            this.param.porta= port;
            this.alg = alg;
            InitializeComponent();
            if (alg == 3)
            {
                this.txtAHRS1.Enabled = false;
                this.txtAHRS2.Enabled = false;
            }
            if (alg == 1)
            {
                this.txtAHRS1.Enabled = false;
                this.txtAHRS2.Enabled = false;
                this.txtvarx.Enabled = false;
                this.txtvary.Enabled = false;
                this.txtvarz.Enabled = false;
            }
            if (alg==2)
            {
                this.txtoffx.Enabled = false;
                this.txtoffy.Enabled = false;
                this.txtoffz.Enabled = false;
                this.txtvarx.Enabled = false;
                this.txtvary.Enabled = false;
                this.txtvarz.Enabled = false;
                this.txtOffmagnx.Enabled = false;
                this.txtOffmagny.Enabled = false;
                this.txtOffmagnz.Enabled = false;
                this.txtSfmagnx.Enabled = false;
                this.txtSfmagny.Enabled = false;
                this.txtSfmagnz.Enabled = false;
            }
        }

        

        private void buttonSalva_Click(object sender, EventArgs e)
        {
            if (txtAHRS1.Text.Equals(""))
            {
                txtAHRS1.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.AHRSpar1 = double.Parse(txtAHRS1.Text);

            if (txtAHRS2.Text.Equals(""))
            {
                txtAHRS2.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.AHRSpar2 = double.Parse(txtAHRS2.Text);

            if (txtfreq.Text.Equals(""))
            {
                txtfreq.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.freq = int.Parse(txtfreq.Text);

            if (txtoffx.Text.Equals(""))
            {
                txtoffx.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.gyroOffX = double.Parse(txtoffx.Text);

            if (txtoffy.Text.Equals(""))
            {
                txtoffy.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.gyroOffY = double.Parse(txtoffy.Text);

            if (txtoffz.Text.Equals(""))
            {
                txtoffz.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.gyroOffZ = double.Parse(txtoffz.Text);

            if (txtvarx.Text.Equals(""))
            {
                txtvarx.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.gyroVarX = double.Parse(txtvarx.Text);

            if (txtvary.Text.Equals(""))
            {
                txtvary.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.gyroVarY = double.Parse(txtvary.Text);

            if (txtvarz.Text.Equals(""))
            {
                txtvarz.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.gyroVarZ = double.Parse(txtvarz.Text);

            if (txtOffmagnx.Text.Equals(""))
            {
                txtOffmagnx.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.magnOffX = double.Parse(txtOffmagnx.Text);

            if (txtOffmagny.Text.Equals(""))
            {
                txtOffmagny.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.magnOffY = double.Parse(txtOffmagny.Text);

            if (txtOffmagnz.Text.Equals(""))
            {
                txtOffmagnz.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.magnOffZ = double.Parse(txtOffmagnz.Text);

            if (txtSfmagnx.Text.Equals(""))
            {
                txtSfmagnx.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.magnSFX = double.Parse(txtSfmagnx.Text);

            if (txtSfmagny.Text.Equals(""))
            {
                txtSfmagny.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.magnSFY = double.Parse(txtSfmagny.Text);

            if (txtSfmagnz.Text.Equals(""))
            {
                txtSfmagnz.BackColor = Color.Coral;
                err = true;
            }
            else
                this.param.magnSFZ = double.Parse(txtSfmagnz.Text);

            if (err == false)
            {
                st.setParam(this.param);
                this.Close();
            }
        }

        private void txtvarx_TextChanged(object sender, EventArgs e)
        {
            txtvarx.BackColor = Color.White;
            err = false;
        }

        private void txtvary_TextChanged(object sender, EventArgs e)
        {
            txtvary.BackColor = Color.White;
            err = false;
        }

        private void txtvarz_TextChanged(object sender, EventArgs e)
        {
            txtvarz.BackColor = Color.White;
            err = false;
        }

        private void txtoffx_TextChanged(object sender, EventArgs e)
        {
            txtoffx.BackColor = Color.White;
            err = false;
        }

        private void txtoffy_TextChanged(object sender, EventArgs e)
        {
            txtoffy.BackColor = Color.White;
            err = false;
        }

        private void txtoffz_TextChanged(object sender, EventArgs e)
        {
            txtoffz.BackColor = Color.White;
            err = false;
        }

        private void txtAHRS1_TextChanged(object sender, EventArgs e)
        {
            txtAHRS1.BackColor = Color.White;
            err = false;
        }

        private void txtAHRS2_TextChanged(object sender, EventArgs e)
        {
            txtAHRS2.BackColor = Color.White;
            err = false;
        }

        private void txtfreq_TextChanged(object sender, EventArgs e)
        {
            txtfreq.BackColor = Color.White;
            err = false;
        }

        private void txtOffmagnx_TextChanged(object sender, EventArgs e)
        {
            txtOffmagnx.BackColor = Color.White;
            err = false;
        }

        private void txtOffmagny_TextChanged(object sender, EventArgs e)
        {
            txtOffmagny.BackColor = Color.White;
            err = false;
        }

        private void txtOffmagnz_TextChanged(object sender, EventArgs e)
        {
            txtOffmagnz.BackColor = Color.White;
            err = false;
        }

        private void txtSfmagnx_TextChanged(object sender, EventArgs e)
        {
            txtSfmagnx.BackColor = Color.White;
            err = false;
        }

        private void txtSfmagny_TextChanged(object sender, EventArgs e)
        {
            txtSfmagny.BackColor = Color.White;
            err = false;
        }

        private void txtSfmagnz_TextChanged(object sender, EventArgs e)
        {
            txtSfmagnz.BackColor = Color.White;
            err = false;
        }

        private void Standard_Click(object sender, EventArgs e)
        {
            if (alg == 1)
            {
                this.param.freq = 100;
                this.param.gyroOffX = -3.6982;
                this.param.gyroOffY = -3.3570;
                this.param.gyroOffZ = -2.5909;
                this.param.magnOffX = 0.46;
                this.param.magnOffY = 0;
                this.param.magnOffZ = -0.13;
                this.param.magnSFX = 1.2;
                this.param.magnSFY = 1;
                this.param.magnSFZ = 1;
            }
            if (alg == 3)
            {
                this.param.freq = 100;
                this.param.gyroOffX = -3.6982;
                this.param.gyroOffY = -3.3570;
                this.param.gyroOffZ = -2.5909;
                this.param.gyroVarX = 0.5647;
                this.param.gyroVarY = 0.5674;
                this.param.gyroVarZ = 0.5394;
                //this.param.magnOffX = 0.277;
                //this.param.magnOffY = -0.0416;
                //this.param.magnOffZ = -0.01;
                //this.param.magnSFX=1.0881;
                //this.param.magnSFY=1.0284;
                //this.param.magnSFZ = 1;
                this.param.magnOffX = 0.46;
                this.param.magnOffY = 0;
                this.param.magnOffZ = -0.13;
                this.param.magnSFX = 1.2;
                this.param.magnSFY = 1;
                this.param.magnSFZ = 1;
            }
            else
            {
                this.param.freq = 100;
                this.param.AHRSpar1=10;
                this.param.AHRSpar2 = 0.001;
            }


            GameRunner gr = new GameRunner(param,alg);
            Thread oThread = new Thread(new ThreadStart(gr.runGame));
            // Start the thread
            oThread.Start();

        }

       
    }

    public class GameRunner
    {
        Parameters param;
        int alg;

        public GameRunner(Parameters param, int alg)
        {
            this.param = param;
            this.alg = alg;
        }
        public void runGame()
        {
            try
            {
                using (Game game = new Game(param,alg))
                {
                    game.Run();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
    }




}
