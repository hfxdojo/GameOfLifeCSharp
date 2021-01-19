using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Program
    {
        private static void Main()
        {
            const int rows = 30;
            const int cols = 30;

            var grid = RandomCellsGrid(rows, cols, new Random());

            while (!Console.KeyAvailable)
            {
                Console.WriteLine(grid.ToString());

                grid = grid.NextGeneration();

                Task.Delay(1000).Wait();
            }

            Console.WriteLine("Done!");
        }

        private static Grid RandomCellsGrid(int rows, int cols, Random random)
        {
            var cells = new Cell[rows, cols];

            for (var row = 0; row < rows; row++)
            for (var col = 0; col < cols; col++)
                cells[row, col] = new Cell(RandomlyAlive(random));

            return new Grid(cells);
        }

        private static bool RandomlyAlive(Random random)
        {
            const double threshold = 0.5;
            return random.NextDouble() > threshold;
        }
    }
}