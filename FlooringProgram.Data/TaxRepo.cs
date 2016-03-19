using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class TaxRepo : ITaxRepo
    {
        private const string FilePath = @"DataFiles\Taxes.txt";

        public List<Tax> LoadTaxRate()
        {

            List<Tax> Taxes = new List<Tax>();
            if (File.Exists(FilePath))
            {
                var reader = File.ReadAllLines(FilePath);
                for (int i = 1; i < reader.Length; i++)
                {
                    var columns = reader[i].Split(',');
                    var tax = new Tax();
                    tax.StateAbbreviation = (columns[0]);
                    tax.StateName = columns[1];
                    tax.TaxRate = decimal.Parse(columns[2]);
                    Taxes.Add(tax);
                }
            }
            else
            {
                var emptytax = new Tax();
                Taxes.Add(emptytax);
                Console.Write("There is no tax file. Talk to system admin.");
                Console.ReadKey();
            }
            return Taxes;
        }
    }
}

