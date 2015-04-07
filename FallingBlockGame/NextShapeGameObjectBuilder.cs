using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using engine;

namespace FallingBlockGame
{
    public class NextShapeGameObjectBuilder : IGameObjectBuilder
    {
        private Vector2 position;
        private string[] blockTypes;
        private TextureAtlas textureAtlas;
        private int blockSize;
        private Random random;

        public int ShapeType { get; set; }

        public NextShapeGameObjectBuilder(Vector2 position, string[] blockTypes, TextureAtlas textureAtlas, int blockSize)
        {
            this.position = position;
            this.blockTypes = blockTypes;
            this.textureAtlas = textureAtlas;
            this.blockSize = blockSize;

            random = new Random();
        }

        public List<GameObject> CreateGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            int row = 0;
            int col = 0;

            TextureAtlas copyTextureAtlas = textureAtlas.Clone() as TextureAtlas;
            copyTextureAtlas.CurrentFrame = new Position(random.Next(0, 2), random.Next(0, 3));

            foreach (char c in blockTypes[ShapeType])
            {
                if (c != '-')
                {
                    if (c != '0')
                    {
                        Vector2 currentPosition = position + new Vector2 (col * blockSize, row * blockSize);
                        PositionComponent positionComponent = new PositionComponent(currentPosition);
                        DrawableComponent drawableComponent = new DrawableComponent(copyTextureAtlas, positionComponent);

                        gameObjects.Add(new GameObject(positionComponent, drawableComponent));
                    }

                    col++;
                }
                else
                {
                    row++;
                    col = 0;
                }
            }

            return gameObjects;
        }
    }
}
