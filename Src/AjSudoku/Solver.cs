using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjSudoku
{
    public class Solver
    {
        public CellInfo Resolve(Position position)
        {
            CellInfo ci;

            for (int number = 1; number <= position.Size; number++)
            {
                ci = Resolve(number, position);
            }
        }

        public CellInfo Resolve(int number, Position position)
        {

        }
    }
}
