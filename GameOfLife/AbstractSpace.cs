using System.Collections.Generic;

namespace GameOfLife
{
    internal abstract class AbstractSpace<T> where T : AbstractLifeObject
    {
        private protected readonly int _cols;
        private protected readonly int _rows;
        private protected readonly T[,] _lifeObjects;

        public AbstractSpace(T[,] objects)
        {
            _rows = objects.GetLength(0);
            _cols = objects.GetLength(1);

            BelongsToThis(objects);

            _lifeObjects = objects;
        }

        public abstract IEnumerable<AbstractLifeObject> GetNeighbors(AbstractLifeObject obj);

        public virtual T[,] NextGeneration()
        {
            var newLifeObjects = new T[_rows, _cols];

            for (var row = 0; row < _rows; row++)
            for (var col = 0; col < _cols; col++)
                newLifeObjects[row, col] = _lifeObjects[row, col].NextGeneration() as T;

            return newLifeObjects;
        }

        private void BelongsToThis(T[,] objects)
        {
            for (var row = 0; row < _rows; row++)
                for (var col = 0; col < _cols; col++)
                    objects[row, col].BelongsTo(this);
        }
    }
}