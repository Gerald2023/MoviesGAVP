using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GV.DVDCentral.BL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        public DateTime ShipDate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public int Quantity { get; set; }
        public int OrderItemsId { get; set; }
        public int MovieId { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
    }
}
