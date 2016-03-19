using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.BLL;

namespace FlooringProgram.Workflows
{
    public class DisplayOrderWorkflow
    {
        public void Execute()
        {
            var date = GetDate();
            var manager = new OrderManager();
            var response = manager.DisplayOrders(date);
            if (response.Success)
            {
                Console.Clear();
                Console.WriteLine("orderNumber  customerName  stateName  taxRate  productType  Area    CostPerSquareFoot LaborCostPerSquareFoot MaterialCost LaborCost Tax   Total");

                const string format = "{0,-10} {1,11} {2,6} {3,12} {4,14} {5,8:n2} {6,5:n2} {7,17:n2} {8,24:n2} {9,12:n2} {10,8:n2} {11,8:n2}";
                
                foreach (var order in response.Data.Orders)
                {
                    string line1 = string.Format(format, order.orderNumber,
                        order.customerName, order.stateName,
                        order.taxRate, order.productType,
                        order.Area, order.CostPerSquareFoot,
                        order.LaborCostPerSquareFoot,
                        order.MaterialCost, order.LaborCost,
                        order.Tax, order.Total);

                    Console.WriteLine(line1);

                }
                Console.Write("\nPress any key to continue... ");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine(response.Message);
                Console.Write("\nPress any key to continue... ");
                Console.ReadKey();
            }

        }

        private string GetDate()
        {
            do
            {
                Console.Write("Enter date of orders to display (MMDDYYYY): ");
                string input = Console.ReadLine();
                int num;
                var passThisString = input;
                bool parsedinput = int.TryParse(input, out num);
                if (parsedinput && input.Length == 8)
                {
                    return passThisString;
                }
                DateTime numcheck;
                bool parseddatetime = DateTime.TryParse(input, out numcheck);
                if (parseddatetime)
                {
                    return numcheck.ToString("MMddyyyy");
                }
            } while (true);
        }
    }
}
