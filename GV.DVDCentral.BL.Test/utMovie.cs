namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadTest()
        {

            Assert.AreEqual(3, MovieManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = MovieManager.Insert("Test","Test",99.3, 99, 99, 99, 99, "Test Path", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Movie movie = new Movie
            {
                Title = "test",
                Description = "Test",
                Cost = 23.5,
                RatingId = 6,
                FormatId = 6,
                DirectorId = 6,
                InStkQty = 6,
                ImagePath = "Test"
                

            };

            int results = MovieManager.Insert(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadById(3);
            movie.Title = "Test";
            movie.Description = "Test";
            int results = MovieManager.Update(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }

}