using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;

namespace GV.DVDCentral.BL
{
    public static class OrderManager
    {
        public static int Insert(int customerId, 
                                int userId, ref int id,
                                bool rollback = false)
        {
            try
            {
                Order order = new Order();
                {
                    order.CustomerId = customerId;
                    order.OrderDate = DateTime.Now;
                    order.UserId = userId;
                    order.ShipDate = DateTime.Now;


                };

                int results = Insert(order, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID
                id = order.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int Insert(Order order, bool rollback = false) // Id by reference
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrder entity = new tblOrder();
                    entity.Id = dc.tblOrders.Any() ? dc.tblOrders.Max(s => s.Id) + 1 : 1;
                    entity.CustomerId = order.CustomerId;
                    entity.OrderDate = order.OrderDate;
                    entity.UserId = order.UserId;
                    entity.ShipDate = order.ShipDate;

                    // Inserts OrderItems objects into the list at Order
                    foreach (OrderItem orderItem in order.OrderItems)
                    {
                        // set the orderId on tblOrderItem
                        //orderItem.OrderId = order.Id;
                        results += OrderItemManager.Insert(orderItem, rollback);
                    }

                    //entity.OrderItems = order.OrderItems;   

                    // IMPORTANT - BACK FILL THE ID 
                    order.Id = entity.Id;

                    dc.tblOrders.Add(entity);
                    results += dc.SaveChanges(); // Make sure to add the += and not only =

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception) { throw; }
        }
        public static int Update(Order order, bool rollback = false)
        {
            try
            {
                int results = 0; 
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == order.Id);

                    if (entity != null)
                    {
                        entity.Id = order.Id;
                        entity.CustomerId = order.CustomerId;
                        entity.OrderDate = DateTime.Now;
                        entity.UserId = order.UserId;
                        entity.ShipDate = DateTime.Now;


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
                    tblOrder entity = dc.tblOrders.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblOrders.Remove(entity);
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

        public static Order LoadById(int id)
        {
            try
            {

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {

                    var entity = (from d in dc.tblOrders
                                  join oi in dc.tblOrderItems on d.Id equals oi.OrderId
                                  where oi.OrderId == id
                                  select new
                                  {

                                      d.Id,
                                      d.CustomerId,
                                      d.UserId,
                                      d.OrderDate,
                                      d.ShipDate

                                  }).FirstOrDefault();

/*                    tblOrder entity = dc.tblOrders.FirstOrDefault(d => d.Id == id);
*/
                      if (entity != null)
                    {
                        return new Order
                        {

                            Id = entity.Id,
                            CustomerId = entity.CustomerId,
                            OrderDate = entity.OrderDate,
                            UserId = entity.UserId,
                            ShipDate = entity.ShipDate,
                            OrderItems = OrderItemManager.LoadByOrderId(id)





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




        public static List<Order> Load(int? CustomerId = null)
        {

            try
            {
                List<Order> list = new List<Order>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from d in dc.tblOrders
                     join oi in dc.tblOrderItems on d.Id equals oi.Id
                     join c in dc.tblCustomers on d.CustomerId equals c.Id
                     where d.CustomerId == CustomerId || CustomerId == null
                     select new
                     {
                         d.Id,
                         d.CustomerId,
                         d.UserId,
                         d.OrderDate,
                         d.ShipDate,



                     })
                     .ToList()
                     .ForEach(order => list.Add(new Order
                     {
                         Id = order.Id,
                         CustomerId = order.CustomerId,
                         UserId = order.UserId,
                         OrderDate = order.OrderDate,
                         ShipDate = order.ShipDate


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
