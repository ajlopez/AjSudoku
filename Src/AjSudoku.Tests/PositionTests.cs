namespace AjSudoku.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using AjSudoku;

    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void CreatePosition()
        {
            Position position = new Position();

            Assert.AreEqual(9, position.Size);
            Assert.AreEqual(3, position.Range);
        }

        [TestMethod]
        public void PutANumber()
        {
            Position position = new Position();

            position.PutNumberAt(1, 0, 0);

            Assert.AreEqual(1, position.GetNumberAt(0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNumberIsTooLarge()
        {
            Position position = new Position();

            position.PutNumberAt(10, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNumberIsNegative()
        {
            Position position = new Position();

            position.PutNumberAt(-1, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNumberIsZero()
        {
            Position position = new Position();

            position.PutNumberAt(0, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfCellIsNotEmpty()
        {
            Position position = new Position();

            position.PutNumberAt(1, 0, 0);
            position.PutNumberAt(2, 0, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNumbersAreInARow()
        {
            Position position = new Position();

            position.PutNumberAt(1, 0, 0);
            position.PutNumberAt(1, 5, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNumbersAreInAColumn()
        {
            Position position = new Position();

            position.PutNumberAt(1, 5, 5);
            position.PutNumberAt(1, 5, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RaiseIfNumberAreInSameSquare()
        {
            Position position = new Position();

            position.PutNumberAt(1, 0, 0);
            position.PutNumberAt(1, 2, 2);
        }

        [TestMethod]
        public void DetectColisions()
        {
            Position position = new Position();

            position.PutNumberAt(5, 1, 1);

            Assert.IsFalse(position.CanPutNumberAt(5, 1, 1));
            Assert.IsFalse(position.CanPutNumberAt(5, 0, 0));
            Assert.IsFalse(position.CanPutNumberAt(5, 1, 8));
            Assert.IsFalse(position.CanPutNumberAt(5, 8, 1));
        }

        [TestMethod]
        public void ValidMoves()
        {
            Position position = new Position();

            position.PutNumberAt(5, 1, 1);

            Assert.IsFalse(position.CanPutNumberAt(5, 1, 1));

            Assert.IsTrue(position.CanPutNumberAt(4, 0, 0));
            Assert.IsTrue(position.CanPutNumberAt(4, 1, 8));
            Assert.IsTrue(position.CanPutNumberAt(4, 8, 1));

            Assert.IsTrue(position.CanPutNumberAt(4, 8, 8));
            Assert.IsTrue(position.CanPutNumberAt(4, 2, 8));
            Assert.IsTrue(position.CanPutNumberAt(4, 8, 2));
        }

        [TestMethod]
        public void Clone()
        {
            Position position = new Position();

            position.PutNumberAt(1, 0, 0);
            position.PutNumberAt(2, 1, 0);
            position.PutNumberAt(3, 2, 0);

            position.PutNumberAt(3, 3, 3);
            position.PutNumberAt(4, 4, 3);
            position.PutNumberAt(5, 5, 3);

            position.PutNumberAt(6, 6, 6);
            position.PutNumberAt(7, 7, 6);
            position.PutNumberAt(8, 8, 6);

            Position newposition = position.Clone();

            Assert.AreEqual(position.Size, newposition.Size);
            Assert.AreEqual(position.Range, newposition.Range);

            for (int x = 0; x < position.Size; x++)
                for (int y = 0; y < position.Size; y++)
                    Assert.AreEqual(newposition.GetNumberAt(x, y), position.GetNumberAt(x, y));
        }
    }
}
