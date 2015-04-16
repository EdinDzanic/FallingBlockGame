using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace engine
{
    public class Border
    {
        public int Size { get; set; }
        public Vector2 Position { get; set; }
        public int Heigth { get; set; }
        public int Width { get; set; }

        public Color BorderColor { get; set; }
        private Texture2D texture;

        public Border(GraphicsDevice graphicsDevice)
        {
            Position = new Vector2(0, 0);
            BorderColor = Color.Black;

            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.White });
            Size = 1;
        }

        public void Draw(Graphics graphics)
        {
            graphics.SpriteBatch.Begin();

            int X = (int)Position.X;
            int Y = (int)Position.Y;
            
            graphics.SpriteBatch.Draw(texture, new Rectangle(X, Y, Width, Size), BorderColor);
            graphics.SpriteBatch.Draw(texture, new Rectangle(X, Y + Heigth, Width, Size), BorderColor);
            graphics.SpriteBatch.Draw(texture, new Rectangle(X, Y, Size, Heigth), BorderColor);
            graphics.SpriteBatch.Draw(texture, new Rectangle(X + Width, Y, Size, Heigth), BorderColor);

            graphics.SpriteBatch.End();
        }
    }
}
