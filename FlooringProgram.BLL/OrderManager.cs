using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;
using FlooringProgram.Data;



namespace FlooringProgram.BLL
{
    public class OrderManager
    {
        private IOrderRepo _repo;
        private ITaxRepo _taxrepo;
        private IProductRepo _productrepo;

        public OrderManager()
        {
            string mode = ConfigurationManager.AppSettings["appName"];
            if (mode == "Test")
            {
                var repo = new TestOrderRepo();
                _repo = repo;
            }
            else
            {
                var repo = new ProdOrderRepo();
                _repo = repo;
                _taxrepo = new TaxRepo();
                _productrepo = new ProductRepo();
            }
        }

        public Response<DisplayOrderReceipt> DisplayOrders(string date)
        {
            var response = new Response<DisplayOrderReceipt>();
            var repo = _repo;
            var orders = repo.LoadOrders(date);
            try
            {
                if (orders.Count == 0)
                {
                    response.Success = false;
                    response.Message = "No orders were found with that date.";
                }
                else
                {
                    response.Success = true;
                    response.Data = new DisplayOrderReceipt();
                    response.Data.Date = int.Parse(date);
                    response.Data.Orders = orders;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<AddOrderReceipt> AddOrder(Order order, string date)
        {
            var response = new Response<AddOrderReceipt>();
            response.Data = new AddOrderReceipt();
            var repo = _repo;
            var orders = _repo.LoadOrders(date);
            var taxes = _taxrepo.LoadTaxRate();
            var products = _productrepo.LoadProductType();
            try
            {
                var highAccountNum = 0;
                if (orders.Count > 0)
                {
                    highAccountNum = orders.Select(a => a.orderNumber).Max();
                }
                order.orderNumber = highAccountNum + 1;
                var taxrate = taxes.First(a => a.StateAbbreviation == order.stateName);
                order.taxRate = taxrate.TaxRate;
                var producttype = products.First(a => a.ProductType == order.productType);
                order.CostPerSquareFoot = producttype.CostPerSquareFoot;
                order.LaborCostPerSquareFoot = producttype.LaborCostPerSquareFoot;
                order = SetDerivedOrderInfo(order);
                orders.Add(order);
                repo.OverWriteFileWithOrder(orders, date);
                response.Success = true;
                response.Data = new AddOrderReceipt();
                response.Data.Date = int.Parse(date);
                response.Data.Orders = orders;
                response.Data.Order = order;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<EditOrderReceipt> EditOrder(Order order, string date, int ordernum)
        {
            var response = new Response<EditOrderReceipt>();
            var repo = _repo;
            var orders = _repo.LoadOrders(date);
            var taxes = _taxrepo.LoadTaxRate();
            var products = _productrepo.LoadProductType();

            try
            {
                var taxrate = taxes.First(a => a.StateAbbreviation == order.stateName);
                order.taxRate = taxrate.TaxRate;
                var producttype = products.First(a => a.ProductType == order.productType);
                order.CostPerSquareFoot = producttype.CostPerSquareFoot;
                order.LaborCostPerSquareFoot = producttype.LaborCostPerSquareFoot;
                order = SetDerivedOrderInfo(order);
                var filtered = orders.Where(a => a.orderNumber != ordernum).ToList();
                filtered.Add(order);
                repo.OverWriteFileWithOrder(filtered, date);
                response.Success = true;
                response.Data = new EditOrderReceipt();
                response.Data.Date = int.Parse(date);
                response.Data.Orders = orders;
                response.Data.Order = order;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<RemoveOrderReceipt> RemoveOrder(Order order, string date, int ordernum)
        {
            var response = new Response<RemoveOrderReceipt>();
            //response.Data = new RemoveOrderReceipt;
            var repo = _repo;
            var orders = _repo.LoadOrders(date);
            try
            {
                var filtered = orders.Where(a => a.orderNumber != ordernum).ToList();
                repo.OverWriteFileWithOrder(filtered, date);
                response.Success = true;
                response.Data = new RemoveOrderReceipt();
                response.Data.Date = int.Parse(date);
                response.Data.Orders = orders;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private Order SetDerivedOrderInfo(Order order)
        {
            var materialcost = order.CostPerSquareFoot * order.Area;
            var laborcost = order.LaborCostPerSquareFoot * order.Area;
            var tax = (materialcost + laborcost) * (order.taxRate / 100);
            var total = materialcost + laborcost + tax;
            order.MaterialCost = materialcost;
            order.LaborCost = laborcost;
            order.Tax = tax;
            order.Total = total;
            return order;
        }
    }
}
