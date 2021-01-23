using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    internal static class Positioner
    {
        public static string InCenter(string gridString, int windowWidth, int windowHeight)
        {
            var elements = new Elements(gridString, windowWidth, windowHeight);

            var strBuilder = new StringBuilder(windowWidth * windowHeight)
                .Append(elements.VerticalMargin)
                .Append(elements.HorizontalBoundaryLine);

            foreach (var gridLine in elements.GridLines)
            {
                // left margin + vertical boundary + gridLine + vertical boundary + right margin
                strBuilder.AppendLine(elements.HorizontalMargin + '|' + gridLine + '|' + elements.HorizontalMargin);
            }

            strBuilder.Append(elements.HorizontalBoundaryLine)
                .Append(elements.VerticalMargin);

            return strBuilder.ToString();
        }

        private class Elements
        {
            public string[] GridLines;
            public string VerticalMargin;
            public string HorizontalMargin;
            public string HorizontalBoundaryLine;

            public Elements(string gridString, int windowWidth, int windowHeight)
            {
                GridLines = gridString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                var gridWidth = GridLines[0].Length;
                var gridHeight = GridLines.Length;

                int horizontalMarginWidth = GetMarginSize(windowWidth, gridWidth);
                int verticalMarginHeight = GetMarginSize(windowHeight, gridHeight);

                string emptyLine = new string(' ', windowWidth) + Environment.NewLine;

                VerticalMargin = string.Concat(Enumerable.Repeat(emptyLine, verticalMarginHeight));

                HorizontalMargin = new string(' ', horizontalMarginWidth);

                HorizontalBoundaryLine = HorizontalMargin + '+' + new string('-', gridWidth) + '+' + HorizontalMargin + Environment.NewLine;
            }

            private static int GetMarginSize(int windowSize, int gridWidth)
            {
                int marginSize = (windowSize - gridWidth) / 2 - 1;
                return marginSize < 0 ? 0 : marginSize;
            }
        }
    }
}