namespace GV.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre
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
            Assert.AreEqual(3, dc.tblMovieGenres.Count()); 

        }

        [TestMethod]
        public void InsertTest()
        {
            // Make an entity
            tblMovieGenre entity = new tblMovieGenre();

            entity.Id = -99;
            entity.MovieId = -99;
            entity.GenreId = -99;


            // add entity to database

            dc.tblMovieGenres.Add(entity);

            //commit the changes (insert a record)

            int result = dc.SaveChanges(); //dc.SaveChanges();
            Assert.AreEqual(1, result);



        }



        [TestMethod]
        public void UpdateTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            //change property values
            entity.GenreId = -16;
            


            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);

        }

        [TestMethod]
        public void DeleteTest()
        {

            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 3).FirstOrDefault();

            //remove the entity
            dc.tblMovieGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.AreNotEqual(result, 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}