using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using engine;

namespace FallingBlockGame
{
    public class GameplayState : IGameState
    {
        private FallingBlockGame game;
        private FieldGameObjectBuilder fieldGameObjectBuilder;

        public GameplayState(FallingBlockGame game)
        {
            this.game = game;
        }
        
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
