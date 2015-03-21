using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace engine
{
    public struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

    }

    public class TextureAtlas : ICloneable
    {

        private int columns;
        private int rows;
        private Texture2D texture;

        public Texture2D Texture { get { return texture; } }
        public Position CurrentFrame { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public TextureAtlas(Texture2D texture)
        {
            this.texture = texture;
            columns = 1;
            rows = 1;
            Width = texture.Width;
            Height = texture.Height;
            CurrentFrame = new Position(0, 0);
        }

        public TextureAtlas(Texture2D texture, int rows, int columns)
        {
            this.texture = texture;
            this.columns = columns;
            this.rows = rows;
            Width = texture.Width;
            Height = texture.Height;
            CurrentFrame = new Position(0, 0);
        }

        public TextureAtlas(Texture2D texture, int rows, int columns, Position currentFrame)
        {
            this.texture = texture;
            this.columns = columns;
            this.rows = rows;
            Width = texture.Width;
            Height = texture.Height;
            CurrentFrame = currentFrame;
        }

        public TextureAtlas(Texture2D texture, int rows, int columns, int width, int height, Position currentFrame)
        {
            this.texture = texture;
            this.columns = columns;
            this.rows = rows;
            Width = width;
            Height = height;
            CurrentFrame = currentFrame;
        }

        public Rectangle GetSourceRectangle()
        {
            int width = texture.Width / columns;
            int height = texture.Height / rows;

            Rectangle sourceRectangle = new Rectangle(width * CurrentFrame.Y, height * CurrentFrame.X, width, height);
            
            return sourceRectangle;
        }

        public object Clone()
        {
            TextureAtlas copy = new TextureAtlas(Texture, rows, columns);
            copy.Height = Height;
            copy.Width = Width;
            copy.CurrentFrame = CurrentFrame;

            return copy;
        }
    }
}
