﻿using System.Linq;

namespace GameOfLife
{
    internal class Cell : AbstractLifeObject
    {
        private AbstractSpace<Cell> Belongs;

        public Cell(bool live) : base(live)
        {
        }

        public Cell() : base(false)
        {
        }

        public override void BelongsTo(object space)
        {
            Belongs = space as AbstractSpace<Cell>;
        }

        public override AbstractLifeObject NextGeneration()
        {
            var neighbors = Belongs.GetNeighbors(this).OfType<Cell>().Count(c => c.Live);

            return new Cell(IsNextGenerationCellAlive(neighbors));
        }

        public override string ToString()
        {
            return Live ? "[]" : "  ";
        }

        private bool IsNextGenerationCellAlive(int neighbors)
        {
            if (Live)
                return neighbors is not (< 2 or > 3);

            return neighbors == 3;
        }
    }
}