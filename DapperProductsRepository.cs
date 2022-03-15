using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace DapperPractice
{
    internal class DapperProductsRepository : IProductsRepository
    {
        private readonly IDbConnection _connection;

        public DapperProductsRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _connection.Query<Products>("SELECT * FROM products;");
        }

        public void CreateProduct(string newProdName, double newPrice, int newCategoryID, int newOnSale, int newStockLevel)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID, OnSale, StockLevel) VALUES (@name, @price, @categoryID, @onSale, @stockLevel);",
                new { name = newProdName, price = newPrice, categoryID = newCategoryID, onSale = newOnSale, StockLevel = newStockLevel });
            Console.WriteLine($"**Success: Your product {newProdName} has been added.\nThis session is over.\n\n");
        }

        public void UpdateProductPrice(int productID, double newValue)
        {
            _connection.Execute("UPDATE products SET Price = @newValue WHERE ProductID = @productID;",
                new { newValue = newValue, productID = productID });

            Console.WriteLine($"**Success: Product price for {productID} has been updated.\n\n");
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID});

            Console.WriteLine($"**Success: Product {productID} has been removed from the database.\n\n");
        }

    }
}
