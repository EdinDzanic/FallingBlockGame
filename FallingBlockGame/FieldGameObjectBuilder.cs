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

        public FieldGameObjectBuilder(Field field, int blockSize)
        {
            this.field = field;
            this.blockSize = blockSize;
        }

        public List<GameObject> CreateGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();

            for (int rowIndex = 0; rowIndex < field.Grid.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < field.Grid[rowIndex].Length; columnIndex++)
                {
                    int x = field.Grid[rowIndex][columnIndex];
                    int y = field.Grid[rowIndex][columnIndex];
                    
                    GameObject gameObject = new GameObject();
                    gameObject.PositionComponent = new PositionComponent(
                        field.X + blockSize * x,  
                        field.Y + blockSize * y);


                }
            }

            return gameObjects;
        }
    }
}
