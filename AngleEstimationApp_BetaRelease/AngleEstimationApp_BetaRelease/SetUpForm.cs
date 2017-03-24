using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna;
using System.Windows.Forms;

namespace AngleEstimationApp_BetaRelease
{
    public partial class SetUpForm : Form
    {
        private int selectedAlgorithm;
        private int selectedObsAlg;
        private int selectedPort;
        private bool trend;
        private ParametersForm paramForm;
        public SetUpForm()
        {
            InitializeComponent();
            algorithmBox_SelectedIndexChanged(null, null);
            selectedAlgorithm = algorithmBox.SelectedIndex;
            selectedObsAlg = obsBox.SelectedIndex;
            selectedPort = portNumber.SelectedIndex;
            portNumber.SelectedIndex = 4;
            obsBox.SelectedIndex = 0;
            algorithmBox.SelectedIndex = 0;
            Controls.Add(trendOn);
            Controls.Add(trendOff);
            trendOn.Checked = false;
            trendOff.Checked = true;
            trend = false;
        }

        private void portNumber_SelectedItemChanged(object sender, EventArgs e)
        {
            selectedPort = portNumber.SelectedIndex;
        }

        private void algorithmBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (algorithmBox.SelectedIndex == 0)
            {
                this.obsBox.Enabled = false;
                this.trendOff.Checked = true;
                this.trendOn.Checked = false;
                this.trendOff.Enabled = false;
                this.trendOn.Enabled = false;
            }
            else {
                this.obsBox.Enabled = true;
                this.trendOff.Enabled = true;
                this.trendOn.Enabled = true;
            }
            selectedAlgorithm = algorithmBox.SelectedIndex;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (trendOn.Checked)
                trend = true;
            GameRunner gr = new GameRunner(selectedAlgorithm,selectedObsAlg,selectedPort,trend);
            Thread oThread = new Thread(new ThreadStart(gr.runGame));
            // Start the thread
            oThread.Start();
        }

        private void obsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedObsAlg = obsBox.SelectedIndex;
        }

        private void setParam_Click(object sender, EventArgs e)
        {
            paramForm = new ParametersForm();
            paramForm.Show();
        }

        private void SetUpForm_Load(object sender, EventArgs e)
        {

        }
    }

    public class GameRunner
    {
        private int algorithm;
        private int obsAlg;
        private int port;
        private bool trend;

        public GameRunner(int a,int b,int port,bool trend)
        {
            this.algorithm = a;
            this.obsAlg = b;
            this.port = port;
            this.trend = trend;
        }
        public void runGame()
        {
            try
            {
                using (Game game = new Game(algorithm,obsAlg,port,trend))
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
