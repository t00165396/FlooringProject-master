using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class ProductRepo : IProductRepo
    {
        private const string FilePath = @"DataFiles\Products.txt";

        public List<Product> LoadProductType()
        {
            List<Product> Products = new List<Product>();

            if (File.Exists(FilePath))
            {
                var reader = File.ReadAllLines(FilePath);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var product = new Product();
                    product.ProductType = (columns[0]);
                    product.CostPerSquareFoot = decimal.Parse(columns[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);
                    Products.Add(product);
                }
            }
            else
            {
                var emptyproduct = new Product();
                Products.Add(emptyproduct);
                Console.Write("There is no product file. Talk to system admin.");
                Console.ReadKey();
            }
            return Products;
        }
    }
}
