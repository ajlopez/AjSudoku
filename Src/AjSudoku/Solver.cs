namespace AjSudoku
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Solver
    {
        public CellInfo Resolve(Position position)
        {
            CellInfo ci = null;

            for (int number = 1; number <= position.Size; number++)
            {
                ci = this.Resolve(number, position);

                if (ci != null)
                    return ci;
            }

            int n;

            for (int x = 0; x < position.Size; x++)
                for (int y = 0; y < position.Size; y++)
                    if ((n = position.GetUniqueNumberAt(x, y)) > 0)
                        return new CellInfo() { Number = n, X = x, Y = y };

            return null;
        }

        public CellInfo Resolve(int number, Position position)
        {
            for (int x = 0; x < position.Size; x++)
            {
                int npossible = 0;
                int ny = 0;

                for (int y = 0; y < position.Size; y++)
                    if (position.CanPutNumberAt(number, x, y))
                    {
                        npossible++;
                        ny = y;
                    }

                if (npossible == 1)
                    return new CellInfo() { Number = number, X = x, Y = ny };
            }

            for (int y = 0; y < position.Size; y++)
            {
                int npossible = 0;
                int nx = 0;

                for (int x = 0; x < position.Size; x++)
                    if (position.CanPutNumberAt(number, x, y))
                    {
                        npossible++;
                        nx = x;
                    }

                if (npossible == 1)
                    return new CellInfo() { Number = number, X = nx, Y = y };
            }

            for (int ix = 0; ix < position.Size; ix += position.Range)
                for (int iy = 0; iy < position.Size; iy += position.Range)
                {
                    int npossible = 0;
                    int nx = 0;
                    int ny = 0;

                    for (int x = 0; x < position.Range; x++)
                        for (int y = 0; y < position.Range; y++)
                        {
                            if (position.CanPutNumberAt(number, ix + x, iy + y))
                            {
                                npossible++;
                                nx = ix + x;
                                ny = iy + y;
                            }
                        }

                    if (npossible == 1)
                        return new CellInfo() { Number = number, X = nx, Y = ny };
                }

            return null;
        }

        public List<List<CellInfo>> GetPossibleMoves(Position position)
        {
            List<List<CellInfo>> possibles = new List<List<CellInfo>>();

            for (int number = 1; number <= position.Size; number++)
            {
                possibles.AddRange(this.GetPossibleMoves(number, position));
            }

            for (int x = 0; x < position.Size; x++)
                for (int y = 0; y < position.Size; y++)
                {
                    List<CellInfo> cells = new List<CellInfo>();

                    foreach (int number in position.GetPossibleNumbersAt(x, y))
                    {
                        cells.Add(new CellInfo() { Number = number, X = x, Y = y });
                    }

                    if (cells.Count > 0)
                        possibles.Add(cells);
                }

            return possibles;
        }

        public List<List<CellInfo>> GetPossibleMoves(int number, Position position)
        {
            List<List<CellInfo>> results = new List<List<CellInfo>>();

            for (int x = 0; x < position.Size; x++)
            {
                List<CellInfo> cells = new List<CellInfo>();

                for (int y = 0; y < position.Size; y++)
                    if (position.CanPutNumberAt(number, x, y))
                    {
                        cells.Add(new CellInfo() { Number = number, X = x, Y = y });
                    }

                if (cells.Count > 0)
                    results.Add(cells);
            }

            for (int y = 0; y < position.Size; y++)
            {
                List<CellInfo> cells = new List<CellInfo>();

                for (int x = 0; x < position.Size; x++)
                    if (position.CanPutNumberAt(number, x, y))
                    {
                        cells.Add(new CellInfo() { Number = number, X = x, Y = y });
                    }

                if (cells.Count > 0)
                    results.Add(cells);
            }

            for (int ix = 0; ix < position.Size; ix += position.Range)
                for (int iy = 0; iy < position.Size; iy += position.Range)
                {
                    List<CellInfo> cells = new List<CellInfo>();

                    for (int x = 0; x < position.Range; x++)
                        for (int y = 0; y < position.Range; y++)
                        {
                            if (position.CanPutNumberAt(number, ix + x, iy + y))
                            {
                                cells.Add(new CellInfo() { Number = number, X = ix + x, Y = iy + y });
                            }
                        }

                    if (cells.Count > 0)
                        results.Add(cells);
                }

            return results;
        }
    }
}
