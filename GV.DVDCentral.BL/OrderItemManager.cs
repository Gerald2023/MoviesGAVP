using GV.DVDCentral.BL.Models;
using GV.DVDCentral.PL;
using Microsoft.EntityFrameworkCore.Storage;

namespace GV.DVDCentral.BL
{
    public static class OrderItemManager
    {
        public static int Insert(int orderId, int movieId, int quantity, double cost,  ref int id, bool rollback = false)
        {
            try
            {
                OrderItem orderItem = new OrderItem();
                {
                    orderItem.OrderId = orderId;
                    orderItem.MovieId = movieId;
                    orderItem.Quantity = quantity;
                    orderItem.Cost = cost;

                    orderItem.Id = id;
                };

                int results = Insert(orderItem, rollback);

                //IMPORTANT - BACKFILL THE REFERENCE ID
                id = orderItem.Id;

                return results;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public static int Insert(OrderItem orderItem, bool rollback = false)
        {


            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblOrderItem entity = new tblOrderItem();
                    entity.Id = dc.tblOrderItems.Any() ? dc.tblOrderItems.Max(s => s.Id) + 1 : 1;

                    entity.OrderId = orderItem.OrderId;
                    entity.MovieId = orderItem.MovieId;
                    entity.Quantity = orderItem.Quantity;
                    entity.Cost = orderItem.Cost;


                    //IMPORTANT - BACK FILL THE ID
                    orderItem.Id = entity.Id;

                    dc.tblOrderItems.Add(entity);
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

        public static int Update(OrderItem orderItem, bool rollback = false)
        {
            try
            {
                int results = 0; 
                using (DVDCentralEntities dc = new DVDCentralEntities()) //blocked scope
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();


                    //Get the row that we are trying to update
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(s => s.Id == orderItem.Id);

                    if (entity != null)
                    {
                     
                        entity.Quantity = orderItem.Quantity;
                        entity.Cost = orderItem.Cost;


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
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(s => s.Id == id);

                    if (entity != null)
                    {
                        dc.tblOrderItems.Remove(entity);
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

        public static OrderItem LoadById(int id)
        {
            try
            {


                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblOrderItem entity = dc.tblOrderItems.FirstOrDefault(d => d.Id == id);
                    if (entity != null)
                    {
                        return new OrderItem
                        {

                            Id = entity.Id,
                            OrderId = entity.OrderId,
                            MovieId = entity.MovieId,
                            Quantity = entity.Quantity,
                            Cost = entity.Cost
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

        public static List<OrderItem> Load()
        {

            try
            {
                List<OrderItem> list = new List<OrderItem>();
                using (DVDCentralEntities dc = new DVDCentralEntities()) // Blocked Scope
                {
                    (from d in dc.tblOrderItems
                     select new
                     {
                         d.Id,
                         d.OrderId,
                         d.MovieId,
                         d.Quantity,
                         d.Cost
                         
                     })
                     .ToList()
                     .ForEach(orderItem => list.Add(new OrderItem
                     {
                        Id = orderItem.Id,
                        OrderId = orderItem.OrderId,
                        MovieId = orderItem.MovieId,
                        Quantity = orderItem.Quantity,
                        Cost = orderItem.Cost

                     }));
                }

                return list;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static List<OrderItem> LoadByOrderId(int orderId)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    List<OrderItem> orderItems = new List<OrderItem>();

                    var entities = dc.tblOrderItems.Where(item => item.OrderId == orderId).ToList();

                    foreach (var entity in entities)
                    {
                        orderItems.Add(new OrderItem
                        {
                            Id = entity.Id,
                            OrderId = entity.OrderId,
                            Quantity = entity.Quantity,
                            MovieId = entity.MovieId,
                            Cost = entity.Cost

                        });
                    }
                    return orderItems;
                }
            }

            catch (Exception)
            {
                throw;
            }

        }
    }
}
