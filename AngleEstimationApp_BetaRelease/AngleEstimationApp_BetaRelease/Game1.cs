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

namespace AngleEstimationApp_BetaRelease
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
        Filter filter;
        Quaternion AuxFrame = new Quaternion((float)0, 0, (float)0, (float)1);
        Texture2D background;
        Rectangle mainFrame;
        int algorithm;
        int obsAlg;
        AcquisitionThread acq; 
        Thread workerThread;

        Plot plotForm;
        private bool trend;


        public Game(int a,int b,int port,bool trend)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.algorithm = a;
            this.obsAlg = b;
            this.COMport = "PL=PL_001{PN=COM"+ (port+1)+ ", SENDMODE=B}";
            switch (algorithm)
            {
                case 0:
                    filter = new ComplementaryFilter();
                    break;
                case 1:
                    filter = new QuaternionCF();
                    break;
                case 2:
                    filter = new KalmanFilter();
                    break;
                default:
                    filter = new KalmanFilter();
                    break;
            }
            filter.loadParameters();
            filter.setObsMethod(obsAlg);
            this.Exiting += new EventHandler(Game1_Exiting);
            this.trend = trend;
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
            
            //Connect to iNemo board
            acq = new AcquisitionThread(COMport, dev, this);
            workerThread = new Thread(acq.DoWork);
            workerThread.Start();
            plotForm = new Plot();
            if(trend)
                plotForm.Show();
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
            if (algorithm == 0)         //Complementary filter
            {
                double[] anglesFiltered = new double[3];

                anglesFiltered[0] = filter.getFilteredAngles()[0, 0];
                anglesFiltered[1] = filter.getFilteredAngles()[1, 0];
                anglesFiltered[2] = filter.getFilteredAngles()[2, 0];
                Matrix m = Matrix.CreateRotationZ((float)(anglesFiltered[2] * Math.PI / 180));
                Matrix m1 = Matrix.CreateRotationY(-(float)(anglesFiltered[1] * Math.PI / 180));
                Matrix m2 = Matrix.CreateRotationX((float)(anglesFiltered[0] * Math.PI / 180));

                Matrix m3 = m * m1 * m2;

                double qw = Math.Sqrt(1 + m3.M11 * m3.M11 + m3.M22 * m3.M22 + m3.M33 * m3.M33) / 2;
                double q1 = (m3.M32 - m3.M23) / (4 * qw);
                double q2 = (m3.M13 - m3.M31) / (4 * qw);
                double q3 = (m3.M21 - m3.M12) / (4 * qw);
                worldMatrix = Matrix.CreateFromQuaternion(AuxFrame * new Quaternion((float)q1, (float)q2, (float)q3, (float)qw));
            }
            else
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) AuxFrame = new Quaternion(-(float)filter.getFilteredQuaternions()[1, 0], -(float)filter.getFilteredQuaternions()[2, 0], -(float)filter.getFilteredQuaternions()[3, 0], (float)filter.getFilteredQuaternions()[0, 0]);
            }
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
            if (algorithm == 0)         //Complementary filter
            {
                double[] anglesFiltered = new double[3];
               
                anglesFiltered[0] = filter.getFilteredAngles()[0, 0];
                anglesFiltered[1] = filter.getFilteredAngles()[1, 0];
                anglesFiltered[2] = filter.getFilteredAngles()[2, 0];
                MatrixLibrary.Matrix anglesMatrix=new MatrixLibrary.Matrix(3,1);
                anglesMatrix[0,0]=anglesFiltered[0];
                anglesMatrix[1,0]=anglesFiltered[1];
                anglesMatrix[2,0]=anglesFiltered[2];
                MatrixLibrary.Matrix q= MyQuaternion.getQuaternionFromAngles(anglesMatrix);
                worldMatrix = Matrix.CreateFromQuaternion(AuxFrame * new Quaternion((float)q[1, 0], -(float)q[2, 0], (float)q[3, 0], -(float)q[0, 0]));
                //worldMatrix = Matrix.CreateFromYawPitchRoll((float)(anglesFiltered[1] * Math.PI / 180), -(float)(anglesFiltered[0] * Math.PI / 180), -(float)(anglesFiltered[2] * Math.PI / 180));
            }
            else {
                float q1=(float)filter.getFilteredQuaternions()[1, 0];
                float q2=(float)filter.getFilteredQuaternions()[2, 0];
                float q3=(float)filter.getFilteredQuaternions()[3, 0];
                float q0=(float)filter.getFilteredQuaternions()[0, 0];
                if(trend)
                    plotForm.AddDataToGraph(q0,q1,q2,q3);
                worldMatrix = Matrix.CreateFromQuaternion(AuxFrame * new Quaternion(q1,q2,q3,q0));
            }
                
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
            filter.filterStep(data.Gyroscope.X, data.Gyroscope.Y, data.Gyroscope.Z,
                        data.Accelometer.X, data.Accelometer.Y, data.Accelometer.Z,
                        data.Magnetometer.X, data.Magnetometer.Y, data.Magnetometer.Z);
        }

    }
}