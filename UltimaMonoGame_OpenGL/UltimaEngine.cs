/***************************************************************************
 *   UltimaEngine.cs
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region Usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;
using UltimaXNA.Configuration;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Core.Input;
using UltimaXNA.Core.Network;
using UltimaXNA.Ultima;
using UltimaXNA.Ultima.IO;
using UltimaXNA.Ultima.IO.FontsNew;
using UltimaXNA.Ultima.IO.FontsOld;
using UltimaXNA.Ultima.Login;
using UltimaXNA.Ultima.UI;
#endregion

namespace UltimaXNA
{
    internal class UltimaEngine : Game
    {
        public static double TotalMs = 0d;

        public UltimaEngine()
        {
            InitializeGraphicsDevice();
        }

        #region Active & Queued Models
        private AUltimaModel _model;
        private AUltimaModel _queuedModel;

        public AUltimaModel QueuedModel
        {
            get { return _queuedModel; }
            set
            {
                if(_queuedModel != null)
                {
                    _queuedModel.Dispose();
                    _queuedModel = null;
                }
                _queuedModel = value;

                if(_queuedModel != null)
                {
                    _queuedModel.Initialize();
                }
            }
        }

        public AUltimaModel ActiveModel
        {
            get { return _model; }
            set
            {
                if(_model != null)
                {
                    _model.Dispose();
                    _model = null;
                }
                _model = value;
                if(_model != null)
                {
                    _model.Initialize();
                }
            }
        }

        public void ActivateQueuedModel()
        {
            if (_queuedModel == null) return;
            ActiveModel = QueuedModel;
            _queuedModel = null;
        }
        #endregion

        protected bool IsMinimized
        {
            get
            {
                
                //Get out top level form via the handle.
                var mainForm = Control.FromHandle(Window.Handle);
                
                //If we are minimized don't waste time trying to draw, and avoid crash on resume.
                //return ((Form)mainForm).WindowState == FormWindowState.Minimized;
                return false;
            }
        }


        protected InputManager Input
        {
            get;
            private set;
        }

        protected UserInterfaceService UserInterface
        {
            get;
            private set;
        }

        protected INetworkClient Network
        {
            get;
            private set;
        }

        protected override void Initialize()
        {
            Content.RootDirectory = "Content";

            // Create all the services we need.
            UltimaServices.Register<SpriteBatch3D>(new SpriteBatch3D(this));
            UltimaServices.Register<SpriteBatchUI>(new SpriteBatchUI(this));
            Network = UltimaServices.Register<INetworkClient>(new NetworkClient());
            Input = UltimaServices.Register<InputManager>(new InputManager(Window.Handle));
            UserInterface = UltimaServices.Register<UserInterfaceService>(new UserInterfaceService());

            // Make sure we have a UO installation before loading IO.
            if (!FileManager.IsUODataPresent) return;
            // Initialize and load data
            AnimData.Initialize();
            Animations.Initialize(GraphicsDevice);
            ArtData.Initialize(GraphicsDevice);

            ASCIIText.Initialize(GraphicsDevice);
            TextUni.Initialize(GraphicsDevice);

            GumpData.Initialize(GraphicsDevice);
            HuesXNA.Initialize(GraphicsDevice);
            TexmapData.Initialize(GraphicsDevice);
            StringData.LoadStringList("enu");
            SkillsData.Initialize();
            GraphicsDevice.Textures[1] = HuesXNA.HueTexture0;
            GraphicsDevice.Textures[2] = HuesXNA.HueTexture1;

            EngineVars.EngineRunning = true;
            EngineVars.InWorld = false;

            ActiveModel = new LoginModel();
        }

        protected override void Update(GameTime gameTime)
        {
            IsFixedTimeStep = Settings.Game.IsFixedTimeStep;

            if(!EngineVars.EngineRunning)
            {
                Settings.Save();
                Exit();
            }
            else
            {
                var totalMs = gameTime.TotalGameTime.TotalMilliseconds;
                var frameMs = gameTime.ElapsedGameTime.TotalMilliseconds;

                TotalMs = totalMs;
                Input.Update(totalMs, frameMs);
                UserInterface.Update(totalMs, frameMs);
                Network.Slice();
                ActiveModel.Update(totalMs, frameMs);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (IsMinimized) return;
            SpriteBatch3D.ResetZ();
            GraphicsDevice.Clear(Color.Black);
            ActiveModel.GetView()
                .Draw(gameTime.ElapsedGameTime.TotalMilliseconds);
            UserInterface.Draw(gameTime.ElapsedGameTime.TotalMilliseconds);

            EngineVars.UpdateFPS(gameTime.ElapsedGameTime.TotalMilliseconds);
            Window.Title =
                Settings.Debug.ShowFps ?
                    string.Format("UltimaXNA FPS:{0}", EngineVars.UpdateFPS(gameTime.ElapsedGameTime.TotalMilliseconds)) :
                    "UltimaXNA";
        }

        // Some settings to designate a screen size and fps limit.
        private void InitializeGraphicsDevice()
        {
            Resolution resolution = Settings.Game.Resolution;
            GraphicsDeviceManager graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = resolution.Width,
                PreferredBackBufferHeight = resolution.Height,
                SynchronizeWithVerticalRetrace = Settings.Game.IsVSyncEnabled
            };

            graphicsDeviceManager.PreparingDeviceSettings += OnPreparingDeviceSettings;

            IsFixedTimeStep = false;
            graphicsDeviceManager.ApplyChanges();
        }

        private static void OnPreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
        }
    }
}