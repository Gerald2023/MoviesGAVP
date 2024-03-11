using GV.DVDCentral.PL;

namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovieGenre
    {


        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = MovieGenreManager.Insert(-99, -99, true);
            Assert.AreEqual(1, results);
        }



        [TestMethod]
        public void UpdateTest()
        {
            // MovieGenre movieGenre = MovieGenreManager.LoadById(3);
          
            int results = MovieGenreManager.Update( 1, 1, true);
            Assert.AreEqual(0, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieGenreManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }

}