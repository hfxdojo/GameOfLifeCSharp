using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    internal class Program
    {
        private static void Main()
        {
            const int rows = 15;
            const int cols = 25;

            var grid = RandomCellsGrid(rows, cols, new Random());
            var generationsCount = 1;

            while (!Console.KeyAvailable)
            {
                Output(grid, generationsCount++);

                grid = new ResizingGrid<Cell>(grid.NextGeneration());

                Task.Delay(500).Wait();
            }

            Console.WriteLine("Done!");
        }

        private static AbstractSpace<Cell> RandomCellsGrid(int rows, int cols, Random random)
        {
            var cells = new Cell[rows, cols];

            for (var row = 0; row < rows; row++)
            for (var col = 0; col < cols; col++)
                cells[row, col] = new Cell(RandomlyAlive(random));

            return new ResizingGrid<Cell>(cells);
        }

        private static bool RandomlyAlive(Random random)
        {
            const double threshold = 0.5;
            return random.NextDouble() > threshold;
        }

        private static void Output(AbstractSpace<Cell> grid, int generationsCount)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Generation: {generationsCount}. To stop press any button.");

            Console.SetCursorPosition(0, 1);
            Console.Write(grid.ToString());
        }
    }
}