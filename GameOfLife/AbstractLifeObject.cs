namespace GameOfLife
{
    internal abstract class AbstractLifeObject
    {
        private protected AbstractSpace Belongs;

        public void BelongsTo(AbstractSpace space)
        {
            Belongs = space;
        }

        public abstract AbstractLifeObject NextGeneration();
    }
}