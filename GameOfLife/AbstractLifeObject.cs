using System;

namespace GameOfLife
{
    internal abstract class AbstractLifeObject
    {
        public readonly bool Live;

        public AbstractLifeObject(bool live)
        {
            Live = live;
        }

        public abstract void BelongsTo(object space);

        public abstract AbstractLifeObject NextGeneration();

        private protected static bool RandomlyAlive(Random random)
        {
            const double threshold = 0.5;
            return random.NextDouble() > threshold;
        }
    }
}