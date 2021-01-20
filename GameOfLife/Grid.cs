using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    internal class Grid<T> : AbstractSpace<T> where T : AbstractLifeObject
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
                if (row >= _rows || col >= _cols) continue;

                yield return _lifeObjects[row, col];
            }
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++) strBuilder.Append(_lifeObjects[row, col]);

                strBuilder.Append(Environment.NewLine);
            }

            return strBuilder.ToString();
        }

        private (int row, int col) GetLocation(AbstractLifeObject lifeObject)
        {
            for (var row = 0; row < _rows; row++)
            for (var col = 0; col < _cols; col++)
                if (_lifeObjects[row, col].Equals(lifeObject))
                    return (row, col);

            throw new ArgumentException("Grid does not contain this life object");
        }
    }
}