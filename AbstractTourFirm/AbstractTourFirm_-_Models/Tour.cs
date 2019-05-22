using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class Tour
    {
        public int Id { set; get; }
        [Required]
        public string TourName { set; get; }
        [Required]
        public string Country { set; get; }
        [Required]
        public int Cost { set; get; }
        public string Season { set; get; }
        [Required]
        public bool IsCredit { set; get; }
        [ForeignKey("TravelId")]
        public virtual List<TourForTravel> TourForTravels { set; get; }
    }
}
