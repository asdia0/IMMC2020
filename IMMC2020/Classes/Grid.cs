namespace IMMC2020
{
    using System.Collections.Generic;
    using System.Linq;

    public class Grid
    {
        public int Breadth { get; set; }
        public int Length { get; set; }
        public List<Square> Squares { get; set; }

        public Grid(int length, int breadth)
        {
            this.Length = length;
            this.Breadth = breadth;

            this.Squares = new();

            for (int i = 0; i < length * breadth; i++)
            {
                Squares.Add(new(this, i, this.Length, this.Breadth));
            }
        }

        /// <summary>
        /// An implementation of Dijkstra's algorithm to find the optimal path between two squares.
        /// </summary>
        public List<Square> Path(Square source, Square target)
        {
            //System.Console.WriteLine((source.X, source.Y));

            List<Square> Q = new();

            Dictionary<Square, Square?> prev = new();
            Dictionary<Square, int> dist = new();

            foreach (Square s in this.Squares)
            {
                dist.Add(s, int.MaxValue);
                prev.Add(s, null);
                Q.Add(s);
            }

            dist[source] = 0;

            while (Q.Count != 0)
            {
                Square u = dist.Where(i => Q.Contains(i.Key)).OrderBy(i => i.Value).FirstOrDefault().Key;

                Q.Remove(u);

                if (u == target)
                {
                    break;
                }

                foreach (Square v in u.AdjacentSquares)
                {
                    if (Q.Contains(v))
                    {
                        int alt = dist[u] + 1;

                        if (alt < dist[v])
                        {
                            dist[v] = alt;
                            prev[v] = u;
                        }
                    }
                }
            }

            List<Square> S = new();
            Square t = target;

            if (prev[t] != null || t == source)
            {
                while (t != null)
                {
                    S.Add(t);
                    t = prev[t];
                }
            }

            S.Reverse();

            return S;
        }
    }
}