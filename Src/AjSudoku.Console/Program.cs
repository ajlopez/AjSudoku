using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AjSudoku;

namespace AjSudoku.Console
{
    class Program
    {
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

            DumpPosition(position);

            Solver solver = new Solver();

            CellInfo ci = solver.Resolve(position);

            while (ci != null)
            {
                System.Console.WriteLine(String.Format("{0} at {1} {2}", ci.number, ci.x+1, ci.y+1));

                position.PutNumberAt(ci.number, ci.x, ci.y);
                DumpPosition(position);

                ci = solver.Resolve(position);
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
