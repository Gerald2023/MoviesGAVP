using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL
{
    public static class RatingManager
    {
        public static int Insert(string description, 
                                 ref int id, 
                                 bool rollback = false)
        {
            try
            {
                Rating rating = new Rating();
                {
                    rating.Description = description;

                };

                int results = Insert(rating, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID
                id = rating.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static int Insert(Rating rating, bool rollback = false)
        {


            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblRating entity = new tblRating();
                    entity.Id = dc.tblRatings.Any() ? dc.tblRatings.Max(s => s.Id) + 1 : 1;

                    entity.Description = rating.Description;


                    //IMPORTANT - BACK FILL THE ID
                    rating.Id = entity.Id;

                    dc.tblRatings.Add(entity);
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

        public static int Update(Rating rating, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    tblRating entity = dc.tblRatings.FirstOrDefault(s => s.Id == rating.Id);

                    if (entity != null)
                    {
                        entity.Id = rating.Id;
                        entity.Description = rating.Description;



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
                    tblRating entity = dc.tblRatings.FirstOrDefault(r => r.Id == id);

                    if (entity != null)
                    {
                        dc.tblRatings.Remove(entity);
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

        public static Rating LoadById(int id)
        {
            try
            {


                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblRating entity = dc.tblRatings.FirstOrDefault(r => r.Id == id);
                    if (entity != null)
                    {
                        return new Rating
                        {

                            Id = entity.Id,
                            Description = entity.Description

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

        public static List<Rating> Load()
        {

            try
            {
                List<Rating> list = new List<Rating>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from r in dc.tblRatings
                     select new
                     {
                         r.Id,
                         r.Description

                     })
                     .ToList()
                     .ForEach(rating => list.Add(new Rating
                     {
                         Id = rating.Id,
                         Description = rating.Description

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
