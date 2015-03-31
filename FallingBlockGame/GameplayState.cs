using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using engine;

namespace FallingBlockGame
{
    public class GameplayState : IGameState
    {
        private const int BLOCK_SIZE = 32;
        private const string BLOCK_TEXTURE_ATLAS = "blocks";

        private FallingBlockGame game;
        private FieldGameObjectBuilder fieldGameObjectBuilder;
        private GameLogic gameLogic;

        public bool IsActive { get; set; }
        public List<IGameState> ChildStates { get; set; }

        private double timer;

        public GameplayState(FallingBlockGame game)
        {
            this.game = game;
            gameLogic = new GameLogic();

            IsActive = false;

            Texture2D texture = game.Content.Load<Texture2D>(BLOCK_TEXTURE_ATLAS);
            TextureAtlas blockTextureAtlas = new TextureAtlas(texture, 2, 3);

            fieldGameObjectBuilder = new FieldGameObjectBuilder(
                gameLogic.Field,
                BLOCK_SIZE,
                blockTextureAtlas);

            ChildStates = new List<IGameState>();
            ChildStates.Add(new GameOverState(game));

            timer = gameLogic.UpdateRate;
        }
        
        public void Update(GameTime gameTime)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsedTime;

            if (!gameLogic.IsGameOver) 
            {
                Movement move = Movement.None;
                if (InputManager.KeyReleased(Keys.Down))
                    move = Movement.Down;
                else if (InputManager.KeyReleased(Keys.Left))
                    move = Movement.Left;
                else if (InputManager.KeyReleased(Keys.Right))
                    move = Movement.Right;
                else if (InputManager.KeyReleased(Keys.Up))
                    move = Movement.Rotate;

                if (timer < 0)
                {
                    timer = gameLogic.UpdateRate;
                    gameLogic.Update(Movement.Down);
                }

                gameLogic.Update(move);
            }
            else if (!ChildStates.First().IsActive)
            {
                ChildStates.First().IsActive = true;
            }
            else
            {
                if (InputManager.IsAnyKeyPressed())
                {
                    gameLogic.Restart();
                    ChildStates.First().IsActive = false;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.RenderManager.ClearScreen(Color.CornflowerBlue);
            List<GameObject> gameObjects = fieldGameObjectBuilder.CreateGameObjects();

            game.RenderManager.Draw(gameObjects);
        }

    }
}
