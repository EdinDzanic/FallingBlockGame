using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using engine;

namespace FallingBlockGame
{
    public class FieldGameObjectBuilder : IGameObjectBuilder
    {
        private Field field;
        private int blockSize;
        private TextureAtlas textureAtlas;

        public FieldGameObjectBuilder(Field field, int blockSize, TextureAtlas textureAtlas)
        {
            this.field = field;
            this.blockSize = blockSize;
            this.textureAtlas = textureAtlas;
        }

        public List<GameObject> CreateGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            for (int rowIndex = 0; rowIndex < field.Grid.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < field.Grid[rowIndex].Length; columnIndex++)
                {                    
                    PositionComponent position = new PositionComponent(
                        field.X + blockSize * columnIndex,  
                        field.Y + blockSize * rowIndex);

                    textureAtlas.Height = blockSize;
                    textureAtlas.Width = blockSize;
                    TextureAtlas copyTextureAtlas = textureAtlas.Clone() as TextureAtlas;
                    DrawableComponent drawable = new DrawableComponent(copyTextureAtlas, position);

                    GameObject gameObject = new GameObject(position, drawable);
                    gameObjects.Add(gameObject);
                }
            }

            return gameObjects;
        }
    }
}
