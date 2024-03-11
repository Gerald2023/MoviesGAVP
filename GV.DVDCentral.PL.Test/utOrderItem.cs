
namespace GV.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem

    {
        //Modular or class level scope
        protected DVDCentralEntities dc;// Declared it. It is intantiated it on line 14. 
        protected IDbContextTransaction transaction;

        [TestInitialize] // This is a method attribute, it is used to set up for the test.  It is run before each test.
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
            //DVDCentralEntities dc = new DVDCentralEntities(); // This is a new instance of the DVDCentralEntities
            Assert.AreEqual(3, dc.tblOrderItems.Count()); //Assert means Test(I'm gonna test something) // di.tblOrderItems is select * from tblOrderItems

        }

        [TestMethod]
        public void InsertTest()
        {
            // DVDCentralEntities di = new DVDCentralEntities(); // This is a new instance of the DVDCentralEntities

            //Make an entity 

            tblOrderItem entity = new tblOrderItem();
            entity.Id = -99;
            entity.OrderId = -99;
            entity.MovieId = -99;
            entity.Quantity = 9999;
            entity.Cost = 9999; 



            //add the entity to the database
            dc.tblOrderItems.Add(entity);

            //commit the changes (insert a record)

            int result = dc.SaveChanges(); //dc.SaveChanges();
            Assert.AreEqual(1, result);

        }




        [TestMethod]
        public void UpdateTest()
        {
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            //change property values
            entity.Quantity = 200;
            entity.Cost = 28.6;


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {

            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 2).FirstOrDefault();

            //remove the entity
            dc.tblOrderItems.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}