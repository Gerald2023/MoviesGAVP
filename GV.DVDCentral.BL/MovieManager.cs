using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL
{
    public static class MovieManager
    {
        public static int Insert(string title,
                                string description,
                                double cost,
                                int ratingId,
                                int directorId,
                                int formatId,
                                int inStkQty,
                                string imagePath,
                                ref int id,
                                bool rollback = false)
        {
            try
            {
                Movie movie = new Movie();
                {
                    movie.Title = title;
                    movie.Description = description;
                    movie.Cost = cost;
                    movie.RatingId = ratingId;
                    movie.FormatId = formatId;
                    movie.DirectorId = directorId;
                    movie.InStkQty = inStkQty;
                    movie.ImagePath = imagePath;


                };

                int results = Insert(movie, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID
                id = movie.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /* public static int Insert(Movie movie, bool rollback = false)
         {


             try
             {
                 int results = 0;

                 using (DVDCentralEntities dc = new DVDCentralEntities())
                 {
                     IDbContextTransaction transaction = null;
                     if (rollback) transaction = dc.Database.BeginTransaction();

                     tblMovie entity = new tblMovie();
                     entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(m => m.Id) + 1 : 1;

                     entity.Title = movie.Title;
                     entity.Description = movie.Description;
                     entity.Cost = movie.Cost;
                     entity.RatingId = movie.RatingId;
                     entity.FormatId = movie.FormatId;
                     entity.DirectorId = movie.DirectorId;
                     entity.InStkQty = movie.InStkQty;
                     entity.ImagePath = movie.ImagePath;



                     //IMPORTANT - BACK FILL THE ID
                     movie.Id = entity.Id;

                     dc.tblMovies.Add(entity);
                     results = dc.SaveChanges();

                     if (rollback) transaction.Rollback();
                 }

                 return results;
             }
             catch (Exception)
             {
                 throw;
             }
         }*/
        public static int Insert(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entity = new tblMovie();
                    entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(m => m.Id) + 1 : 1;

                    entity.Title = movie.Title;
                    entity.Description = movie.Description;
                    entity.Cost = movie.Cost;
                    entity.RatingId = movie.RatingId;
                    entity.FormatId = movie.FormatId;
                    entity.DirectorId = movie.DirectorId;
                    entity.InStkQty = movie.InStkQty;
                    entity.ImagePath = movie.ImagePath;

                    // IMPORTANT - BACK FILL THE ID
                    movie.Id = entity.Id;

                    dc.tblMovies.Add(entity);
                    results = dc.SaveChanges();

                    // Insert into tblMovieGenre
/*                    if (results > 0)
                    {
                        foreach (var genreId in movie.Genres.Select(g => g.Id))
                        {
                            MovieGenreManager.Insert(movie.Id, genreId, rollback);
                        }
                    }*/

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public static int Update(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.Title = movie.Title;
                        entity.Description = movie.Description;
                        entity.ImagePath = movie.ImagePath;


                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }


        }


        public static int Delete(int id, bool rollback = false)
        {


            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to delete
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        dc.tblMovies.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                    if (rollback) transaction.Rollback();

                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }



        }

        public static Movie LoadById(int id)
        {
            try
            {


                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie entity = dc.tblMovies.FirstOrDefault(d => d.Id == id);
                    if (entity != null)
                    {
                        return new Movie
                        {

                            Id = entity.Id,
                            Title = entity.Title,
                            Description = entity.Description,
                            Cost = entity.Cost,
                            RatingId = entity.RatingId,
                            FormatId = entity.FormatId,
                            DirectorId = entity.DirectorId,
                            InStkQty = entity.InStkQty,
                            ImagePath = entity.ImagePath,
                            Genres = GenreManager.Load(id)


                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        /*        public static List<Movie> Load(int? genreId = null) 
                {

                    try
                    {
                        List<Movie> list = new List<Movie>();
                        using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                        {
                            var query = from m in dc.tblMovies
                                        join mg in dc.tblMovieGenres on m.Id equals mg.MovieId into movieGenres
                                        from movieGenre in movieGenres.DefaultIfEmpty()
                                        join r in dc.tblRatings on m.RatingId equals r.Id
                                        join f in dc.tblFormats on m.FormatId equals f.Id
                                        join d in dc.tblDirectors on m.DirectorId equals d.Id
                                        where movieGenre.GenreId == genreId || genreId == null
                                        select new
                                        {
                                            m.Id,
                                            m.Title,
                                            m.Cost,
                                            m.InStkQty,
                                            Rating = r.Description,
                                            Format = f.Description,
                                            DirectorFullName = d.FirstName + " " + d.LastName,
                                            m.ImagePath




                                        };

                            query.Distinct().ToList().ForEach(movie => list.Add(new Movie
                            {
                                Id = movie.Id,
                                Title = movie.Title,
                                Cost = movie.Cost,
                                InStkQty = movie.InStkQty,
                                Rating = movie.Rating,
                                Format = movie.Format,
                                DirectorFullName = movie.DirectorFullName,
                                ImagePath = movie.ImagePath
                            }));
                        }

                        return list;
                    }
                    catch (Exception)
                    {
                        throw;
                    }



                }*/



        public static List<Movie> Load(int? genreId = null)
        {

            try
            {
                List<Movie> list = new List<Movie>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from m in dc.tblMovies

                     join mg in dc.tblMovieGenres on m.Id equals mg.MovieId

                     join r in dc.tblRatings on m.RatingId equals r.Id
                     join f in dc.tblFormats on m.FormatId equals f.Id
                     join d in dc.tblDirectors on m.DirectorId equals d.Id
                     where mg.GenreId == genreId || genreId == null
                     select new
                     {
                         m.Id,
                         m.Title,
                         m.Cost,
                         m.InStkQty,
                         Rating = r.Description,
                         Format = f.Description,
                         DirectorFullName = d.FirstName + " " + d.LastName,
                         m.ImagePath


                     }).Distinct().ToList()

                     .ForEach(movie => list.Add(new Movie
                     {

                         Id = movie.Id,

                         Title = movie.Title,
                         Cost = movie.Cost,
                         InStkQty = movie.InStkQty,
                         Rating = movie.Rating,
                         Format = movie.Format,
                         DirectorFullName = movie.DirectorFullName,
                         ImagePath = movie.ImagePath






                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
    
}
