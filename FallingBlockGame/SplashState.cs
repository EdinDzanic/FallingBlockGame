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
    public class SplashState : IGameState
    {
        private const int TIME = 3000;
        private const string NEXT_STATE = "mainmenu";
        private const string LOGO = "logo";

        public bool IsActive { get; set; }
        public Dictionary<string, IGameState> ChildStates { get; set; }
        
        private int timer;

        private FallingBlockGame game;
        private GameStateManager gameStateManager;
        private Image logo;

        public SplashState(FallingBlockGame game, GameStateManager gameStateManager)
        {
            ChildStates = new Dictionary<string, IGameState>();
            IsActive = false;

            this.game = game;
            this.gameStateManager = gameStateManager;
            logo = new Image(game.Content, LOGO);
            logo.Heigth = 120;
            logo.Width = 300;
            logo.Position = new Vector2(150, 200);

            timer = TIME;
        }
        
        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.Milliseconds;
            
            if (timer < 0)
            {
                gameStateManager.ChangeState(NEXT_STATE);
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.RenderManager.ClearScreen(Color.Black);
            logo.Draw(game.RenderManager.Graphics);
        }
    }
}
