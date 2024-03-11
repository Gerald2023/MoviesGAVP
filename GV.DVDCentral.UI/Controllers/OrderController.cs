using GV.DVDCentral.BL;
using GV.DVDCentral.UI.Extensions;
using GV.DVDCentral.UI.Models;
using GV.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GV.DVDCentral.UI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Orders";
            return View(OrderManager.Load());
        }

        public IActionResult Details(int id)
        {



            if (Authenticate.IsAuthenticated(HttpContext))
            {


                var item = OrderManager.LoadById(id);
               ViewBag.Title = "Details for" + item.Description;
                return View(OrderManager.LoadById(id));
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

            }

        }

        public IActionResult Create()
        {

            ViewBag.Title = "Create an order";
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                int result = OrderManager.Insert(order);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {

            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Edit order " + item.Description;
            return View(item);

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(OrderManager.LoadById(id));
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });

            }
        }

       

        [HttpPost]

        public IActionResult Edit(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Update(order, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(order);
            }
        }




      
        public IActionResult Delete(int id)
        {
            var item = OrderManager.LoadById(id);
            ViewBag.Title = "Delete ";
            return View(item);
        }


        [HttpPost]
        public IActionResult Delete(int id, Order order, bool rollback = false)
        {
            try
            {
                int result = OrderManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(order);
            }
        }

    }
}
