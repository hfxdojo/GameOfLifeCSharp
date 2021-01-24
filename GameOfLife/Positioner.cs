using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    internal static class Positioner
    {
        public static string InCenter(string gridString, int windowWidth, int windowHeight)
        {
            var (gridLines, gridWidth, gridHeight) = Decompose(gridString);

            int horizontalMarginWidth = GetMarginSize(windowWidth, gridWidth);
            int verticalMarginHeight = GetMarginSize(windowHeight, gridHeight);

            string emptyLine = new string(' ', windowWidth) + Environment.NewLine;

            var verticalMargin = string.Concat(Enumerable.Repeat(emptyLine, verticalMarginHeight));

            var horizontalMargin = new string(' ', horizontalMarginWidth);

            return Compose(gridLines,
                           verticalMargin,
                           horizontalMargin,
                           horizontalMargin,
                           verticalMargin,
                           totalSize: windowWidth * windowHeight);
        }

        private static (string[] lines, int width, int height) Decompose(string gridString)
        {
            var gridLines = gridString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var gridWidth = gridLines[0].Length;
            var gridHeight = gridLines.Length;

            return (gridLines, gridWidth, gridHeight);
        }

        private static int GetMarginSize(int windowSize, int gridWidth)
        {
            int marginSize = (windowSize - gridWidth) / 2 - 1;
            return marginSize < 0 ? 0 : marginSize;
        }

        private static string Compose(string[] gridLines,
                                      string topMargin,
                                      string leftMargin,
                                      string rightMargin,
                                      string bottomMargin,
                                      int totalSize)
        {
            /*
                   topMargin
             ----------------------
             left  |  grid  | right
             left  |  grid  | right
             left  |  grid  | right
             ----------------------
                  bottomMargin
             */
            var strBuilder = new StringBuilder(totalSize);
            
            strBuilder.Append(topMargin);

            foreach (var gridLine in gridLines)
            {
                strBuilder.AppendLine(leftMargin + gridLine + rightMargin);
            }

            strBuilder.Append(bottomMargin);

            return strBuilder.ToString();
        }
    }
}