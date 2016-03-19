using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Workflows;

namespace FlooringProgram
{
    public class MainMenu
    {
        public void Execute()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("******************************");
                Console.WriteLine("*      Flooring Program      *");
                Console.WriteLine("*                            *");
                Console.WriteLine("*    1. Display Orders       *");
                Console.WriteLine("*    2. Add an Order         *");
                Console.WriteLine("*    3. Edit an Order        *");
                Console.WriteLine("*    4. Remove an Order      *");
                Console.WriteLine("*    5. Quit                 *");
                Console.WriteLine("*                            *");
                Console.WriteLine("******************************\n");

                Console.Write("Enter choice: ");
                string input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.Substring(0, 1).ToUpper() == "Q")
                {
                    break;
                }
                ProcessChoice(input);
            } while (true);
        }

        private void ProcessChoice(string input)
        {
            switch (input)
            {
                case "1":
                    var DisplayOrder = new DisplayOrderWorkflow();
                    DisplayOrder.Execute();
                    break;
                case "2":
                    var AddOrder = new AddOrderWorkflow();
                    AddOrder.Execute();
                    break;
                case "3":
                    var EditOrder = new EditOrderWorkflow();
                    EditOrder.Execute();
                    break;
                case "4":
                    var RemoveOrder = new RemoveOrderWorkflow();
                    RemoveOrder.Execute();
                    break;
                case "5":
                    System.Environment.Exit(1);
                    break;
            }
        }
    }
}
