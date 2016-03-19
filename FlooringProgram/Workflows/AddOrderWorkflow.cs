using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.BLL;
using FlooringProgram.Models;

namespace FlooringProgram.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            string Date = GetDate();
            Console.Clear();
            string CustomerName = GetCustomerName();
            Console.Clear();
            string StateName = GetStateName();
            Console.Clear();
            string ProductType = GetProductType();
            Console.Clear();
            decimal Area = GetArea();
            Console.Clear();
            var manager = new OrderManager();
            var order = new Order();
            order.customerName = CustomerName;
            order.stateName = StateName;
            order.productType = ProductType;
            order.Area = Area;

            var response = manager.AddOrder(order, Date);
            if (response.Data.Orders == null || response.Data.Orders.Count < 1)
            {
                Console.WriteLine("Blah...");
            }
            var neworder = response.Data.Order;
            Console.WriteLine();
            Console.WriteLine("NEW ORDER SUMMARY");
            Console.WriteLine("*****************");
            Console.WriteLine("orderNumber  customerName  stateName  taxRate  productType  Area    CostPerSquareFoot LaborCostPerSquareFoot MaterialCost LaborCost Tax    Total");
            const string formataddorder = "{0,1} {1,15} {2,11} {3,12} {4,10} {5,8:n2} {6,9} {7,17} {8,24} {9,11} {10,8:n2} {11,7:n2}";
            string line1 = string.Format(formataddorder, "#", neworder.customerName, neworder.stateName,
                                      neworder.taxRate, neworder.productType,
                                      neworder.Area, neworder.CostPerSquareFoot,
                                      neworder.LaborCostPerSquareFoot,
                                      neworder.MaterialCost, neworder.LaborCost,
                                      neworder.Tax, neworder.Total);

            Console.WriteLine(line1);
            Console.WriteLine();

            bool prompt = false;
            string newgame = "";
            while (prompt == false)
            {
                Console.Write("Commit new order? (Y/N): ");
                newgame = Console.ReadLine();
                if (newgame.ToUpper() == "Y" || newgame.ToUpper() == "YES"
                    || newgame.ToUpper() == "N" || newgame.ToUpper() == "NO")
                {
                    prompt = true;
                }
            }
            if (newgame.ToUpper() == "Y" || newgame.ToUpper() == "YES")
            {
                Console.Write("Order added to the system. Press any key to continue...");
                Console.ReadKey();
            }
            else
            {

                manager.RemoveOrder(neworder, Date, neworder.orderNumber);
                Console.Write("Order has been cancelled! Press any key to continue...");
                Console.ReadKey();
            }
        }

        private string GetCustomerName()
        {
            do
            {
                Console.Write("Enter customer name (',' or '.' not allowed): ");
                string input = Console.ReadLine();
                int num;
                bool test = int.TryParse(input, out num);
                if (input.Length > 0 && input != "" && !test && !input.Contains(",") && !input.Contains("."))
                {
                    return input;
                }
            } while (true);
        }

        private string GetStateName()
        {
            do
            {
                Console.Write("Enter state name (only OH/PA/MI/IN): ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "OH" || input.ToUpper() == "PA" ||
                    input.ToUpper() == "MI" || input.ToUpper() == "IN")
                {
                    return input.ToUpper();
                }
            } while (true);
        }

        private string GetDate()
        {
            do
            {
                Console.Write("Enter date of order (MMDDYYYY): ");
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

        private string GetProductType()
        {
            do
            {
                Console.Write("Enter product type (only carpet/laminate/tile/wood): ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "CARPET")
                {
                    return "Carpet";
                }
                if (input.ToUpper() == "LAMINATE")
                {
                    return "Laminate";
                }
                if (input.ToUpper() == "TILE")
                {
                    return "Tile";
                }
                if (input.ToUpper() == "WOOD")
                {
                    return "Wood";
                }
            } while (true);

        }

        private decimal GetArea()
        {
            do
            {
                Console.Write("Enter area greater than zero (ex: 10.00): ");
                string input = Console.ReadLine();
                decimal num;
                bool test = decimal.TryParse(input, out num);
                if (input != "" && test && decimal.Parse(input) > 0)
                {
                    return num;
                }
            } while (true);
        }
    }
}
