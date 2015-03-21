using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace engine
{
    public class Drawable2D : IDrawable
    {
        private DrawableComponent drawableComponent;
        private PositionComponent positionComponent;

        public Drawable2D(PositionComponent positionComponent, DrawableComponent drawableComponent)
        {
            this.positionComponent = positionComponent;
            this.drawableComponent = drawableComponent;
        }

        public void Draw(Graphics graphics)
        {
            graphics.SpriteBatch.Begin();
            
            Rectangle destinationRectangle = new Rectangle(
                (int)positionComponent.Position.X, 
                (int)positionComponent.Position.Y,
                drawableComponent.Texture.Width, 
                drawableComponent.Texture.Height);
            
            graphics.SpriteBatch.Draw(drawableComponent.Texture.Texture, destinationRectangle, 
                drawableComponent.Texture.GetSourceRectangle(), drawableComponent.Color);
            
            graphics.SpriteBatch.End();
        }

    }
}
