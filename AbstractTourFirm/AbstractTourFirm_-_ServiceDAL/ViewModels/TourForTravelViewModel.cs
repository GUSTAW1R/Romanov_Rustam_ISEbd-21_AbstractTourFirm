using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___ServiceDAL.ViewModels
{
    public class TourForTravelViewModel
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public int TourId { get; set; }
        public int Count { get; set; }
        public DateTime? Date_Start { get; set; }
    }
}
