using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Program
    {
        private static void Main()
        {
            const int rows = 25;
            const int cols = 50;

            var grid = RandomCellsGrid(rows, cols, new Random());
            var generationsCount = 1;

            while (!Console.KeyAvailable)
            {
                Output(grid, generationsCount++);

                grid = grid.NextGeneration();

                Task.Delay(500).Wait();
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

        private static void Output(Grid grid, int generationsCount)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(grid.ToString());

            Console.WriteLine($"Generation: {generationsCount}. To stop press any button.");
        }
    }
}