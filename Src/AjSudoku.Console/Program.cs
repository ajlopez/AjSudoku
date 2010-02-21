using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjSudoku;

namespace AjSudoku.Console
{
    class Program
    {
        static Stack<Position> positions = new Stack<Position>();
        static Solver solver = new Solver();

        static void Main(string[] args)
        {
            string gametxt = args[0];
            Position position = new Position();

            for (int k = 0; k < gametxt.Length; k++)
            {
                int x = k % 9;
                int y = k / 9;

                char cell = gametxt[k];

                if (cell >= '1' && cell <= '9')
                    position.PutNumberAt(cell - '0', x, y);
            }

            positions.Push(position);

            while (positions.Count>0)
            {
                position = positions.Pop();

                DumpPosition(position);

                CellInfo ci = solver.Resolve(position);

                while (ci != null)
                {
                    System.Console.WriteLine(String.Format("{0} at {1} {2}", ci.Number, ci.X + 1, ci.Y + 1));

                    position.PutNumberAt(ci.Number, ci.X, ci.Y);
                    DumpPosition(position);

                    ci = solver.Resolve(position);
                }

                if (position.Solved)
                    break;

                List<List<CellInfo>> results = solver.GetPossibleMoves(position);

                if (results.Count == 0)
                {
                    System.Console.WriteLine("No Branch");
                    continue;
                }

                bool hasbranches = false;

                foreach (List<CellInfo> cells in results)
                {
                    if (cells.Count != 2)
                        continue;

                    foreach (CellInfo cell in cells)
                    {
                        System.Console.WriteLine(String.Format("Branch {0} at {1} {2}", cell.Number, cell.X + 1, cell.Y + 1));
                        Position newposition = position.Clone();
                        newposition.PutNumberAt(cell.Number, cell.X, cell.Y);
                        DumpPosition(newposition);
                        positions.Push(newposition);
                    }

                    hasbranches = true;

                    break;
                }

                if (!hasbranches)
                    System.Console.WriteLine("No Branch 2");
            }

            System.Console.ReadLine();
        }

        private static void DumpPosition(Position p)
        {
            for (int y = 0; y < p.Size; y++)
            {
                for (int x = 0; x < p.Size; x++)
                {
                    int number = p.GetNumberAt(x, y);

                    char cell = (char) (number + '0');

                    if (number == 0)
                        cell = '.';

                    System.Console.Write(' ');
                    System.Console.Write(cell);
                }
                System.Console.WriteLine();
            }
        }
    }
}
