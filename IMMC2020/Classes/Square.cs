namespace IMMC2020
{
    using System.Collections.Generic;

    public class Square
    {
        private double cost;

        public List<Square> AdjacentSquares
        {
            get
            {
                List<Square> res = new();

                res.Add(this.GetNthSquareUp(1));
                res.Add(this.GetNthSquareDown(1));
                res.Add(this.GetNthSquareLeft(1));
                res.Add(this.GetNthSquareRight(1));
                res.Add((this.GetNthSquareUp(1) == null ? null : this.GetNthSquareUp(1).GetNthSquareLeft(1)));
                res.Add((this.GetNthSquareUp(1) == null ? null : this.GetNthSquareUp(1).GetNthSquareRight(1)));
                res.Add((this.GetNthSquareDown(1) == null ? null : this.GetNthSquareDown(1).GetNthSquareLeft(1)));
                res.Add((this.GetNthSquareDown(1) == null ? null : this.GetNthSquareDown(1).GetNthSquareRight(1)));

                res.RemoveAll(i => i == null);

                return res;
            }
        }

        public double CostIncurred
        {
            get
            {
                if (this.Product != null && this.Density > this.Product.HumanThreshold && this.Product.Quantity > 0)
                {
                    this.cost += this.Product.DiscountedPrice;
                }

                return this.cost;
            }
        }

        public double Density { get; set; }
        public Grid Grid { get; set; }
        public Product? Product { get; set; }
        public int X { get; }

        public int Y { get; }

        public Square(Grid grid, int number, int length, int breadth)
        {
            this.Grid = grid;
            this.X = (number % length);
            this.Y = (number / breadth);
        }

        /// <summary>
        /// Returns the <see cref="Square"/> n squares below of the current <see cref="Square"/>.
        /// </summary>
        /// <param name="n">The number of squares.</param>
        /// <returns>The <see cref="Square"/> n squares below of the current <see cref="Square"/>.</returns>
        public Square? GetNthSquareDown(int n)
        {
            int y = this.Y - n;
            int x = this.X;

            if (y < 0 || y >= this.Grid.Breadth)
            {
                return null;
            }

            int sqID = (y * this.Grid.Length) + x;

            if (sqID >= this.Grid.Length * this.Grid.Breadth || sqID < 0)
            {
                return null;
            }

            return this.Grid.Squares[sqID];
        }

        /// <summary>
        /// Returns the <see cref="Square"/> n squares left of the current <see cref="Square"/>.
        /// </summary>
        /// <param name="n">The number of squares.</param>
        /// <returns>The <see cref="Square"/> n squares left of the current <see cref="Square"/>.</returns>
        public Square? GetNthSquareLeft(int n)
        {
            int y = this.Y;
            int x = this.X - n;

            if (x < 0 || x >= this.Grid.Length)
            {
                return null;
            }

            int sqID = (y * this.Grid.Length) + x;

            if (sqID >= this.Grid.Length * this.Grid.Breadth || sqID < 0)
            {
                return null;
            }

            return this.Grid.Squares[sqID];
        }

        /// <summary>
        /// Returns the <see cref="Square"/> n squares right of the current <see cref="Square"/>.
        /// </summary>
        /// <param name="n">The number of squares.</param>
        /// <returns>The <see cref="Square"/> n squares right of the current <see cref="Square"/>.</returns>
        public Square? GetNthSquareRight(int n)
        {
            int y = this.Y;
            int x = this.X + n;

            if (x < 0 || x >= this.Grid.Length)
            {
                return null;
            }

            int sqID = (y * this.Grid.Length) + x;

            if (sqID >= this.Grid.Length * this.Grid.Breadth || sqID < 0)
            {
                return null;
            }

            return this.Grid.Squares[sqID];
        }

        /// <summary>
        /// Returns the <see cref="Square"/> n squares above of the current <see cref="Square"/>.
        /// </summary>
        /// <param name="n">The number of squares.</param>
        /// <returns>The <see cref="Square"/> n squares above of the current <see cref="Square"/>.</returns>
        public Square? GetNthSquareUp(int n)
        {
            int y = this.Y + n;
            int x = this.X;

            if (y < 0 || y >= this.Grid.Breadth)
            {
                return null;
            }

            int sqID = (y * this.Grid.Length) + x;

            if (sqID >= this.Grid.Length * this.Grid.Breadth || sqID < 0)
            {
                return null;
            }

            return this.Grid.Squares[sqID];
        }

        public override string ToString()
        {
            return $"({this.X}, {this.Y})";
        }
    }
}