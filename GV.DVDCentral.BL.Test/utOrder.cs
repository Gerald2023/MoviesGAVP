namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {

            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = OrderManager.Insert(-98, -98, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Order order = new Order
            {
                CustomerId = -98,
                UserId = -99,
                OrderDate = DateTime.Now,
                ShipDate = DateTime.Now

            };

            int results = OrderManager.Insert(order, true);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void InsertOrderItemsTest()
        {
            Order order = new Order
            {
                CustomerId = 99,
                OrderDate = DateTime.Now,
                UserId = 99,
                ShipDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        Id = 88,
                        MovieId = 1,
                        Cost = 9.99,
                        Quantity = 9
                    },
                    new OrderItem
                    {
                        Id = 88,
                        MovieId = 2,
                        Cost = 9.99,
                        Quantity = 9,
                        OrderId=4

                    }
                }

            };


            int result = OrderManager.Insert(order, true);
            Assert.AreEqual(order.OrderItems[1].OrderId, order.Id);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            int id = OrderManager.Load().LastOrDefault().Id;
            Order order = OrderManager.LoadById(id);
            Assert.AreEqual(order.Id, id);
            Assert.IsTrue(order.OrderItems.Count > 0);
        }


        [TestMethod]
        public void LoadByIdCustomerIdTest()
        {
            int customerId = OrderManager.Load().FirstOrDefault().CustomerId;

            Assert.AreEqual(OrderManager.LoadById(customerId).CustomerId, customerId);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(3);
            order.UserId = -16;
            int results = OrderManager.Update(order, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderManager.Delete(3, true);
            Assert.AreEqual(1, results);

        }
    }

}