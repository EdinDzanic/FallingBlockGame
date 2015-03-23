using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FallingBlockGame
{
    public class GameLogic
    {
        private const int FIELD_WIDTH = 10;
        private const int FIELD_HEIGHT = 20;

        private string[] blockTypes = new string[] {
            "11-11",
            "010-111",
            "001-111",
            "100-111",
            "1111",
            "011-110",
            "110-011"
        };

        private Field field;
        public Field Field { get { return field;} }

        private List<Coordinate> fallingBlocks;
        

        public GameLogic()
        {
            field = new Field(FIELD_HEIGHT, FIELD_WIDTH, 20, 20);

            fallingBlocks = new List<Coordinate>();
            CreateFallingBlocks();
        }

        public void CreateFallingBlocks()
        {
            Random random = new Random();
            int shapeType = random.Next(0, blockTypes.Length);
            int color = random.Next(1, 5);

            int row = 0;
            int col = 0;
            foreach (char c in blockTypes[shapeType])
            {
                if(c != '-')
                {
                    if (c != '0')
                        fallingBlocks.Add(new Coordinate(row, col));
                    col++;
                }
                else
                {
                    row++;
                    col = 0;
                }
            }

            AddFallingBlocksToGrid(color);
        }

        private void AddFallingBlocksToGrid(int color)
        {
            foreach (Coordinate block in fallingBlocks)
            {
                Field.Grid[block.X][block.Y] = color;
            }
        }
    }
}
