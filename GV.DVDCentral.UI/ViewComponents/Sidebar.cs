using GV.DVDCentral.BL;
using Microsoft.AspNetCore.Mvc;

namespace GV.DVDCentral.UI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(GenreManager.Load().OrderBy(p=> p.Description));

        }
    }
}
