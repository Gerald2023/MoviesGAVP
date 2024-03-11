
namespace GV.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
    {
        //Modular or class level scope
        protected DVDCentralEntities dc;// Declared it. It is intantiated it on line 14. 
        protected IDbContextTransaction transaction; 

        [TestInitialize] // This is a method attribute, it is used to set up for the test.  It is run before each test.
        public void Initialize()
        {
            dc = new DVDCentralEntities(); // Instantiated from protected DVDCentralEntities di;
            transaction  = dc.Database.BeginTransaction();
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
            Assert.AreEqual(3, dc.tblDirectors.Count()); //Assert means Test(I'm gonna test something) // di.tblDirectors is select * from tblDirectors

        }

        [TestMethod]
        public void InsertTest()
        {
           // DVDCentralEntities di = new DVDCentralEntities(); // This is a new instance of the DVDCentralEntities

            //Make an entity 

            tblDirector entity = new tblDirector();
            entity.Id = -99;
            entity.FirstName = "Gerald";
            entity.LastName = "Vallejos";

            //add the entity to the database
            dc.tblDirectors.Add(entity);

            //commit the changes (insert a record)

            int result = dc.SaveChanges(); //dc.SaveChanges();
            Assert.AreEqual(1, result);

        }



        [TestMethod]
        public void UpdateTest()
        {
            // select * from tblDirector
            tblDirector entity = dc.tblDirectors.FirstOrDefault();

            //change property values
            entity.FirstName = "Gabriel";
            entity.LastName = "Gonzalez";

            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {
            // select * from tblDirector
            tblDirector entity = dc.tblDirectors.Where(e=>e.Id ==3).FirstOrDefault();

            //remove the entity
            dc.tblDirectors.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }

    }
}