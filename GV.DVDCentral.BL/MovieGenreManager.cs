using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL
{
    public static class MovieGenreManager
    {

        /*        public static int Insert(int movieId, int genreId, ref int id, bool rollback = false)
                {
                    try
                    {
                        tblMovieGenre movieGenre = new tblMovieGenre();
                        {
                            movieGenre.Id = id;
                            movieGenre.MovieId = movieId;
                            movieGenre.GenreId = genreId;
                        };

                        int results = Insert(movieGenre, rollback);

                        //IMPORTANT - BACKFILL THE REFERENCE ID
                        id = movieGenre.Id;

                        return results;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
        */

        public static void Insert(int movieId, int genreId, bool rollback = false)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovieGenre tblMovieGenre = new tblMovieGenre();
                    tblMovieGenre.MovieId = movieId;
                    tblMovieGenre.GenreId = genreId;
                    tblMovieGenre.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(sa => sa.Id) + 1 : 1;
               
                    dc.tblMovieGenres.Add(tblMovieGenre);
                    dc.SaveChanges();

                }

            }
            catch (Exception ex) 
            {

                throw;
            }

            /*
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovieGenre entity = new tblMovieGenre();
                    entity.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(s => s.Id) + 1 : 1;

                    entity.MovieId = movieId;
                    entity.GenreId = genreId;



                    //IMPORTANT - BACK FILL THE ID
                    //dc.Id = entity.Id;

                    dc.tblMovieGenres.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                }

                return results


            }
            catch (Exception)
            {
                throw;
            }*/

        }

        public static int Update(int movieId, int genreId, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    //tblMovieGenre entity = new tblMovieGenre();

                    //Get the row that we are trying to update
                    tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(s => s.MovieId == movieId && s.GenreId == genreId);


                    //tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault(s => s.Id == movieGenre.Id);

                    if (entity != null)
                    {

                        entity.MovieId = movieId;
                        entity.GenreId = genreId;




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

        public static void Delete(int movieId, int genreId, bool rollback = false)
        {


            try
            {
              //  int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities()) 
                {
                  // IDbContextTransaction transaction = null;
                    //if (rollback) transaction = dc.Database.BeginTransaction();


                    tblMovieGenre? tblMovieGenre = dc.tblMovieGenres
                                                    .FirstOrDefault(s => s.MovieId == movieId 
                                                     && s.GenreId == genreId);

                    if (tblMovieGenre != null)
                    {
                        dc.tblMovieGenres.Remove(tblMovieGenre);
                        dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }
                   // if (rollback) transaction.Rollback();

                }
            }
            catch (Exception)
            {

                throw;
            }



        }




    }




}
