namespace GV.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
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
            Assert.AreEqual(3, dc.tblCustomers.Count()); 

        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblCustomer entity = new tblCustomer(); 

            entity.Id = 5;
            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.Address = "Test";
            entity.City = "Test";
            entity.State = "TT";
            entity.ZIP = "Test";
            entity.Phone = "Test";
            entity.UserId = -99;

            // add entity to database

            dc.tblCustomers.Add(entity);

            //commit the changes (insert a record)

            int result = dc.SaveChanges(); //dc.SaveChanges();
            Assert.AreEqual(1, result);



        }



        [TestMethod]
        public void UpdateTest()
        {
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            //change property values
            entity.Address = " Update Test";
            


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {

            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 3).FirstOrDefault();

            //remove the entity
            dc.tblCustomers.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}