using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;

namespace engine
{
    public class GameStateManager
    {
        // map of states
        private Dictionary<string, IGameState> gameStates;
        private IGameState currentState;
        public Game Game { get; set; }

        public GameStateManager(Game game)
        {
            gameStates = new Dictionary<string, IGameState>();
            Game = game;
        }

        public void Update(GameTime gameTime)
        {
            if (currentState.IsActive)
                currentState.Update(gameTime);
        }
        private void UpdateChildStates(GameTime gameTime)
        {
            foreach (IGameState state in currentState.ChildStates)
            {
                if (state.IsActive)
                    state.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (currentState.IsActive)
                currentState.Draw(gameTime);
        }
        private void DrawChildStates(GameTime gameTime)
        {
            foreach (IGameState state in currentState.ChildStates)
            {
                if (state.IsActive)
                    state.Draw(gameTime);
            }
        }

        public void Add(string stateName, IGameState state)
        {
            gameStates[stateName] = state;
        }

        public void ChangeState(string stateName)
        {
            if (currentState != null)
                currentState.IsActive = false;
            currentState = gameStates[stateName];
            currentState.IsActive = true;
        }
    }
}
