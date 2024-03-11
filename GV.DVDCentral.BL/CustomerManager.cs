using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL
{
    public static class CustomerManager
    {
        public static int Insert(string firstName, 
                                string lastName, 
                                string address, 
                                string city, 
                                string state, 
                                string zip,
                                string phone, 
                                int userId, 
                                ref int id, 
                                bool rollback = false)
        {
            try
            {
                Customer customer = new Customer();
                {
                    customer.FirstName = firstName;
                    customer.LastName = lastName;
                    customer.Address = address;
                    customer.City = city;
                    customer.State = state;
                    customer.ZIP = zip;
                    customer.Phone = phone;
                    customer.UserId = userId; 
                    customer.Id = id; 
                };

                int results = Insert(customer, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID
                id = customer.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int Insert(Customer customer, bool rollback = false)
        {


            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblCustomer entity = new tblCustomer();
                    entity.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(s => s.Id) + 1 : 1;

                    entity.FirstName = customer.FirstName;
                    entity.LastName = customer.LastName;
                    entity.Address = customer.Address;
                    entity.City = customer.City;
                    entity.State = customer.State;
                    entity.ZIP = customer.ZIP;
                    entity.Phone = customer.Phone;
                    entity.UserId = customer.UserId;


                    //IMPORTANT - BACK FILL THE ID
                    customer.Id = entity.Id;

                    dc.tblCustomers.Add(entity);
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

        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0; 
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(s => s.Id == customer.Id);

                    if (entity != null)
                    {
                        entity.Id = customer.Id;
                        entity.FirstName = customer.FirstName;
                        entity.LastName = customer.LastName;
                        entity.Address = customer.Address;
                        entity.City = customer.City;
                        entity.State = customer.State;
                        entity.ZIP = customer.ZIP;
                        entity.Phone = customer.Phone;
                        entity.UserId = customer.UserId;



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
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblCustomers.Remove(entity);
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

        public static Customer LoadById(int id)
        {
            try
            {


                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblCustomer entity = dc.tblCustomers.FirstOrDefault(d => d.Id == id);
                    if (entity != null)
                    {
                        return new Customer
                        {

                            Id = entity.Id,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            Address = entity.Address,
                            City = entity.City,
                            State = entity.State,
                            ZIP = entity.ZIP,
                            Phone = entity.Phone,
                            UserId = entity.UserId

                            
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

        public static List<Customer> Load()
        {

            try
            {
                List<Customer> list = new List<Customer>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from d in dc.tblCustomers
                     select new
                     {
                         d.Id,
                         d.FirstName,
                         d.LastName, 
                         d.Address,
                         d.City,
                         d.State,
                         d.ZIP,
                         d.Phone,
                         d.UserId

                         
                     })
                     .ToList()
                     .ForEach(customer => list.Add(new Customer
                     {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName, 
                        Address = customer.Address,
                        City = customer.City,
                        State = customer.State,
                        ZIP = customer.ZIP,
                        Phone = customer.Phone,
                        UserId = customer.UserId


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
