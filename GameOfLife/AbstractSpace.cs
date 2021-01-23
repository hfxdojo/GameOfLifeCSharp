using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    internal abstract class AbstractSpace<T> where T : AbstractLifeObject, new()
    {
        private protected int Rows => _lifeObjects.GetLength(0);
        private protected int Cols => _lifeObjects.GetLength(1);

        private protected uint _generationsCount = 0;

        private T[,] _lifeObjects;

        public T[,] LifeObjects
        {
            get { return _lifeObjects; }
            set 
            { 
                _lifeObjects = value;
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
            var newLifeObjects = new T[Rows, Cols];

            for (var row = 0; row < Rows; row++)
            for (var col = 0; col < Cols; col++)
                newLifeObjects[row, col] = LifeObjects[row, col].NextGeneration() as T;

            return newLifeObjects;
        }

        public virtual string Report()
        {
            return $"Generation: {_generationsCount}.";
        }

        public virtual string ToString(bool border)
        {
            int objectLength = new T().ToString().Length;
            var horizontalBorder = '+' + new string('-', Cols * objectLength) + '+';
            const char verticalBorder = '|';

            var strBuilder = new StringBuilder();

            if (border) strBuilder.AppendLine(horizontalBorder);

            for (var row = 0; row < Rows; row++)
            {
                if (border) strBuilder.Append(verticalBorder);
                for (var col = 0; col < Cols; col++) strBuilder.Append(LifeObjects[row, col]);
                if (border) strBuilder.Append(verticalBorder);

                strBuilder.AppendLine();
            }

            if (border) strBuilder.AppendLine(horizontalBorder);

            return strBuilder.ToString();
        }

        private void BelongsToThis(T[,] objects)
        {
            for (var row = 0; row < Rows; row++)
                for (var col = 0; col < Cols; col++)
                    objects[row, col].BelongsTo(this);
        }
    }
}