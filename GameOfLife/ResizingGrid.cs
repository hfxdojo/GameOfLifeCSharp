﻿namespace GameOfLife
{
    internal class ResizingGrid<T> : Grid<T> where T : AbstractLifeObject, new()
    {
        private int _shiftRows;
        private int _shiftCols;

        public ResizingGrid(T[,] objects) : base(objects)
        {
        }

        public override T[,] NextGeneration()
        {
            var nextGeneration = base.NextGeneration();

            return PopulateEmptyPositions(Resize(nextGeneration));
        }

        public override string Report()
        {
            return base.Report() + $" Top left corner coords ({_shiftRows}, {_shiftCols}).";
        }

        private T[,] Resize(T[,] lifeObjects)
        {
            var rows = lifeObjects.GetLength(0);
            var cols = lifeObjects.GetLength(1);

            // positive means extend
            var (top, bottom, left, right) = GetSizeAdjustments(lifeObjects, rows, cols);

            var resized = new T[rows + top + bottom, cols + left + right];

            for (var row = 1 - top; row < rows + bottom - 1; row++)
                for (var col = 1 - left; col < cols + right - 1; col++)
                    resized[row + top, col + left] = lifeObjects[row, col];

            _shiftRows -= top;
            _shiftCols -= left;

            return resized;
        }

        private static (int top, int bottom, int left, int right) GetSizeAdjustments(T[,] lifeObjects, int rows, int cols)
        {
            var top = GetSizeAdjustmentTop(lifeObjects, rows, cols);
            var bottom = GetSizeAdjustmentBottom(lifeObjects, rows, cols);
            var left = GetSizeAdjustmentLeft(lifeObjects, rows, cols);
            var right = GetSizeAdjustmentRight(lifeObjects, rows, cols);

            return (top, bottom, left, right);
        }

        private static T[,] PopulateEmptyPositions(T[,] lifeObjects)
        {
            for (var row = 0; row < lifeObjects.GetLength(0); row++)
                for (var col = 0; col < lifeObjects.GetLength(1); col++)
                    lifeObjects[row, col] ??= new T();

            return lifeObjects;
        }

        private static int GetSizeAdjustmentTop(T[,] lifeObjects, int rows, int cols)
        {
            for (var row = 0; row < rows; row++)
                for (var col = 0; col < cols; col++)
                    if (lifeObjects[row, col].Live)
                        return IsWithinTolerance(row) ? 0 : -row + 1;

            return 0;
        }

        private static int GetSizeAdjustmentBottom(T[,] lifeObjects, int rows, int cols)
        {
            for (var row = rows - 1; row >= 0; row--)
                for (var col = 0; col < cols; col++)
                    if (lifeObjects[row, col].Live)
                        return IsWithinTolerance(rows - row) ? 0 : -rows + row + 2;
            return 0;
        }

        private static int GetSizeAdjustmentLeft(T[,] lifeObjects, int rows, int cols)
        {
            for (var col = 0; col < cols; col++)
                for (var row = 0; row < rows; row++)
                    if (lifeObjects[row, col].Live)
                        return IsWithinTolerance(col) ? 0 : -col + 1;

            return 0;
        }

        private static int GetSizeAdjustmentRight(T[,] lifeObjects, int rows, int cols)
        {
            for (var col = cols - 1; col >= 0; col--)
                for (var row = 0; row < rows; row++)
                    if (lifeObjects[row, col].Live)
                        return IsWithinTolerance(cols - col) ? 0 : -cols + col + 2;

            return 0;
        }

        private static bool IsWithinTolerance(int value) => value is >= 1 and <= 5;
    }
}