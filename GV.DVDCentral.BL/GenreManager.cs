using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL
{
    public static class GenreManager
    {
        public static int Insert(string description, ref int id, bool rollback = false)
        {
            try
            {
                Genre genre = new Genre();
                {
                    genre.Description = description;

                };

                int results = Insert(genre, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID
                id = genre.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Genre genre, bool rollback = false)
        {


            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblGenre entity = new tblGenre();
                    entity.Id = dc.tblGenres.Any() ? dc.tblGenres.Max(s => s.Id) + 1 : 1;

                    entity.Description = genre.Description;

                    foreach(Movie movie in genre.Movies)
                    {
                        results += MovieManager.Insert(movie, rollback);
                    }


                    //IMPORTANT - BACK FILL THE ID
                    genre.Id = entity.Id;

                    dc.tblGenres.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Update(Genre genre, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    tblGenre entity = dc.tblGenres.FirstOrDefault(s => s.Id == genre.Id);

                    if (entity != null)
                    {
                        entity.Id = genre.Id;
                        entity.Description = genre.Description;



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
                    tblGenre entity = dc.tblGenres.FirstOrDefault(g => g.Id == id);

                    if (entity != null)
                    {
                        dc.tblGenres.Remove(entity);
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

        public static Genre LoadById(int id)
        {
            try
            {


                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblGenre entity = dc.tblGenres.FirstOrDefault(g => g.Id == id);
                    if (entity != null)
                    {
                        return new Genre
                        {

                            Id = entity.Id,
                            Description = entity.Description,
                            Movies = MovieManager.Load(entity.Id)

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

        public static List<Genre> Load(int movieId)
        {

            try
            {
                List<Genre> list = new List<Genre>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from g in dc.tblGenres
                     join mg in dc.tblMovieGenres on g.Id equals mg.GenreId
                     where mg.MovieId == movieId
                     select new
                     {
                         g.Id,
                         g.Description

                     })
                     .ToList()
                     .ForEach(genre => list.Add(new Genre
                     {
                         Id = genre.Id,
                         Description = genre.Description,
                         Movies = MovieManager.Load(genre.Id)

                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public static List<Genre> Load()
        {

            try
            {
                List<Genre> list = new List<Genre>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from g in dc.tblGenres
                     select new
                     {
                         g.Id,
                         g.Description

                     })
                     .ToList()
                     .ForEach(genre => list.Add(new Genre
                     {
                         Id = genre.Id,
                         Description = genre.Description,
                         Movies = MovieManager.Load(genre.Id)

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
