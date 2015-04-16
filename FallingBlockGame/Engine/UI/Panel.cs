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
        private int height;
        private int width;
        private Vector2 position;
        
        public Vector2 Position {
            get 
            {
                return position;
            }
            set
            {
                position = value;
                Border.Position = position;
            }
        }

        public int Heigth
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                Border.Heigth = height;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                Border.Width = width;
            }
        }
        public Color BackgroundColor { get; set; }
        public Texture2D BackgroundImage { get; set; }

        public Border Border { get; set; }

        public Panel(GraphicsDevice graphicsDevice)
        {
            position = new Vector2(0, 0);
            BackgroundColor = Color.White;
            BackgroundImage = new Texture2D(graphicsDevice, 1, 1);
            BackgroundImage.SetData<Color>(new Color[] { Color.White });

            Border = new Border(graphicsDevice);
            Border.Position = Position;
            Border.Width = Width;
            Border.Heigth = Heigth;
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

            Border.Draw(graphics);
        }
    }
}
