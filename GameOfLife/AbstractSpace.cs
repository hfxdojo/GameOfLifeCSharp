using System.Collections.Generic;

namespace GameOfLife
{
    internal abstract class AbstractSpace<T> where T : AbstractLifeObject
    {
        private protected int _cols;
        private protected int _rows;
        private protected uint _generationsCount = 1;

        private T[,] _lifeObjects;

        public T[,] LifeObjects
        {
            get { return _lifeObjects; }
            set 
            { 
                _lifeObjects = value;
                _rows = _lifeObjects.GetLength(0);
                _cols = _lifeObjects.GetLength(1);
                _generationsCount++;

                BelongsToThis(_lifeObjects);
            }
        }


        public AbstractSpace(T[,] objects)
        {
            LifeObjects = objects;
        }

        public abstract IEnumerable<AbstractLifeObject> GetNeighbors(AbstractLifeObject obj);

        public virtual T[,] NextGeneration()
        {
            var newLifeObjects = new T[_rows, _cols];

            for (var row = 0; row < _rows; row++)
            for (var col = 0; col < _cols; col++)
                newLifeObjects[row, col] = LifeObjects[row, col].NextGeneration() as T;

            return newLifeObjects;
        }

        private void BelongsToThis(T[,] objects)
        {
            for (var row = 0; row < _rows; row++)
                for (var col = 0; col < _cols; col++)
                    objects[row, col].BelongsTo(this);
        }

        internal string Report()
        {
            return $"Generation: {_generationsCount}. To stop press any button.";
        }
    }
}