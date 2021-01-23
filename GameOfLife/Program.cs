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
            Console.Write(Positioner.InCenter(grid.ToString(border: true), Console.WindowWidth, Console.WindowHeight));

            Console.SetCursorPosition(0, 0);
            Console.Write(grid.Report() + $" To stop press any button.");
        }
    }
}