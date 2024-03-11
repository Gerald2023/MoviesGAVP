namespace GV.DVDCentral.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoginSucessfulTest()
        {
            Seed();
            Assert.IsTrue(UserManager.Login(new User { UserName = "bfoote", Password = "maple" }));
            Assert.IsTrue(UserManager.Login(new User { UserName = "kfrog", Password = "misspiggy" }));
        }

        public void Seed()
        {
            UserManager.Seed();
        }

        [TestMethod]
        public void InsertTest()
        {
            int id = 0;

            User user = new User
            {
                Id = 4,
                FirstName = "Bryan",
                LastName = "Foote",
                UserName = "bfoote",
                Password = "maple"
            };

            int result = UserManager.Insert(user, true);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LoginFailureNoUserName()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new User { UserName = "", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void LoginFailureBadPassword()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new User { UserName = "bfoote", Password = "birch" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureBadUserName()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new User { UserName = "bfote", Password = "maple" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        [TestMethod]
        public void LoginFailureNoPassword()
        {
            try
            {
                Seed();
                Assert.IsFalse(UserManager.Login(new User { UserName = "bfoote", Password = "" }));
            }
            catch (LoginFailureException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(2, UserManager.Load().Count);
        }

        [TestMethod]
        public void UpdateTest()
        {

            User student = UserManager.LoadById(2);
            student.FirstName = "Test";
            int results = UserManager.Update(student, true);
            Assert.AreEqual(1, results);
        }
    }
}
