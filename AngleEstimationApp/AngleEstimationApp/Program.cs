using System;
using System.Threading;
using System.Windows.Forms;


namespace AngleEstimationApp
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///
        static void Main(string[] args)
        {
            //using (Game game = new Game("5","0.1","4"))//args[1], args[2],args[3]))
            //{
            //    game.Run();
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StarterForm());
            //StarterForm st = new StarterForm();
            //st.Show();
            //AcquisitionThread ac = new AcquisitionThread();
            //Thread workerThread = new Thread(ac.DoWork);
            //workerThread.Start();
            //System.Threading.Thread.Sleep(3000);

            //ac.RequestStop();
        }
    }
}

