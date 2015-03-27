#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using MonoGame.Framework;
using engine;
#endregion

namespace FallingBlockGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FallingBlockGame : Game
    {
        public const int HEIGTH = 700;
        public const int WIDTH = 600;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private RenderManager renderManager;
        public RenderManager RenderManager { get { return renderManager; } }

        private GameStateManager gameStateManager;
        public GameStateManager GameStateManager { get { return gameStateManager; } }

        public FallingBlockGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = HEIGTH;
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.ApplyChanges();
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            renderManager = new RenderManager(new Graphics(GraphicsDevice));
            
            gameStateManager = new GameStateManager((Game)this);
            gameStateManager.Add("gameplay", new GameplayState(this));
            gameStateManager.ChangeState("gameplay");

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Update();
            gameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            gameStateManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
