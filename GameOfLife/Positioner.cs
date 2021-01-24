using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    internal static class Positioner
    {
        public static string InCenter(string gridString, int windowWidth, int windowHeight)
        {
            var gridLines = gridString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var gridWidth = gridLines[0].Length;
            var gridHeight = gridLines.Length;

            int horizontalMarginWidth = GetMarginSize(windowWidth, gridWidth);
            int verticalMarginHeight = GetMarginSize(windowHeight, gridHeight);

            string emptyLine = new string(' ', windowWidth) + Environment.NewLine;

            var verticalMargin = string.Concat(Enumerable.Repeat(emptyLine, verticalMarginHeight));

            var horizontalMargin = new string(' ', horizontalMarginWidth);

            var totalSize = windowWidth * windowHeight;

            return Compose(gridLines, verticalMargin, horizontalMargin, horizontalMargin, verticalMargin, totalSize);
        }

        private static int GetMarginSize(int windowSize, int gridWidth)
        {
            int marginSize = (windowSize - gridWidth) / 2 - 1;
            return marginSize < 0 ? 0 : marginSize;
        }

        private static string Compose(string[] gridLines, string topMargin, string leftMargin, string rightMargin, string bottomMargin, int totalSize)
        {
            /*
                topMargin
             ---------------
             l  |        | r
             e  |        | i
             f  |  grid  | g
             t  |        | h
                |        | t
             ---------------
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