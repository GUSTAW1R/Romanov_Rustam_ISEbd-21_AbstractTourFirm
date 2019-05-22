using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class TourForTravel
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public int TourId { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public DateTime? Date_Start { get; set; }
        public virtual Tour Tours { set; get; }
        public virtual Travel Travels { set; get; }
    }
}
