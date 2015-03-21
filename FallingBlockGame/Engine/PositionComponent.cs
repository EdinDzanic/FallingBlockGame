using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace engine
{
    public class PositionComponent
    {
        #region Fields and properties

        public Vector2 Position { get; set; }

        #endregion

        #region Constructor

        public PositionComponent(Vector2 position)
        {
            Position = position;
        }

        public PositionComponent(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        #endregion
    }
}
