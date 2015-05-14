using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using engine;

namespace engine
{
    public class Image
    {        
        private string imageFile;
        private Texture2D imageTexture;
        private int width;
        private int heigth;

        public Vector2 Position { get; set; }
        public int Width {
            get { return width; } 
            set 
            {
                if (value < 0)
                    throw new ArgumentException("Width of image must be bigger than 0.");
                else
                    width = value;
            }
        }

        public int Heigth
        {
            get { return heigth; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Heigth of image must be bigger than 0.");
                else
                    heigth = value;
            }
        }

        public Texture2D ImageTexture {
            get { return imageTexture; }
            set
            {
                imageTexture = value;
            }
        }

        public Image(ContentManager contentManager, string imageFile)
        {
            this.imageFile = imageFile;
            imageTexture = contentManager.Load<Texture2D>(imageFile);
        }

        public Image(Texture2D texture)
        {
            imageTexture = texture;
            imageFile = texture.Name;
        }

        public void Draw(Graphics graphics)
        {
            graphics.SpriteBatch.Begin();

            Rectangle destinationRectangle = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Width,
                Heigth);
            graphics.SpriteBatch.Draw(imageTexture, destinationRectangle, Color.White);

            graphics.SpriteBatch.End();

        }
    }
}
