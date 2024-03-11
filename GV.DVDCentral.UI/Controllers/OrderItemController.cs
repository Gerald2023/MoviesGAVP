using GV.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace GV.DVDCentral.UI.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "List of All OrderItems";

            return View(OrderItemManager.Load());
        }
    }
}
