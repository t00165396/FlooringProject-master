using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using FlooringProgram.BLL;
using FlooringProgram.Models;


namespace FlooringProgram.Tests
{
    [TestFixture]
    public class OrderManagerTests
    {
        [Test]
        public void DisplayOrderTest()
        {
            // arrange
           // var manager = new OrderManager();
            //var response = manager.DisplayOrders("09222015");
        
            // act
            //var count = response.Data.Orders.Count;
            // assert
            //Assert.AreEqual(count, 2);
        }

        [Test]
        public void AddOrderTest()
        {
            // arrange
            var manager = new OrderManager();
            // act
            var order1 = new Order();
            order1.orderNumber = 1;
            order1.customerName = "Wise";
            order1.stateName = "OH";
            order1.taxRate = 6.25M;
            order1.productType = "Wood";
            order1.Area = 100.00M;
            order1.CostPerSquareFoot = 5.15M;
            order1.LaborCostPerSquareFoot = 4.75M;
            order1.MaterialCost = 515.00M;
            order1.LaborCost = 475.00M;
            order1.Tax = 61.88M;
            order1.Total = 1051.88M;
            string num = "09222015";
            var response = manager.AddOrder(order1, num);
            var count = response.Data.Orders.Count;
            // assert
            Assert.AreEqual(count, 3);
        }

        [Test]
        public void EditOrderTest()
        {
            // arrange
            var date = "09222015";
            var order1 = new Order();
            var manager = new OrderManager();
            order1.orderNumber = 6;
            order1.customerName = "Wise";
            order1.stateName = "OH";
            order1.taxRate = 6.25M;
            order1.productType = "Wood";
            order1.Area = 100.00M;
            order1.CostPerSquareFoot = 5.15M;
            order1.LaborCostPerSquareFoot = 4.75M;
            order1.MaterialCost = 515.00M;
            order1.LaborCost = 475.00M;
            order1.Tax = 61.88M;
            order1.Total = 1051.88M;
            // act
            manager.AddOrder(order1, date);
            order1.customerName = "Dave";
            var response = manager.EditOrder(order1, date, 6);
            var newname = response.Data.Order.customerName;
            // assert
            Assert.AreNotEqual(newname, "Wise");
        }

        [Test]
        public void RemoveOrderTest()
        {
            // arrange
            var manager = new OrderManager();
            var response = manager.DisplayOrders("09222015");
            // act
            var count = response.Data.Orders.Count;
            var orders = response.Data.Orders;
            var order1 = orders.Single(a => a.orderNumber == 1);
            manager.RemoveOrder(order1, "09222015", 1);
            var response2 = manager.DisplayOrders("09222015");
            var count2 = response2.Data.Orders.Count;
            // assert
            Assert.AreNotEqual(count, count2);
        }
    }
}
