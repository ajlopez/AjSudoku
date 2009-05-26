using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjSudoku
{
    public class Position
    {
        private int size;
        private int range;

        private int [,] numbers;

        public Position() 
            : this(9)
        {
        }

        public Position(int size) 
        {
            this.size = size;
            this.range = (int) Math.Sqrt(size);
            this.numbers = new int[size, size];
        }

        public int Size
        {
            get
            {
                return this.size;
            }
        }

        public int Range
        {
            get
            {
                return this.range;
            }
        }

        public void PutNumberAt(int number, int x, int y)
        {
            if (number <= 0 || number > this.size)
                throw new InvalidOperationException("Invalid number");

            if (this.numbers[x, y] != 0)
                throw new InvalidOperationException("Cell is not empty");

            for (int k = 0; k < this.size; k++)
                if (this.numbers[k, y] == number || this.numbers[x, k] == number)
                    throw new InvalidOperationException("Number is in the same row or column");

            int ix = x / this.range;
            int iy = y / this.range;

            for (int k = 0; k < this.range; k++)
                for (int j = 0; j < this.range; j++)
                    if (this.numbers[ix + k, iy + j] == number)
                        throw new InvalidOperationException("Number is in the same square");

            this.numbers[x, y] = number;
        }

        public bool CanPutNumberAt(int number, int x, int y)
        {
            if (number <= 0 || number > this.size)
                return false;

            if (this.numbers[x, y] != 0)
                return false;

            for (int k = 0; k < this.size; k++)
                if (this.numbers[k, y] == number || this.numbers[x, k] == number)
                    return false;

            int ix = x / this.range;
            int iy = y / this.range;

            for (int k = 0; k < this.range; k++)
                for (int j = 0; j < this.range; j++)
                    if (this.numbers[ix + k, iy + j] == number)
                        return false;

            return true;
        }

        public int GetNumberAt(int x, int y)
        {
            return this.numbers[x, y];
        }

        public Position Clone()
        {
            Position position = new Position(this.size);

            for (int x = 0; x < this.size; x++)
                for (int y = 0; y < this.size; y++)
                    position.numbers[x, y] = this.numbers[x, y];

            return position;
        }
    }
}
