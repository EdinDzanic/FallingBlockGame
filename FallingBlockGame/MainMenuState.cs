﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using engine;

namespace FallingBlockGame
{
    public class MainMenuState : IGameState
    {
        private const string BACKGROUND = "menubackground";
        
        private FallingBlockGame game;
        private GameStateManager gameStateManager;

        public bool IsActive { get; set; }
        public Dictionary<string, IGameState> ChildStates { get; set; }

        private Texture2D Background;
        private Menu menu;

        public MainMenuState(FallingBlockGame game, GameStateManager gameStateManager)
        {
            this.game = game;
            this.gameStateManager = gameStateManager;

            IsActive = false;
            ChildStates = new Dictionary<string, IGameState>();

            Background = game.Content.Load<Texture2D>(BACKGROUND);
            
            SpriteFont font = game.Content.Load<SpriteFont>("font_20");
            menu = new Menu(font);
            menu.Position = new Vector2(100, 350);
            menu.MenuItems.Add(new MenuItem("Start"));
            menu.MenuItems.Add(new MenuItem("Exit"));
        }
        
        public void Update(GameTime gameTime)
        {
            menu.Update();
        }

        public void Draw(GameTime gameTime)
        {
            game.RenderManager.Graphics.SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            game.RenderManager.Graphics.SpriteBatch.Draw(Background, new Vector2(0,0), new Rectangle(0, 0, 600, 700), Color.White);
            game.RenderManager.Graphics.SpriteBatch.End();

            menu.Draw(game.RenderManager.Graphics);
        }
    }
}