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
        private const int FIELD_HEIGHT = 22;
        private const double SPEED_RATE_INCREASE = 0.1;

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
        public Field Field { get { return field; } }

        private List<Coordinate> fallingBlocks;

        private bool isGameOver;
        public bool IsGameOver { get { return isGameOver; } }
        public int Speed { get; set; }
        public double UpdateRate { get { return (1 - (SPEED_RATE_INCREASE * (Speed - 1))); } }

        public GameLogic()
        {
            field = new Field(FIELD_HEIGHT, FIELD_WIDTH, 20, 20);

            fallingBlocks = new List<Coordinate>();
            CreateFallingBlocks();

            isGameOver = false;

            Speed = 1;
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
                if (c != '-')
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
                if (block.X >= 0)
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
                ClearFullRows();
                CheckForGameOver();
                CreateFallingBlocks();
            }
        }

        private void CheckForGameOver()
        {
            for (int rowIndex = 0; rowIndex < 2; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < FIELD_WIDTH; columnIndex++)
                {
                    if (Field.Grid[rowIndex][columnIndex] != 0)
                    {
                        isGameOver = true;
                        break;
                    }
                }
            }
        }

        public void Restart()
        {
            for (int rowIndex = 0; rowIndex < Field.Grid.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < Field.Grid[rowIndex].Length; columnIndex++)
                {
                    Field.Grid[rowIndex][columnIndex] = 0;
                }
            }

            isGameOver = false;
            fallingBlocks.Clear();
            CreateFallingBlocks();
        }

        private void ClearFullRows()
        {
            for (int row = 0; row < Field.Grid.Length; row++)
            {
                bool isFullRow = true;
                for (int column = 0; column < Field.Grid[row].Length; column++)
                {
                    if (Field.Grid[row][column] == 0)
                    {
                        isFullRow = false;
                        break;
                    }
                }

                if (isFullRow)
                    MoveRowsDown(row);
            }
        }

        private void MoveRowsDown(int row)
        {
            for (int rowIndex = row; rowIndex >= 0; rowIndex--)
            {
                if (rowIndex != 0)
                    for (int column = 0; column < Field.Grid[row].Length; column++)
                    {
                        Field.Grid[rowIndex][column] = Field.Grid[rowIndex - 1][column];
                    }
            }
        }

        private void Rotate()
        {
            int xMin = int.MaxValue;
            int xMax = int.MinValue;
            int yMin = int.MaxValue;
            int yMax = int.MinValue;

            foreach (var block in fallingBlocks)
            {
                xMin = Math.Min(xMin, block.X);
                yMin = Math.Min(yMin, block.Y);
                xMax = Math.Max(xMax, block.X);
                yMax = Math.Max(yMax, block.Y);
            }

            List<Coordinate> rotatedBlocks = new List<Coordinate>();
            for (int x = xMin; x <= xMax; x++)
            {
                for (int y = yMin; y <= yMax; y++)
                {
                    Coordinate coordinate = new Coordinate(x, y);
                    if (fallingBlocks.Contains(coordinate))
                    {
                        coordinate.X = xMin + y - yMin;
                        coordinate.Y = yMax - x + xMin;
                        rotatedBlocks.Add(coordinate);
                    }
                }
            }

            bool canBeRotated = true;
            foreach (var rotatedBlock in rotatedBlocks)
            {
                if (rotatedBlock.X < 0 || rotatedBlock.X >= FIELD_HEIGHT ||
                    rotatedBlock.Y < 0 || rotatedBlock.Y >= FIELD_WIDTH)
                {
                    canBeRotated = false;
                    break;
                }
                else if (Field.Grid[rotatedBlock.X][rotatedBlock.Y] != 0 &&
                    !IsPartOfFallingBlocks(rotatedBlock.X, rotatedBlock.Y))
                {
                    canBeRotated = false;
                    break;
                }
            }

            if (canBeRotated)
            {
                int blockColor = Field.Grid[fallingBlocks.First().X][fallingBlocks.First().Y];
                foreach (var block in fallingBlocks)
                {
                    Field.Grid[block.X][block.Y] = 0;
                }

                foreach (var rotatedBlock in rotatedBlocks)
                {
                    Field.Grid[rotatedBlock.X][rotatedBlock.Y] = blockColor;
                }

                fallingBlocks = rotatedBlocks;
            }
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
