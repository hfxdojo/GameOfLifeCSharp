using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    internal static class Positioner
    {
        public static string InCenter(string gridString)
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