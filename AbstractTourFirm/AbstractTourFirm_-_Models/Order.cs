using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int TravelId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public TravelStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
