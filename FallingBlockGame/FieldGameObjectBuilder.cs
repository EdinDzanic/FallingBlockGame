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
        private Dictionary<int, Position> textureMap;

        public FieldGameObjectBuilder(Field field, int blockSize, TextureAtlas textureAtlas)
        {
            this.field = field;
            this.blockSize = blockSize;
            this.textureAtlas = textureAtlas;
            textureMap = new Dictionary<int, Position>();

            this.textureAtlas.Height = blockSize;
            this.textureAtlas.Width = blockSize;

            textureMap[0] = new Position(0, 0);
            textureMap[1] = new Position(0, 1);
            textureMap[2] = new Position(0, 2);
            textureMap[3] = new Position(1, 0);
            textureMap[4] = new Position(1, 1);
            textureMap[5] = new Position(1, 2);
        }

        public List<GameObject> CreateGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            for (int rowIndex = 2; rowIndex < field.Grid.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < field.Grid[rowIndex].Length; columnIndex++)
                {                    
                    PositionComponent position = new PositionComponent(
                        field.X + blockSize * columnIndex,
                        field.Y + blockSize * (rowIndex - 2));

                    TextureAtlas copyTextureAtlas = textureAtlas.Clone() as TextureAtlas;
                    int value = field.Grid[rowIndex][columnIndex];
                    copyTextureAtlas.CurrentFrame = textureMap[value];
                    DrawableComponent drawable = new DrawableComponent(copyTextureAtlas, position);

                    GameObject gameObject = new GameObject(position, drawable);
                    gameObjects.Add(gameObject);
                }
            }

            return gameObjects;
        }
    }
}
