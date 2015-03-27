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
        }
        
        public void Update(GameTime gameTime)
        {

            if (!gameLogic.IsGameOver) 
            {
                Movement move = Movement.None;
                if (InputManager.KeyPressed(Keys.Down))
                    move = Movement.Down;
                else if (InputManager.KeyPressed(Keys.Left))
                    move = Movement.Left;
                else if (InputManager.KeyPressed(Keys.Right))
                    move = Movement.Right;
                else if (InputManager.KeyPressed(Keys.Up))
                    move = Movement.Rotate;

                gameLogic.Update(move);
            }
            else
            {
                game.Exit();
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
