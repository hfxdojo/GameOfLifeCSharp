using System.Collections.Generic;

namespace GameOfLife
{
    internal abstract class AbstractSpace
    {
        private protected readonly int _cols;
        private protected readonly int _rows;
        private protected readonly AbstractLifeObject[,] _lifeObjects;

        public AbstractSpace(AbstractLifeObject[,] objects)
        {
            _rows = objects.GetLength(0);
            _cols = objects.GetLength(1);

            BelongsToThis(objects);

            _lifeObjects = objects;
        }

        public abstract IEnumerable<AbstractLifeObject> GetNeighbors(AbstractLifeObject obj);

        public Grid NextGeneration()
        {
            var newLifeObjects = new AbstractLifeObject[_rows, _cols];

            for (var row = 0; row < _rows; row++)
            for (var col = 0; col < _cols; col++)
                newLifeObjects[row, col] = _lifeObjects[row, col].NextGeneration();

            return new Grid(newLifeObjects);
        }

        private void BelongsToThis(AbstractLifeObject[,] objects)
        {
            for (var row = 0; row < _rows; row++)
                for (var col = 0; col < _cols; col++)
                    objects[row, col].BelongsTo(this);
        }
    }
}