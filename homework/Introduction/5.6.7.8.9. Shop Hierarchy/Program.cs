namespace _5._6._7._8._9._Shop_Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ShopContext())
            {
                ClearDatabase(context);
                FillSalesmen(context);
                FillItems(context);
                ReadCommands(context);
                //PrintSalesmenWithCustomersCount(context);
                //PrintCustomersWithOrdersAndReviews(context);
                //PrintOrdersAndReviews(context);
                //PrintCustomerInfo(context);
                PrintOrdersWithMoreThanOneItem(context);
            }
        }

        //------------------------- COMMAND FUNCTIONS -----------------------------//

        private static void ReadCommands(ShopContext context)
        {
            var line = Console.ReadLine();
            while (!line.Equals("END"))
            {
                string[] data = line.Split('-');
                string cmd = data[0];
                switch (cmd)
                {
                    case "register":
                        Register(context, data[1]);
                        break;
                    case "order":
                        Order(context, data[1]);
                        break;
                    case "review":
                        Review(context, data[1]);
                        break;
                }
                line = Console.ReadLine();
            }
        }

        private static void Order(ShopContext context, string line)
        {
            string[] data = line.Split(';');
            int customerId = int.Parse(data[0]);

            var order = new Order() { CustomerId = customerId };

            for (int i = 1; i < data.Length; i++)
            {
                var itemId = int.Parse(data[i]);
                order.Items.Add(new ItemsOrders
                {
                    ItemId = itemId
                });
            }

            context.Add(order);

            context.SaveChanges();
        }

        private static void Review(ShopContext context, string line)
        {
            string[] data = line.Split(';');
            int customerId = int.Parse(data[0]);
            int itemId = int.Parse(data[1]);

            var review = new Review() { CustomerId = customerId, ItemId = itemId };
            
            context.Add(review);

            context.SaveChanges();
        }

        private static void Register(ShopContext context, string line)
        {
            string[] registerTokens = line.Split(';');

            string name = registerTokens[0];
            int salesmenId = int.Parse(registerTokens[1]);

            context.Add(new Customer() { Name = name, SalesmenId = salesmenId });

            context.SaveChanges();
        }

        //-------------------------- FILL FUNCTIONS -------------------------------//

        private static void FillItems(ShopContext context)
        {
            var line = Console.ReadLine();
            while (!line.Equals("END"))
            {
                string[] data = line.Split(';');
                var name = data[0];
                var price = decimal.Parse(data[1]);

                Item item = new Item() { Name = name, Price = price };
                context.Items.Add(item);

                line = Console.ReadLine();
            }

            context.SaveChanges();
        }

        private static void FillSalesmen(ShopContext context)
        {
            string[] salesmenNames = Console.ReadLine().Split(';');

            foreach (var name in salesmenNames)
            {
                Salesmen salesmen = new Salesmen() { Name = name };
                context.Salesmens.Add(salesmen);
            }

            context.SaveChanges();
        }

        private static void ClearDatabase(ShopContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        //------- ------------------ PRINT FUNCTIONS -----------------------------//

        private static void PrintOrdersWithMoreThanOneItem(ShopContext context)
        {
            var id = int.Parse(Console.ReadLine());

            var customer = context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    Orders = c.Orders.Count(o => o.Items.Count > 1)
                })
                .FirstOrDefault();

            Console.WriteLine($"Orders: {customer.Orders}");
        }

        private static void PrintCustomerInfo(ShopContext context)
        {
            var id = int.Parse(Console.ReadLine());

            var customer = context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count,
                    Salesmen = c.Salesmen.Name
                })
                .FirstOrDefault();

            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Orders: {customer.Orders}");
            Console.WriteLine($"Reviews: {customer.Reviews}");
            Console.WriteLine($"Salesmen: {customer.Salesmen}");
        }

        private static void PrintOrdersAndReviews(ShopContext context)
        {
            var id = int.Parse(Console.ReadLine());

            var customer = context.Customers.FirstOrDefault(c => c.Id == id);

            var orders = customer.Orders
                .Select(o => new
                {
                    o.Id,
                    Items = o.Items.Count
                })
                .OrderBy(o => o.Id)
                .ToList();

            foreach (var o in orders)
            {
                Console.WriteLine($"order {o.Id}: {o.Items} items");
            }

            var reviews = customer.Reviews.Count;
            Console.WriteLine($"reviews: {reviews}");
        }

        private static void PrintCustomersWithOrdersAndReviews(ShopContext context)
        {
            var customers = context.Customers
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .OrderByDescending(c => c.Orders)
                .ThenByDescending(c => c.Reviews)
                .ToList();

            foreach (var c in customers)
            {
                Console.WriteLine(c.Name);
                Console.WriteLine($"Orders: {c.Orders}");
                Console.WriteLine($"Reviews: {c.Reviews}");
            }
        }

        private static void PrintSalesmenWithCustomersCount(ShopContext context)
        {
            var salesmen = context.Salesmens
                .Select(s => new
                {
                    s.Name,
                    Customers = s.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var s in salesmen)
            {
                Console.WriteLine($"{s.Name} - {s.Customers} customers");
            }
        }
    }
}
