using System;

namespace CubicSnake.Core
{
    public class Space
    {
        public Space(int height, int width, int depth)
        {
            Height = height;
            Width = width;
            Depth = depth;
            
            Cells = new bool[height, width, depth];
        }
        
        public int Height { get; }
        public int Width { get; }
        public int Depth { get; }

        public bool[,,] Cells { get; }

        public bool Insert(Point point)
        {
            if (!FitsInCells(point))
            {
                return false;
            }
            
            var cellIsUsed = Cells[point.X, point.Y, point.Z];
            if (cellIsUsed)
            {
                return false;
            }

            Cells[point.X, point.Y, point.Z] = true;
            return true;
        }

        public bool FitsInCells(Point point)
        {
            return point.X >= 0 && point.X < Height
                && point.Y >= 0 && point.Y < Width
                && point.Z >= 0 && point.Z < Depth;
        }
    }
}