﻿using System;
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
        private const int MOVE_SPEED = 100;
        private const string BLOCK_TEXTURE_ATLAS = "blocks";

        private FallingBlockGame game;
        private FieldGameObjectBuilder fieldGameObjectBuilder;
        private NextShapeGameObjectBuilder nextShapeGameObjectBuilder;
        private GameLogic gameLogic;
        
        private List<List<GameObject>> nextShapes;

        public bool IsActive { get; set; }
        public Dictionary<string, IGameState> ChildStates { get; set; }

        private Label score;
        private Panel scorePanel;

        private double timer;
        private double keyDownTimer;

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

            ChildStates = new Dictionary<string, IGameState>();
            ChildStates.Add("gameover", new GameOverState(game));
            ChildStates.Add("pause", new PauseState(game));

            timer = gameLogic.UpdateRate;
            keyDownTimer = MOVE_SPEED;

            score = new Label("Score: 0");
            score.Font = game.Content.Load<SpriteFont>("test");
            score.TextColor = Color.Black;
            score.Position = new Vector2(400, 70);

            scorePanel = new Panel(game.GraphicsDevice);
            scorePanel.Position = new Vector2(400, 70);
            scorePanel.Heigth = 40;
            scorePanel.Width = 100;

            nextShapeGameObjectBuilder = new NextShapeGameObjectBuilder(
                new Vector2(400, 150),
                gameLogic.blockTypes,
                blockTextureAtlas,
                BLOCK_SIZE);

            nextShapes = new List<List<GameObject>>();
            for (int i = 0; i < gameLogic.blockTypes.Length; i++)
            {
                nextShapeGameObjectBuilder.ShapeType = i;
                nextShapes.Add(nextShapeGameObjectBuilder.CreateGameObjects());
            }
        }

        private Movement UpdateKeyHoldDownMovement(GameTime gameTime,  Movement movement)
        {
            keyDownTimer -= gameTime.ElapsedGameTime.Milliseconds;
            if (keyDownTimer < 0)
            {
                    keyDownTimer = MOVE_SPEED;
                    return movement;
            }

            return Movement.None;
        }
        
        public void Update(GameTime gameTime)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsedTime;

            if (!gameLogic.IsGameOver) 
            {

                if (InputManager.KeyPressed(Keys.Space) || ChildStates["pause"].IsActive)
                    ChildStates["pause"].IsActive = true;
                else
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

                    if (InputManager.KeyboardState.IsKeyDown(Keys.Down))
                        move = UpdateKeyHoldDownMovement(gameTime, Movement.Down);
                    else if (InputManager.KeyboardState.IsKeyDown(Keys.Left))
                        move = UpdateKeyHoldDownMovement(gameTime, Movement.Left);
                    else if (InputManager.KeyboardState.IsKeyDown(Keys.Right))
                        move = UpdateKeyHoldDownMovement(gameTime, Movement.Right);
                    else
                        keyDownTimer = MOVE_SPEED;

                    if (timer < 0)
                    {
                        timer = gameLogic.UpdateRate;
                        gameLogic.Update(Movement.Down);
                    }

                    gameLogic.Update(move);
                    score.Text = string.Format("Score: {0}", gameLogic.Score);
                }
            }
            else if (!ChildStates["gameover"].IsActive)
            {
                ChildStates["gameover"].IsActive = true;
            }
            else
            {
                if (InputManager.IsAnyKeyPressed())
                {
                    gameLogic.Restart();
                    ChildStates["gameover"].IsActive = false;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            game.RenderManager.ClearScreen(Color.CornflowerBlue);
            List<GameObject> gameObjects = fieldGameObjectBuilder.CreateGameObjects();

            game.RenderManager.Draw(gameObjects);
            game.RenderManager.Draw(nextShapes[gameLogic.NextShapeType]);

            scorePanel.Draw(game.RenderManager.Graphics);
            score.Draw(game.RenderManager.Graphics);
        }

    }
}
