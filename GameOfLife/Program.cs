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

            var grid = new ResizingGrid<Cell>(Cell.Populate(rows, cols));

            while (!Console.KeyAvailable)
            {
                Output(grid);

                grid = new ResizingGrid<Cell>(grid.NextGeneration());

                Task.Delay(500).Wait();
            }

            Console.WriteLine("Done!");
        }

        private static void Output(AbstractSpace<Cell> grid)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(FullScreen(grid.ToString()));

            Console.SetCursorPosition(0, 0);
            Console.Write(grid.Report());
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