using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using engine;

namespace FallingBlockGame
{
    public class PauseState : IGameState
    {
        private const string PAUSE_MESSAGE = "Game paused";
        
        public bool IsActive { get; set; }
        public Dictionary<string, IGameState> ChildStates { get; set; }
        
        private Label label;
        private FallingBlockGame game;
        private bool firstTime;

        public PauseState(FallingBlockGame game)
        {
            label = new Label(PAUSE_MESSAGE);
            this.game = game;

            label.Font = game.Content.Load<SpriteFont>("test");
            label.TextColor = Color.White;
            label.Position = new Vector2(0, 0);
            Vector2 correction = label.Font.MeasureString(PAUSE_MESSAGE);
            label.Padding = new Vector2(
                FallingBlockGame.WIDTH / 2 - correction.X / 2,
                FallingBlockGame.HEIGTH / 2 - correction.Y / 2);
            label.BackgroundColor = Color.Black;
            label.BackgroundImage = game.Content.Load<Texture2D>("background");
            label.Width = FallingBlockGame.WIDTH;
            label.Heigth = FallingBlockGame.HEIGTH; 

            IsActive = false;
            firstTime = false;
            ChildStates = new Dictionary<string, IGameState>();
        }
        
        public void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Keys.Space) && firstTime)
            {
                IsActive = false;
                firstTime = false;
            }
            else if(IsActive)
            {
                firstTime = true;
            }
        }

        public void Draw(GameTime gameTime)
        {
            label.Draw(game.RenderManager.Graphics);
        }
    }
}
