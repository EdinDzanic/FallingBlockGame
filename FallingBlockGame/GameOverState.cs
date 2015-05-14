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
    public class GameOverState : IGameState
    {
        public bool IsActive { get; set; }

        public Dictionary<string, IGameState> ChildStates { get; set; }

        private const string GAME_OVER = "Game over! Press any key to continue...";
        private Label label;
        private FallingBlockGame game;

        public GameOverState(FallingBlockGame game)
        {
            this.game = game;
            label = new Label(GAME_OVER);
            label.Font = game.Content.Load<SpriteFont>("test");
            label.TextColor = Color.White;
            label.Position = new Vector2(0, 0);
            Vector2 correction = label.Font.MeasureString(GAME_OVER);
            label.Padding = new Vector2(
                FallingBlockGame.WIDTH / 2 - correction.X / 2,
                FallingBlockGame.HEIGTH / 2 - correction.Y / 2);
            label.BackgroundImage = game.Content.Load<Texture2D>("background");
            label.BackgroundColor = Color.Black;
            label.Width = FallingBlockGame.WIDTH;
            label.Heigth = FallingBlockGame.HEIGTH;

            IsActive = false;
            ChildStates = new Dictionary<string, IGameState>();
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
