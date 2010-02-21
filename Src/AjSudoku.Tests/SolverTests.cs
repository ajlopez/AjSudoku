using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AjSudoku.Tests
{
    [TestClass]
    public class SolverTests
    {
        [TestMethod]
        public void GetPossibleMoves()
        {
            Position position = new Position();
            Solver solver = new Solver();

            List<List<CellInfo>> result = solver.GetPossibleMoves(position);

            Assert.IsNotNull(result);
            Assert.AreEqual(27 * 9 + 81, result.Count);

            foreach (List<CellInfo> cells in result)
                Assert.AreEqual(9, cells.Count);
        }

        [TestMethod]
        public void NotResolveEmptyPosition()
        {
            Position position = new Position();
            Solver solver = new Solver();

            Assert.IsNull(solver.Resolve(position));
        }
    }
}
