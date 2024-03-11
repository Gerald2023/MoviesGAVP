using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {

        

        [TestMethod]
        public void LoadTest()
        {

            Assert.AreEqual(3, CustomerManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = CustomerManager.Insert("Test", "Test", "test",  "TEST", "WI", "Test", "Test", -99,  ref id, true);
            Assert.AreEqual(1, results);
        }
        
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Customer customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                Address = "Test",
                City = "Test",
                State = "WI",
                ZIP = "Test",
                Phone = "Test",
                UserId = -99,
                

            };

            int results = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(3);
            customer.FirstName = "Testing update method";
            int results = CustomerManager.Update(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = CustomerManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
           Customer customer = CustomerManager.LoadById(1);
            Assert.AreEqual("John", customer.FirstName);

            
        }
    }

}