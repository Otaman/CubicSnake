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
            
            Cells = new bool[width, height, depth];
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
            return point.X >= 0 && point.X < Width
                && point.Y >= 0 && point.Y < Height
                && point.Z >= 0 && point.Z < Depth;
        }

        public void Remove(Point point)
        {
            if (FitsInCells(point))
                Cells[point.X, point.Y, point.Z] = false;
        }
    }
}