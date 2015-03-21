using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Ts
{
    public class Graphics
    {
        #region Fields and Properties

        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;

        public SpriteBatch SpriteBatch 
        {
            get
            {
                return spriteBatch;
            } 
        }
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDevice;
            }
        }

        #endregion

        #region Constructor

        public Graphics(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        #endregion
    }
}
