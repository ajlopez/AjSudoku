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
        private bool[, ,] impossible;

        public Position() 
            : this(9)
        {
        }

        public Position(int size) 
        {
            this.size = size;
            this.range = (int) Math.Sqrt(size);
            this.numbers = new int[size, size];
            this.impossible = new bool[size, size, size];
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

            if (this.impossible[x, y, number - 1])
                throw new InvalidOperationException("Number position is impossible");

            for (int k = 0; k < this.size; k++)
            {
                //if (this.numbers[k, y] == number || this.numbers[x, k] == number)
                //    throw new InvalidOperationException("Number is in the same row or column");
                this.impossible[k, y, number - 1] = true;
                this.impossible[x, k, number - 1] = true;
            }

            int ix = x / this.range;
            int iy = y / this.range;

            ix *= this.range;
            iy *= this.range;

            for (int k = 0; k < this.range; k++)
                for (int j = 0; j < this.range; j++)
                {
                    //if (this.numbers[ix + k, iy + j] == number)
                    //    throw new InvalidOperationException("Number is in the same square");
                    this.impossible[ix + k, iy + j, number - 1] = true;
                }

            this.numbers[x, y] = number;
        }

        public bool Solved
        {
            get
            {
                for (int x = 0; x < this.size; x++)
                    for (int y = 0; y < this.size; y++)
                        if (this.numbers[x, y] == 0)
                            return false;

                return true;
            }
        }

        public bool CanPutNumberAt(int number, int x, int y)
        {
            if (number <= 0 || number > this.size)
                return false;

            if (this.numbers[x, y] != 0)
                return false;

            if (this.impossible[x, y, number - 1])
                return false;

            //for (int k = 0; k < this.size; k++)
            //    if (this.numbers[k, y] == number || this.numbers[x, k] == number)
            //        return false;

            //int ix = x / this.range;
            //int iy = y / this.range;

            //for (int k = 0; k < this.range; k++)
            //    for (int j = 0; j < this.range; j++)
            //        if (this.numbers[ix + k, iy + j] == number)
            //            return false;

            return true;
        }

        public int GetNumberAt(int x, int y)
        {
            return this.numbers[x, y];
        }

        public List<int> GetPossibleNumbersAt(int x, int y)
        {
            List<int> result = new List<int>();

            if (numbers[x, y] != 0)
                return result;

            for (int k = 0; k < this.size; k++)
                if (!impossible[x, y, k])
                {
                    result.Add(k + 1);
                }

            return result;
        }

        public int GetUniqueNumberAt(int x, int y)
        {
            if (numbers[x, y] != 0)
                return 0;

            int npossibles = 0;
            int number = 0;

            for (int k = 0; k < this.size; k++)
                if (!impossible[x, y, k])
                {
                    npossibles++;
                    number = k + 1;
                }

            if (npossibles==1)
                return number;

            return 0;
        }

        public Position Clone()
        {
            Position position = new Position(this.size);

            Array.Copy(this.numbers, position.numbers, this.numbers.Length);
            Array.Copy(this.impossible, position.impossible, this.impossible.Length);

            //for (int x = 0; x < this.size; x++)
            //    for (int y = 0; y < this.size; y++)
            //        position.numbers[x, y] = this.numbers[x, y];

            return position;
        }
    }
}
