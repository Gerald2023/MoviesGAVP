using GV.DVDCentral.BL;
using GV.DVDCentral.UI.Extensions;
using GV.DVDCentral.UI.Models;
using GV.DVDCentral.UI.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace GV.DVDCentral.UI.Controllers
{

   

    public class MovieController : Controller
    {

        private readonly IWebHostEnvironment _host;

        public MovieController(IWebHostEnvironment host)
        {
            _host = host;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "List of All Movies";
            return View(MovieManager.Load());
        }

        public IActionResult Details(int id)
        {
         

            var item = MovieManager.LoadById(id);
            ViewBag.Title = "Details for " + item.Description;
            return View(item);
        }




        public IActionResult Create()
        {
            ViewBag.Title = "Create a Movie";

            MovieVM movieVM = new MovieVM();
            movieVM.Movie = new Movie();

            movieVM.Genres = GenreManager.Load(); // Load genres for the dropdown
            movieVM.Ratings = RatingManager.Load();
            movieVM.Formats = FormatManager.Load();
            movieVM.Directors = DirectorManager.Load();

      /*      // Store the genre ids in the session
            HttpContext.Session.SetObject("genreids", movieVM.GenreIds);*/

            if (Authenticate.IsAuthenticated(HttpContext))
            {
                return View(movieVM);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Create(MovieVM movieVM)
        {
            try
            {
                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                    {
                        movieVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }

                IEnumerable<int> newGenreIds = new List<int>();

                newGenreIds = movieVM.GenreIds;

                HttpContext.Session.SetObject("genreids", movieVM.GenreIds); // store genreIds

                IEnumerable<int> adds = newGenreIds;

                newGenreIds = GetObject();





                int result = MovieManager.Insert(movieVM.Movie); // Insert movies into tblMovie

                adds.ToList().ForEach(a => MovieGenreManager.Insert(movieVM.Movie.Id, a));






                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }



        public IActionResult Browse(int id)
        {
           var results = MovieManager.LoadById(id);
            ViewBag.Title = "List of " + results.Description + " Movies";   
            return View(nameof(Index), MovieManager.Load(id));
        }

        public IActionResult Edit(int id)
        {
            if (Authenticate.IsAuthenticated(HttpContext))
            {
                MovieVM movieVM = new MovieVM(id);

                ViewBag.Title = "Edit " + movieVM.Movie.Description;

                // Store the genre ids in the session
                HttpContext.Session.SetObject("genreids", movieVM.GenreIds);

                return View(movieVM);
            }
            else
            {
                return RedirectToAction("Login", "User", new { returnUrl = UriHelper.GetDisplayUrl(HttpContext.Request) });
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, MovieVM movieVM, bool rollback = false)
        {
            try
            {
                IEnumerable<int> newGenreIds = movieVM.GenreIds ?? Enumerable.Empty<int>();
                IEnumerable<int> oldGenreIds = GetObject() ?? Enumerable.Empty<int>();

                //
                var breakpointCheck = true;

                // old movie cannot be null
                IEnumerable<int> deletes = oldGenreIds.Except(newGenreIds);
                IEnumerable<int> adds = newGenreIds.Except(oldGenreIds);

                deletes.ToList().ForEach(d => MovieGenreManager.Delete(id, d));
                adds.ToList().ForEach(a => MovieGenreManager.Insert(id, a));

                if (movieVM.File != null)
                {
                    movieVM.Movie.ImagePath = movieVM.File.FileName;
                    string path = _host.WebRootPath + "\\images\\";

                    using (var stream = System.IO.File.Create(path + movieVM.File.FileName))
                    {
                        movieVM.File.CopyTo(stream);
                        ViewBag.Message = "File Uploaded Successfully...";
                    }
                }

                int result = MovieManager.Update(movieVM.Movie, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(movieVM);
            }
        }

        private IEnumerable<int> GetObject()
        {
            return HttpContext.Session.GetObject<IEnumerable<int>>("genreids") ?? Enumerable.Empty<int>();
        }

        public IActionResult Delete(int id)
        {
            var item = MovieManager.LoadById(id);
            ViewBag.Title = "Delete " + item.Description;
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(int id, Movie movie, bool rollback = false)
        {
            try
            {
                int result = MovieManager.Delete(id, rollback);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Title = "Delete " + movie.Description;
                ViewBag.Error = ex.Message;
                return View(movie);
            }
        }


    }


}
