using System;
using System.Collections.Generic;

namespace GameOfLife
{
    internal class Grid<T> : AbstractSpace<T> where T : AbstractLifeObject, new()
    {
        public Grid(T[,] objects) : base(objects)
        {
        }

        public override IEnumerable<AbstractLifeObject> GetNeighbors(AbstractLifeObject lifeObject)
        {
            var (objectRow, objectCol) = GetLocation(lifeObject);

            for (var row = objectRow - 1; row <= objectRow + 1; row++)
            for (var col = objectCol - 1; col <= objectCol + 1; col++)
            {
                if (row == objectRow && col == objectCol) continue;
                if (row < 0 || col < 0) continue;
                if (row >= Rows || col >= Cols) continue;

                yield return LifeObjects[row, col];
            }
        }

        private (int row, int col) GetLocation(AbstractLifeObject lifeObject)
        {
            for (var row = 0; row < Rows; row++)
            for (var col = 0; col < Cols; col++)
                if (LifeObjects[row, col].Equals(lifeObject))
                    return (row, col);

            throw new ArgumentException("Grid does not contain this life object");
        }
    }
}