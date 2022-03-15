using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace DapperPractice
{
    internal class Program
    {

        static public string GetValidResponse(string userPrompt)
        {
            string userInput = "";
            bool validUserInput = false;
            do
            {
                userInput = Console.ReadLine().ToUpper();
                if (userInput.ToUpper() == "ADD" || userInput.ToUpper() == "UPD")
                {
                    validUserInput = true;
                    Console.WriteLine("Executing your command.");
                }
                else
                {
                    Console.WriteLine("Sorry, this is not a valid input. You need to enter ADD or UPD, retry please:  ");
                }
            }
            while (!validUserInput);
            return userInput;
        }

        static void Main(string[] args)
        {
            #region Configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            #endregion

            IDbConnection connection = new MySqlConnection(connString);

            DapperDepartmentRepository repo = new DapperDepartmentRepository(connection);
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("\n\nCurent Departments List: \n");
            var depos = repo.GetAllDepartments();

            foreach (var dept in depos)
            {
                Console.WriteLine($"Dep ID: {dept.DepartmentID}  Name: {dept.Name}\n");
            }

            DapperProductsRepository repoPr = new DapperProductsRepository(connection);
            Console.WriteLine("*******************************************************************");
            Console.WriteLine("\nCurrent Products List: \n");
            var prod = repoPr.GetAllProducts();

            foreach (var prd in prod)
            {
                Console.WriteLine($"Product ID: {prd.ProductID}  Name: {prd.Name} Price: {prd.Price} Category ID: {prd.CategoryID}, Is On Sale: {prd.OnSale}, Stock level: {prd.StockLevel}\n");
            }

            Console.WriteLine("-------------------------End of Products table-------------------------\n\n");

            Console.WriteLine("Would you like to add a product to the database? Please have product name, price, category ID and stock level ready and enter ADD. \nIf you need to update, please enter UPD to edit product price. \n  8[  We apologize but editing product ID, sale status, product name or stock level is unavailable with current access level 8[ You may delete and re-enter. \n\n");
            Console.WriteLine();
            
           var repo1 = new DapperProductsRepository(connection);
           var response = GetValidResponse("Please enter your selection");
            
          
            if (response == "ADD")
            {
                Console.WriteLine("\n\nWhat is the new product name?");
                var prodName = Console.ReadLine();

                Console.WriteLine("What is this product's price?");
                var price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("What is its category id (options 1-10)?");
                var categoryID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter 1, if product is on sale, otherwise, please enter 0: ");
                int saleStatus = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("What is its stock level?");
                var stockLev = Convert.ToInt32(Console.ReadLine());

                repo1.CreateProduct(prodName, price, categoryID, saleStatus, stockLev);
            }
            else if (response == "UPD")
            {
                Console.Write ($"\n\nWhat is the ID of the product you would like to update?  ");

                var productID = Convert.ToInt32(Console.ReadLine());

                Console.Write($"Enter the new price for {productID}    ");
                var newPrice = Convert.ToDouble(Console.ReadLine());

                repo1.UpdateProductPrice(productID, newPrice);

            }
            else
            {
                Console.WriteLine("Thank you for using Dapper and MySQL.");
            }
        }
    }
}
