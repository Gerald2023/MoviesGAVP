using GV.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace GV.DVDCentral.UI.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Ratings";
            return View(RatingManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(RatingManager.LoadById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rating rating)
        {
            try
            {
                int result = RatingManager.Insert(rating);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {
            return View(RatingManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Rating rating, bool rollback = false)
        {
            try
            {
                int result = RatingManager.Update(rating, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(rating);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(RatingManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Rating rating, bool rollback = false)
        {
            try
            {
                int result = RatingManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(rating);
            }
        }

    }
}
