using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlLibrary.MKI062V2;
using System.Timers;

namespace AngleEstimationApp
{
    class AcquisitionThread
    {
        private volatile bool _shouldStop;
        public static bool tic = false;
        INEMO2_Device device;
        Game connected_game;
        //Timer Clock;
        //double time;
        //DateTime dt;
        INEMO2_FrameData data=new INEMO2_FrameData();

        public AcquisitionThread(string port,INEMO2_Device device, Game g)
        {
            this.connected_game = g;
            this.device = device;
            this.device.Connect(port);
            this.device.Start(0,50,0);
            this.device.Led_ON();
            //Clock = new Timer();
            //Clock.Interval = 20;
            //Clock.Start();
            //Clock.Elapsed += new ElapsedEventHandler(Tic_Handler);
            //dt = DateTime.Now;
        }
        public void DoWork()
        {
            while (!_shouldStop)
            {
                //if (tic)
                //{
                    //data = new INEMO2_FrameData();
                    
                    device.GetSample(ref data);
                    //System.Console.WriteLine("AAAA " + data.Accelometer.Z);
                    //TimeSpan duration = DateTime.Now - dt;
                    //dt = dt + duration;
                    //time = duration.Milliseconds;
                    connected_game.PacketReceived(data);
                    System.Threading.Thread.Sleep((int)(connected_game.dt*1000));
                    
                    //tic = false;
                //}
            }
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }

        //private static void Tic_Handler(object source, ElapsedEventArgs e)
        //{
        //    tic = true;
        //}



        internal void Disconnect()
        {
            this.device.Disconnect();
        }
    }
}
