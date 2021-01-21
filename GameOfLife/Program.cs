using System;
using System.Text;
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
            Console.Write(FullScreen(grid.ToString()));

            Console.SetCursorPosition(0, 0);
            Console.Write($"Generation: {generationsCount}. To stop press any button.");
        }

        private static string FullScreen(string gridString)
        {
            var gridLines = gridString.Split(Environment.NewLine);

            var gridWidth = gridLines[0].Length;
            var gridHeight = gridLines.Length;

            int horozontalMargin = (Console.WindowWidth - gridWidth) / 2;
            horozontalMargin = horozontalMargin < 0 ? 0 : horozontalMargin;

            int verticalMargin = (Console.WindowHeight - gridHeight) / 2;
            verticalMargin = verticalMargin < 0 ? 0 : verticalMargin;

            var strBuilder = new StringBuilder();
            // top margin
            strBuilder.Insert(0, new string(' ', Console.WindowWidth - 10) + Environment.NewLine, verticalMargin);

            foreach (var gridLine in gridLines)
            {
                // left margin
                strBuilder.Append(new string(' ', horozontalMargin));
                strBuilder.Append(gridLine);
                // right margin
                strBuilder.Append(new string(' ', horozontalMargin));
                strBuilder.Append(Environment.NewLine);
            }
            // bottom margin
            strBuilder.Insert(strBuilder.Length, new string(' ', Console.WindowWidth) + Environment.NewLine, verticalMargin);

            return strBuilder.ToString();
        }
    }
}