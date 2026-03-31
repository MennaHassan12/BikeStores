using BikeStores.Data;
using Microsoft.EntityFrameworkCore;
namespace BikeStores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //1-List all customers' first and last names along with their email addresses
           
            var customers = db.Customers
                .Select(c => new
                    {
                        FullName = c.FirstName + " " + c.LastName,
                        c.Email
                    });

                    foreach (var c in customers)
                    {
                        Console.WriteLine($"Name: {c.FullName} - Email: {c.Email}");}
                    


            //2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3).
            
            var orders = db.Orders
               .Where(o => o.StaffId == 3);

                 foreach (var o in orders)
                   {
                         Console.WriteLine(o.OrderId);

                        }
           

            //3- Get all products that belong to a category named "Mountain Bikes".

             var products = db.Products
                 .Where(p => p.Category.CategoryName == "Mountain Bikes");

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId} | {p.ProductName} | Price:{p.ListPrice} | Year:{p.ModelYear}");
            }
           


            //4-Count the total number of orders per store.
           
                var ordersPerStore = db.Orders
                    .GroupBy(o => o.StoreId)
                    .Select(g => new
                        {
                            StoreId = g.Key,
                            Count = g.Count()
                        });

                                foreach (var s in ordersPerStore)
                                {
                                    Console.WriteLine($"Store({s.StoreId}) => Orders : {s.Count}");
                                }
           

            //5- List all orders that have not been shipped yet (shipped_date is null).
            
              var orders = db.Orders
                .Where(o => o.ShippedDate == null);

            foreach (var o in orders)
            {
                Console.WriteLine($"ID: {o.OrderId}");
                Console.WriteLine($"Customer: {o.CustomerId}");
                Console.WriteLine($"Status: {o.OrderStatus}");
                Console.WriteLine($"Order Date: {o.OrderDate}");
                Console.WriteLine($"Required Date: {o.RequiredDate}");
                Console.WriteLine($"Shipped Date: {o.ShippedDate}");
                Console.WriteLine($"Store: {o.StoreId}");
                Console.WriteLine($"Staff: {o.StaffId}");
                Console.WriteLine("----------------------");
            }
            

            //6- Display each customer’s full name and the number of orders they have placed.

            var result = db.Customers
                   .Select(c => new
                   {
                       Name = c.FirstName + " " + c.LastName,
                       OrdersCount = c.Orders.Count()
                   });
           foreach (var r in result)
           {
               Console.WriteLine($"{r.Name} - {r.OrdersCount}");
           }
            

            //7- List all products that have never been ordered (not found in order_items).
             var products = db.Products
                 .Where(p => !p.OrderItems.Any());
                 foreach (var p in products)
                 {
                     Console.WriteLine($"{p.ProductId} | {p.ProductName} | Price:{p.ListPrice}");
                 }
            

            //8- Display products that have a quantity of less than 5 in any store stock.
             
            var products = db.Stocks
                    .Where(s => s.Quantity < 5)
                    .Select(s => s.Product);
            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductName} | Price:{p.ListPrice}");
            }


            //9- Retrieve the first product from the products table

            var p = db.Products.FirstOrDefault();

            Console.WriteLine($"{p.ProductId} | {p.ProductName} | Price:{p.ListPrice}");


            //10- Retrieve all products from the products table with a certain model year.

            int year = 2017;

            var products = db.Products
                .Where(p => p.ModelYear == year);
            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductId} | {p.ProductName} | Price:{p.ListPrice}");
            }

            //11- Display each product with the number of times it was ordered.
            var products = db.Products;

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductName} => Ordered: {p.OrderItems.Count()} times");
            }

            //12- Count the number of products in a specific category

            var count = db.Products
                .Count(p => p.CategoryId == 1);
            var count2 = db.Products
              .Count(p => p.Category.CategoryName == "Mountain Bikes");

            Console.WriteLine($"Total Products: {count}");

            //13- Calculate the average list price of products.
            var avg = db.Products.Average(p => p.ListPrice);

            Console.WriteLine($"Average Price: {avg}");

            14 - Retrieve a specific product from the products table by ID
            var p = db.Products.FirstOrDefault(p => p.ProductId == 5);

            Console.WriteLine($"{p.ProductId} | {p.ProductName} | Price:{p.ListPrice}");

            //15- List all products that were ordered with a quantity greater than 3 in any order.
            var products = db.OrderItems
                .Where(o => o.Quantity > 3)
                .Select(o => o.Product);


            //16- Display each staff member’s name and how many orders they processed.
            var staffs = db.Staffs
                .Select(s => new
                {
                    Name = s.FirstName + " " + s.LastName,
                    OrdersCount = s.Orders.Count()
                });

            //17- List active staff members only (active = true) along with their phone numbers.
            var staff = db.Staffs
            .Where(s => s.Active == 1)
            .Select(s => new
            {
                s.FirstName,
                s.Phone
            });

            //18- List all products with their brand name and category name
            var result = db.Products
                .Select(p => new
                {
                    p.ProductName,
                    Brand = p.Brand.BrandName,
                    Category = p.Category.CategoryName
                });

            //19- Retrieve orders that are completed.
            var orders = db.Orders
                    .Where(o => o.OrderStatus == 4);

            //20- List each product with the total quantity sold (sum of quantity from order_items).
            var result = db.Products
                .Select(p => new
                {
                    p.ProductName,
                    TotalSold = p.OrderItems.Sum(o => o.Quantity)
                });

        }
    }
}
