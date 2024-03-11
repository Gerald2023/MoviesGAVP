namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utDirector
    {
        [TestMethod]
        public void LoadTest()
        {

            Assert.AreEqual(3, DirectorManager.Load().Count);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = DirectorManager.Insert("Test", "Test", ref id, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Director director = new Director
            {
               FirstName = "Test",
               LastName = "Test"

            };

            int results = DirectorManager.Insert(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Director director = DirectorManager.LoadById(3);
            director.FirstName = "Test";
            int results = DirectorManager.Update(director, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = DirectorManager.Delete(3, true);
            Assert.AreEqual(1, results);
        }
    }

}