using System.Linq;

namespace GameOfLife
{
    internal class Cell : AbstractLifeObject
    {
        private readonly bool _live;

        public Cell(bool live)
        {
            _live = live;
        }

        public override AbstractLifeObject NextGeneration()
        {
            var neighbors = Belongs.GetNeighbors(this).OfType<Cell>().Count(c => c._live);

            return new Cell(IsNextGenerationCellAlive(neighbors));
        }

        private bool IsNextGenerationCellAlive(int neighbors)
        {
            if (_live)
                return neighbors is not (< 2 or > 3);

            return neighbors == 3;
        }

        public override string ToString()
        {
            return _live ? "[]" : "--";
        }
    }
}