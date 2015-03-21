using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace engine
{
    public class DrawableComponent : IDrawableComponent
    {
        private PositionComponent position;
        public TextureAtlas Texture { get; set; }
        public Color Color { get; set; }

        public DrawableComponent(TextureAtlas texture, PositionComponent position)
        {
            Texture = texture;
            this.position = position;
            Color = Color.White;
        }

        public DrawableComponent(TextureAtlas texture, PositionComponent position, Color color)
        {
            Texture = texture;
            this.position = position;
            Color = color;
        }

        public IDrawable CreateDrawable()
        {
            return new Drawable2D(position, this);
        }
    }
}
