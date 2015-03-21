using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ts
{
    interface IGameState
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}
