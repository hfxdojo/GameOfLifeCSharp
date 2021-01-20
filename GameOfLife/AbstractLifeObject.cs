namespace GameOfLife
{
    internal abstract class AbstractLifeObject
    {
        public readonly bool Live;
        private protected AbstractSpace Belongs;

        public AbstractLifeObject(bool live)
        {
            Live = live;
        }

        public void BelongsTo(AbstractSpace space)
        {
            Belongs = space;
        }

        public abstract AbstractLifeObject NextGeneration();
    }
}