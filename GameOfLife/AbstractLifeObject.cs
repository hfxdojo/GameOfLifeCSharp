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
    }
}