using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using engine;

namespace FallingBlockGame
{
    public class GameOverState : IGameState
    {
        public bool IsActive { get; set; }

        public List<IGameState> ChildStates { get; set; }

        private const string GAME_OVER = "Game over";
        private Label label;
        private FallingBlockGame game;

        public GameOverState(FallingBlockGame game)
        {
            this.game = game;
            label = new Label(GAME_OVER);
            label.Font = game.Content.Load<SpriteFont>("test");
            label.TextColor = Color.Red;
            label.Position = new Vector2(0, 0);
            label.BackgroundImage = game.Content.Load<Texture2D>("background");
            label.BackgroundColor = Color.Gray;
            label.Width = FallingBlockGame.WIDTH;
            label.Heigth = FallingBlockGame.HEIGTH;

            IsActive = false;
            ChildStates = new List<IGameState>();
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime)
        {
            label.Draw(game.RenderManager.Graphics);
        }
    }
}
