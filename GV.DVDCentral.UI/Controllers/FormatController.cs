using GV.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace GV.DVDCentral.UI.Controllers
{
    public class FormatController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Formats";
            return View(FormatManager.Load());
        }

        public IActionResult Details(int id)
        {
            return View(FormatManager.LoadById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Format format)
        {
            try
            {
                int result = FormatManager.Insert(format);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult Edit(int id)
        {
            return View(FormatManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Edit(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Update(format, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(format);
            }
        }

        public IActionResult Delete(int id)
        {
            return View(FormatManager.LoadById(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, Format format, bool rollback = false)
        {
            try
            {
                int result = FormatManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View(format);
            }
        }

    }
}
