namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrderItem
    {
        [TestMethod]
        public void LoadTest()
        {

            Assert.AreEqual(3, OrderItemManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = OrderItemManager.Insert(-99, -99, 23, 35.6, ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod] 
        public void LoadByOrderIdTest()
        {
            int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
            Assert.IsTrue(OrderItemManager.LoadByOrderId(orderId).Count > 0);
        }

        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            OrderItem orderItem = new OrderItem
            {
               OrderId = -99,
               MovieId = -99,
               Quantity = 23,
               Cost = 35.6,

            };

            int results = OrderItemManager.Insert(orderItem, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            OrderItem orderItem = OrderItemManager.LoadById(3);
            orderItem.Cost = 1000;
            int results = OrderItemManager.Update(orderItem, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderItemManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }

}