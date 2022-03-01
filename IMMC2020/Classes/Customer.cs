namespace IMMC2020
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Customer
    {
        public List<(Product, double)> Bought { get; set; }
        public List<Product> DesiredProducts { get; set; }
        public List<(Product, double)> Inventory { get; set; }
        public List<Square> Path { get; set; }
        public Simulation Simulation { get; set; }

        public Customer(Simulation simulation, int numberOfDesiredProducts, List<Product> availableProducts)
        {
            this.Simulation = simulation;
            this.Inventory = new();
            this.Bought = new();

            for (int i = 0; i < numberOfDesiredProducts; i++)
            {
                HashSet<(Product, int)> distinct = new();

                int count = 0;

                while (distinct.Count < numberOfDesiredProducts)
                {
                    Dictionary<Product, double> weights = availableProducts.ToDictionary(i => i, i => i.Desirability / availableProducts.Sum(i => i.Desirability));

                    distinct.Add((weights.RandomElementByWeight(i => (float)i.Value).Key, count));

                    count++;
                }

                this.DesiredProducts = distinct.OrderBy(i => i.Item2).Select(i => i.Item1).ToList();
            }

            this.GetPath();
        }

        public void GetPath()
        {
            List<Square> targets = new();

            targets.Add(this.Simulation.Entrance);

            foreach (Product p in this.DesiredProducts)
            {
                targets.Add(this.Simulation.Grid.Squares.Where(i => i.Product == p).FirstOrDefault());
            }

            targets.Add(this.Simulation.Cashier);

            targets.Add(this.Simulation.Exit);

            List<Square> res = new();

            for (int i = 0; i < targets.Count - 1; i++)
            {
                List<Square> path = Simulation.Grid.Path(targets[i], targets[i + 1]);

                if (i != 0)
                {
                    path.RemoveAt(0);
                }

                res.AddRange(path);
            }

            Random rand = new();
            if (rand.Next(100) == 42)
            {
                int index = rand.Next(res.Count);
                res.Insert(index, res[index]);
            }

            this.Path = res;
        }
    }
}