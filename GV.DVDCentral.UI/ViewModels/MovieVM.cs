using GV.DVDCentral.BL;
using GV.DVDCentral.BL.Models;

namespace GV.DVDCentral.UI.ViewModels
{
    public class MovieVM
    {
        public Movie Movie { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>(); // all the genres

        public IEnumerable<int> GenreIds { get; set; }  // old Ids. The ones that will be deleted 

        public List<Director> Directors { get; set; } = new List<Director>();

        public List<Rating> Ratings { get; set; } = new List<Rating>();

        public List<Format> Formats { get; set; } = new List<Format> { };    

        public IFormFile File { get; set; }  

        public MovieVM()
        {
            Genres = GenreManager.Load();
        }

        public MovieVM(int id)
        {
            Movie = MovieManager.LoadById(id);

            Genres = GenreManager.Load(); // Load genres for the dropdown
            Ratings = RatingManager.Load();
            Formats = FormatManager.Load();
            Directors = DirectorManager.Load();
            GenreIds = Movie.Genres.Select(a => a.Id);
        }



    }
}
