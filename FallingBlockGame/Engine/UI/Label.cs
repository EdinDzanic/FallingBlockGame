using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace engine
{
    public class Label
    {
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public SpriteFont Font { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Padding { get; set; }
        public int Width { get; set; }
        public int Heigth { get; set; }

        public Color BackgroundColor { get; set; }
        public Texture2D BackgroundImage { get; set; }

        public Label(string text)
        {
            Text = text;
            Padding = new Vector2(0, 0);
            Position = new Vector2(0, 0);
        }

        public void Draw(Graphics graphics)
        {
            graphics.SpriteBatch.Begin();
            graphics.SpriteBatch.Draw(BackgroundImage, 
                new Rectangle((int)Position.X, (int)Position.Y, Width, Heigth), 
                BackgroundColor);
            graphics.SpriteBatch.DrawString(Font, Text, Position + Padding, TextColor);
            graphics.SpriteBatch.End();
        }
    }
}
