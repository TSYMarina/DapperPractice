using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperPractice
{
    internal interface IProductsRepository
    {
            IEnumerable<Products> GetAllProducts();
            void CreateProduct(string newProdName, double newPrice, int newCategoryID, int newOnSale, int newStockLevel);

    }
}
