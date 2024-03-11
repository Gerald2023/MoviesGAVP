namespace GV.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
    {

        //Modular or class level scope
        protected DVDCentralEntities dc;// Declared it. It is intantiated it on line 14. 
        protected IDbContextTransaction transaction;


        [TestInitialize]
        public void Initialize()
        {
            dc = new DVDCentralEntities(); // Instantiated from protected DVDCentralEntities di;
            transaction = dc.Database.BeginTransaction();

        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose(); // to clean the memory
            dc = null;  

        }
       
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblOrders.Count()); 

        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblOrder entity = new tblOrder(); 

            entity.Id = -99;
            entity.CustomerId = -99;
            entity.OrderDate = DateTime.Now;
            entity.UserId = -99;
            entity.ShipDate = DateTime.Now; 
            
            // add entity to database

            dc.tblOrders.Add(entity);

            //commit the changes (insert a record)

            int result = dc.SaveChanges(); //dc.SaveChanges();
            Assert.AreEqual(1, result);



        }



        [TestMethod]
        public void UpdateTest()
        {
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            //change property values
            entity.CustomerId = -16;
            


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {

            tblOrder entity = dc.tblOrders.Where(e => e.Id == 2).FirstOrDefault();

            //remove the entity
            dc.tblOrders.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}