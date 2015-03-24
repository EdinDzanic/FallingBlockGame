using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FallingBlockGame
{
    public enum Movement
    {
        None,
        Left,
        Right,
        Down,
        Rotate
    }
    
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

        private bool IsPartOfFallingBlocks(int x, int y)
        {
            foreach (var block in fallingBlocks)
            {
                if (block.X == x && block.Y == y)
                    return true;
            }

            return false;
        }
        
        private bool CaneBeMoved(int side, int down)
        {
            foreach (var block in fallingBlocks)
            {
                int newX = block.X + down;
                int newY = block.Y + side;
                if (newX >= FIELD_HEIGHT ||
                    newY < 0 || newY >= FIELD_WIDTH ||
                    (Field.Grid[newX][newY] != 0 && !IsPartOfFallingBlocks(newX, newY)))
                    return false;

            }

            return true;
        }

        private void MoveBlocks(int side, int down)
        {
            if (CaneBeMoved(side, down))
            {
                int value = Field.Grid[fallingBlocks.First().X][fallingBlocks.First().Y];
                
                for (int i = 0; i < fallingBlocks.Count; i++)
                {
                    Field.Grid[fallingBlocks[i].X][fallingBlocks[i].Y] = 0;
                    fallingBlocks[i] = new Coordinate(
                        fallingBlocks[i].X + down,
                        fallingBlocks[i].Y + side);
                }

                AddFallingBlocksToGrid(value);
            }
            else if (down == 1)
            {
                fallingBlocks.Clear();
                CreateFallingBlocks();
            }
        }

        private void Rotate()
        {
            throw new NotImplementedException();
        }

        private void MoveBlocksDown()
        {
            MoveBlocks(0, 1);
        }

        private void MoveBlocksRight()
        {
            MoveBlocks(1, 0);
        }

        private void MoveBlocksLeft()
        {
            MoveBlocks(-1, 0);
        }

        public void Update(Movement move)
        {
            switch (move)
            {
                case Movement.Left:
                    MoveBlocksLeft();
                    break;
                case Movement.Right:
                    MoveBlocksRight();
                    break;
                case Movement.Down:
                    MoveBlocksDown();
                    break;
                case Movement.Rotate:
                    Rotate();
                    break;
                default:
                    break;
            }
        }
    }
}
