using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ControlLibrary.MKI062V2;

namespace AngleEstimationApp
{
    public partial class Form3 : Form
    {
        INEMO2_Device device=new INEMO2_Device();
        INEMO2_FrameData data = new INEMO2_FrameData();
        int click = 0;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            device.Connect("PL=PL_001{PN=COM4,SENDMODE=B}");
            device.Start(0, 50, 0);
            device.Led_ON();
        }

        private void buttonAvvia_Click(object sender, EventArgs e)
        {
            
            click++;
        }
    }
}
