using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace engine
{
    public class Panel
    {
        public Vector2 Position { get; set; }
        public int Heigth { get; set; }
        public int Width { get; set; }

        public Color BackgroundColor { get; set; }
        public Texture2D BackgroundImage { get; set; }

        public Panel(GraphicsDevice graphicsDevice)
        {
            Position = new Vector2(0, 0);
            BackgroundColor = Color.White;
            BackgroundImage = new Texture2D(graphicsDevice, 1, 1);
            BackgroundImage.SetData<Color>(new Color[] { Color.White });
        }

        public void Draw(Graphics graphics)
        {
            graphics.SpriteBatch.Begin();

            Rectangle destinationRectangle = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Width,
                Heigth);
            graphics.SpriteBatch.Draw(BackgroundImage, destinationRectangle, BackgroundColor);

            graphics.SpriteBatch.End();
        }
    }
}
