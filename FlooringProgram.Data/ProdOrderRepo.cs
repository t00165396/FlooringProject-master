using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using System.IO;

namespace FlooringProgram.Data
{
    public class ProdOrderRepo : IOrderRepo
    {
        private const string FilePath = @"DataFiles\";

        public List<Order> LoadOrders(string date)
        {
            List<Order> Orders = new List<Order>();
            string filename = "Orders_" + date + ".txt";
            var fileToRead = FilePath + filename;
            if (File.Exists(fileToRead))
            {
                var reader = File.ReadAllLines(fileToRead);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var order = new Order();

                    order.orderNumber = int.Parse(columns[0]);
                    order.customerName = columns[1];
                    order.stateName = columns[2];
                    order.taxRate = decimal.Parse(columns[3]);
                    order.productType = columns[4];
                    order.Area = decimal.Parse(columns[5]);
                    order.CostPerSquareFoot = decimal.Parse(columns[6]);
                    order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    order.MaterialCost = decimal.Parse(columns[8]);
                    order.LaborCost = decimal.Parse(columns[9]);
                    order.Tax = decimal.Parse(columns[10]);
                    order.Total = decimal.Parse(columns[11]);

                    Orders.Add(order);
                }
            }
            else
            {

                //Console.ReadKey();
            }
            return Orders;
        }

        private void OverWriteFile(List<Order> orders, string date)
        {
            string filename = "Orders_" + date + ".txt";
            var fileToRead = FilePath + filename;
            if (File.Exists(fileToRead))
            {
                File.Delete(fileToRead);
            }
                // deletefile
                //then create new file and write.
                using (var writer = File.CreateText(fileToRead))
                {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (var order in orders)
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", order.orderNumber,
                        order.customerName,
                        order.stateName,
                        order.taxRate,
                        order.productType,
                        order.Area,
                        order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot,
                        order.MaterialCost,
                        order.LaborCost,
                        order.Tax,
                        order.Total);
                    }
                }
            }
        

        public void OverWriteFileWithOrder(List<Order> orders, string date)
        {
            OverWriteFile(orders, date);
        }
    }
}
