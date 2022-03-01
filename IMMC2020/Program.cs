namespace IMMC2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Be brave, be honest to yourself and stop this trush talkings!!!
    /// </summary>
    public class Program
    {
        private static void Main()
        {
            SimulateLayoutPermutations(9, 7, "X X 4 X 3 X X X X X X X X X 5 X X X X X 2 X X X 9 X X X X X 0 X 8 X X X X X 7 X X X 6 X X X X X 1 X X X X X X X X exit X cashier X entrance X");
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static List<string> LayoutCombinations(string layout)
        {
            List<string> res = new();

            List<string> squares = layout.Split(" ").ToList().Select(i => i.ToLower()).ToList();

            int numProd = 0;

            for (int i = 0; i < squares.Count; i++)
            {
                string sq = squares[i];

                List<string> forbidden = new();
                forbidden.Add("x");
                forbidden.Add("cashier");
                forbidden.Add("entrance");
                forbidden.Add("exit");

                if (!forbidden.Contains(sq))
                {
                    squares[i] = "P";
                    numProd++;
                }
            }

            IEnumerable<IEnumerable<int>> combinations = GetPermutations(Enumerable.Range(1, numProd), numProd);

            foreach (IEnumerable<int> com in combinations)
            {
                List<int> c = com.ToList();

                List<string> localSq = squares.ToList();

                int count = 0;

                for (int i = 0; i < localSq.Count; i++)
                {
                    if (localSq[i] == "P")
                    {
                        localSq[i] = (c[count] - 1).ToString();
                        count++;
                    }
                }

                res.Add(string.Join(" ", localSq));
            }

            return res;
        }

        public static string LayoutString(int length, int breadth, string layout)
        {
            string res = string.Empty;
            int count = 0;

            List<string> l = layout.Split(" ").ToList().Select(i => i.ToLower()).ToList();

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < breadth; j++)
                {
                    switch (l[count])
                    {
                        case "x":
                            res += "[ ]";
                            break;

                        case "cashier":
                            res += "[C]";
                            break;

                        case "entrance":
                            res += "[E]";
                            break;

                        case "exit":
                            res += "[e]";
                            break;

                        default:
                            res += $"[{l[count]}]";
                            break;
                    }
                    count++;
                }

                res += "\n";
            }

            return res;
        }

        public static void SimulateLayoutPermutations(int length, int breadth, string layout)
        {
            (string bestL, double bestC) = (string.Empty, double.MaxValue);

            foreach (string l in LayoutCombinations(layout))
            {
                List<Product> products = new();
                Grid grid = new(length, breadth);

                Simulation s = new(products, grid, 100, 1);

                // simulation, name, retail, discount, quantity, rating
                s.Products.Add(new(s, "Product 1", 1000, 10, 50, 4.5));
                s.Products.Add(new(s, "Product 2", 1500, 20, 80, 4.2));
                s.Products.Add(new(s, "Product 3", 2000, 17, 20, 4.9));
                s.Products.Add(new(s, "Product 4", 100, 53, 60, 3.7));
                s.Products.Add(new(s, "Product 5", 1373, 71, 26, 4.2));
                s.Products.Add(new(s, "Product 6", 1429, 12, 61, 3.2));
                s.Products.Add(new(s, "Product 7", 6451, 25, 16, 5));
                s.Products.Add(new(s, "Product 8", 8352, 16, 2, 4.5));
                s.Products.Add(new(s, "Product 9", 9218, 47, 15, 4.2));
                s.Products.Add(new(s, "Product 10", 5620, 07, 51, 2.6));

                s.SetLayout(l);

                double cost = s.CostIncurred;

                if (cost < bestC)
                {
                    bestL = l;
                    bestC = cost;
                }
            }

            Console.WriteLine(LayoutString(length, breadth, bestL));
            Console.WriteLine(bestC);
        }
    }
}