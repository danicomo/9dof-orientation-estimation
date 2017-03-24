using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using ControlLibrary.MKI062V2;
using System.Threading;
using RPY;

namespace AngleEstimationApp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        private string COMport; 
        
        INEMO2_Device dev= new INEMO2_Device();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Matrix worldMatrix;
        Matrix cameraMatrix;
        Matrix projectionMatrix;
        BasicEffect cubeEffect;
        BasicShape cube = new BasicShape(new Vector3(7, 5, 1), new Vector3(0, 0, 0));
        AHRSalgorithm AHRS;
        KalmanFilter KalmanFilter;
        ComplementaryFilter ComplementaryFilter;
        Quaternion AuxFrame = new Quaternion(0, 0, 0, 1);
        Texture2D background;
        Rectangle mainFrame;
        int alg;
        AcquisitionThread acq; 
        Thread workerThread;

        public double dt;


        public Game(Parameters param,int alg)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.alg = alg;
            dt = 1.0 / param.freq;
            this.COMport = "PL=PL_001{PN=COM"+ param.porta+ ", SENDMODE=B}";

            if (alg == 1)         //Complementary filter
                ComplementaryFilter = new ComplementaryFilter(param);
            if (alg == 2)         //AHRS
            {
                AHRS = new AHRSalgorithm();
                AHRS.setParamaters(param.AHRSpar1, param.AHRSpar2,param.freq);
            }
            if (alg == 3)       //Kalman filter
                KalmanFilter = new KalmanFilter(param);

            this.Exiting += new EventHandler(Game1_Exiting);
            
        }

        void Game1_Exiting(object sender, EventArgs e)
        {
            acq.Disconnect();
            Environment.Exit(0);
        }


        /// <summary>
        /// Allows the game to perform any initialization
        /// it needs to before starting to run.
        /// This is where it can query for any required 
        /// services and load any non-graphic
        /// related content.  Calling base. Initialize will
        /// enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            this.BeginRun();
            initializeWorld();
        
            acq=new AcquisitionThread(COMport,dev,this);         //connette iNEMO e prepara per acquisizione
            workerThread = new Thread(acq.DoWork);
            workerThread.Start();
            //imutag = new IMUtagv1p0serial(IMUtagCOMport, settings);  //connect the iNEMO
            //imutag.packetReceived += new IMUtagv1p0serial.onDataPacketReceived(imutag_packetReceived);
        }

        /// <summary>
        /// LoadContent will be called once 
        /// per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Load the background content.
            background = Content.Load<Texture2D>("Textures\\myback");
            // Set the rectangle parameters.
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            
            cube.shapeTexture = Content.Load<Texture2D>("Textures\\mytext");
        }

        /// <summary>
        /// UnloadContent will be called once per game
        /// and is the place to unload all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic 
        /// such as updating the world,
        /// checking for collisions, 
        /// gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();
            if (Mouse.GetState().RightButton == ButtonState.Pressed) graphics.ToggleFullScreen();
            if (alg == 1)         //Complementary filter
            {
                double[] anglesFiltered = ComplementaryFilter.getAnglesFiltered();
                Matrix m = Matrix.CreateRotationZ((float)(anglesFiltered[2] * Math.PI / 180));
                Matrix m1 = Matrix.CreateRotationY(-(float)(anglesFiltered[1] * Math.PI / 180));
                Matrix m2 = Matrix.CreateRotationX((float)(anglesFiltered[0] * Math.PI / 180));

                Matrix m3 = m * m1 * m2;

                double qw = Math.Sqrt(1 + m3.M11 * m3.M11 + m3.M22 * m3.M22 + m3.M33 * m3.M33) / 2;
                double q1 = (m3.M32 - m3.M23) / (4 * qw);
                double q2 = (m3.M13 - m3.M31) / (4 * qw);
                double q3 = (m3.M21 - m3.M12) / (4 * qw);
                worldMatrix = Matrix.CreateFromQuaternion(AuxFrame * new Quaternion((float)q1, (float)q2, (float)q3, (float)qw));

                //worldMatrix = Matrix.CreateFromYawPitchRoll((float)(anglesFiltered[1] * Math.PI / 180), -(float)(anglesFiltered[0] * Math.PI / 180), -(float)(anglesFiltered[2] * Math.PI / 180));
            }
            if (alg == 2)         //AHRS
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) AuxFrame = new Quaternion((float)-AHRS.SEq_2, (float)-AHRS.SEq_3, (float)-AHRS.SEq_4, (float)AHRS.SEq_1);
            if (alg == 3)       //Kalman filter
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) AuxFrame = new Quaternion((float)-KalmanFilter.q_filt2, (float)-KalmanFilter.q_filt3, (float)-KalmanFilter.q_filt4, (float)KalmanFilter.q_filt1);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">
        /// Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the background.
            // Start building the sprite.
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            // Draw the background.
            spriteBatch.Draw(background, mainFrame, Color.White);
            // End building the sprite.
            spriteBatch.End();

            cubeEffect.Begin();
            if (alg == 1)         //Complementary filter
            {
                double[] anglesFiltered = ComplementaryFilter.getAnglesFiltered();
                //Console.Out.Write(anglesFiltered[0] + " " + anglesFiltered[1] + " " + anglesFiltered[2]+"\n");
                worldMatrix = Matrix.CreateFromYawPitchRoll((float)(anglesFiltered[1] * Math.PI / 180), -(float)(anglesFiltered[0] * Math.PI / 180), -(float)(anglesFiltered[2] * Math.PI / 180));
            }
            if (alg == 2)         //AHRS
                worldMatrix = Matrix.CreateFromQuaternion(AuxFrame * new Quaternion((float)AHRS.SEq_2, (float)AHRS.SEq_3, (float)AHRS.SEq_4, (float)AHRS.SEq_1));
            if (alg == 3)       //Kalman filter
                worldMatrix = Matrix.CreateFromQuaternion(AuxFrame * new Quaternion((float)-KalmanFilter.q_filt2, (float)-KalmanFilter.q_filt3, (float)KalmanFilter.q_filt4, (float)KalmanFilter.q_filt1));
            cubeEffect.World = worldMatrix;
            foreach (EffectPass pass in cubeEffect.CurrentTechnique.Passes)
            {
                pass.Begin();
                cubeEffect.Texture = cube.shapeTexture;
                cube.RenderShape(GraphicsDevice);
                pass.End();
            }
            cubeEffect.End();
            base.Draw(gameTime);
        }

        public void initializeWorld()
        {
            cameraMatrix = Matrix.CreateLookAt(new Vector3(0, -30, 1), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Window.ClientBounds.Width / Window.ClientBounds.Height, 1.0f, 50.0f);
            worldMatrix = Matrix.Identity;
            cubeEffect = new BasicEffect(GraphicsDevice, null);
            cubeEffect.World = worldMatrix;
            cubeEffect.View = cameraMatrix;
            cubeEffect.Projection = projectionMatrix;
            cubeEffect.TextureEnabled = true;
        }

        public void PacketReceived(INEMO2_FrameData data)
        {
            
            if (alg == 1)         //Complementary filter
                ComplementaryFilter.complementaryFiltering(data.Gyroscope.X, data.Gyroscope.Y, data.Gyroscope.Z,
                        data.Accelometer.X, data.Accelometer.Y, data.Accelometer.Z,
                        data.Magnetometer.X, data.Magnetometer.Y, data.Magnetometer.Z);
            if (alg == 2)         //AHRS
                AHRS.update(-data.Gyroscope.X, -data.Gyroscope.Y, -data.Gyroscope.Z,
                        -data.Accelometer.X, data.Accelometer.Y, -data.Accelometer.Z,
                        data.Magnetometer.X, data.Magnetometer.Y, -data.Magnetometer.Z);   // update filter with sensor data
            if (alg == 3)       //Kalman filter      
                KalmanFilter.filter_step(data.Gyroscope.X, data.Gyroscope.Y, data.Gyroscope.Z,
                        data.Accelometer.X, data.Accelometer.Y, data.Accelometer.Z,
                        data.Magnetometer.X, data.Magnetometer.Y, data.Magnetometer.Z);
        }

    }
}