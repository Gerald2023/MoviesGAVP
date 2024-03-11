using GV.DVDCentral.BL;
using GV.DVDCentral.BL.Models;
using GV.DVDCentral.UI.Extensions;
using GV.DVDCentral.UI.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GV.DVDCentral.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View(UserManager.Load());
        }


        private void SetUser(User user)
        {

            HttpContext.Session.SetObject("user", user);
            if (user != null)
            {
                HttpContext.Session.SetObject("fullname", "Welcome " + user.FullName);

            }
            else
            {
                HttpContext.Session.SetObject("fullname", string.Empty);

            }
        }




        //clean the session
        public IActionResult Logout()
        {
            ViewBag.Title = "Logout";
            SetUser(null);
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            ViewBag.Title = "Login";
            return View();
        }


        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                ViewBag.Title = "Login";
                bool result = UserManager.Login(user);
                SetUser(user);

                if (TempData["returnUrl"] != null)
                    return Redirect(TempData["returnUrl"]?.ToString());




                return RedirectToAction(nameof(Index), "Movie");
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Login";
                ViewBag.Error = ex.Message;
                return View(user);
            }
        }


        public IActionResult Seed()
        {

            UserManager.Seed();
            return View();
        }


        //user sign up feature 
        public IActionResult Create()
        {
            
                return View();
            


        }

        [HttpPost]
        public IActionResult Create(User user)
        {

            try
            {
                int result = UserManager.Insert(user);
                return RedirectToAction(nameof(Login));
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*        public IActionResult Browse(int id)
                {
                    return View(nameof(Index), UserManager.Load(id));
                }
        */


        public IActionResult Browse2()
        {
            var authenticatedUserId = HttpContext.Session.GetObject<User>("user")?.Id;

            if (authenticatedUserId != null)
            {
                // Load user information for the authenticated user
                var user = UserManager.Load(authenticatedUserId.Value).FirstOrDefault();
                return View(nameof(Index), new List<User> { user });
            }
            else
            {
                // Handle unauthorized access (redirect or show an error)
                return RedirectToAction("Login", "User");
            }
        }



        public IActionResult Edit(int id)
        {
            

            if (Authenticate.IsAuthenticated(HttpContext))
            {

                return View(UserManager.LoadById(id));
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

            }
        }

        [HttpPost]
        public IActionResult Edit(int id, User user, bool rollback = false)
        {


            try
            {
                int result = UserManager.Update(user, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(user);
            }
        }

        public void seed()
        {
            UserManager.Seed();

        }

    }
}

