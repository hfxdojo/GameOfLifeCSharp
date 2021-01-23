using System;
using System.Linq;
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

            //var grid = new Grid<Cell>(Cell.Populate(rows, cols));
            var grid = new ResizingGrid<Cell>(Cell.Populate(rows, cols));

            while (!Console.KeyAvailable)
            {
                Output(grid);

                grid.LifeObjects = grid.NextGeneration();

                Task.Delay(500).Wait();
            }

            Console.WriteLine("Done!");
        }

        private static void Output(AbstractSpace<Cell> grid)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(ToCenterOfWindow(grid.ToString()));

            Console.SetCursorPosition(0, 0);
            Console.Write(grid.Report() + $" To stop press any button.");
        }

        private static string ToCenterOfWindow(string gridString)
        {
            var gridLines = gridString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var gridWidth = gridLines[0].Length;
            var gridHeight = gridLines.Length;

            int horozontalMarginWidth = GetMarginSize(Console.WindowWidth, gridWidth);
            int verticalMarginHeight = GetMarginSize(Console.WindowHeight, gridHeight);

            string emptyLine = new string(' ', Console.WindowWidth) + Environment.NewLine;

            var horizontalMargin = new string(' ', horozontalMarginWidth);

            var horizontalBoundaryLine = horizontalMargin + '+' + new string('-', gridWidth) + '+' + horizontalMargin + Environment.NewLine;

            var strBuilder = new StringBuilder();

            // top margin
            strBuilder.Append(string.Concat(Enumerable.Repeat(emptyLine, verticalMarginHeight)));

            strBuilder.Append(horizontalBoundaryLine);

            foreach (var gridLine in gridLines)
            {
                // left margin + vertical boundary + gridLine + vertical boundary + right margin
                strBuilder.Append(horizontalMargin + '|' + gridLine + '|' + horizontalMargin + Environment.NewLine);
            }

            strBuilder.Append(horizontalBoundaryLine);

            // bottom margin
            strBuilder.Append(string.Concat(Enumerable.Repeat(emptyLine, verticalMarginHeight)));

            return strBuilder.ToString();
        }

        private static int GetMarginSize(int windowSize, int gridWidth)
        {
            int marginSize = (windowSize - gridWidth) / 2 - 1;
            return marginSize < 0 ? 0 : marginSize;
        }
    }
}