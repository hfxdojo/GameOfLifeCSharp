using System.Collections.Generic;

namespace GameOfLife
{
    internal abstract class AbstractSpace
    {
        public abstract IEnumerable<AbstractLifeObject> GetNeighbors(AbstractLifeObject obj);
    }
}