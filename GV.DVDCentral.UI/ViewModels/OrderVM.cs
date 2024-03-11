using GV.DVDCentral.BL;

namespace GV.DVDCentral.UI.ViewModels
{
    public class OrderVM
    {

        public Order Order { get; set; }
        //  public IEnumerable<OrderItem> OrderItems { get; set; }



        /*        public int Quantity { get; set; }
                public int OrderItemsId { get; set; }
                public int MovieId { get; set; }
                public double Cost { get; set; }*/

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        public IEnumerable<int> OrderItemsIds { get; set; }
/*
        public int Id => Order.Id;
        public int CustomerId => Order.CustomerId;
        public DateTime OrderDate => Order.OrderDate;
        public int UserId => Order.UserId;
        public DateTime ShipDate => Order.ShipDate;*/


        public OrderVM(int id) 
        {
            OrderItems = OrderItemManager.Load();
            Order = OrderManager.LoadById(id);

            OrderItemsIds = Order.OrderItems.Select(a => a.Id);

        
        }
    }
}
