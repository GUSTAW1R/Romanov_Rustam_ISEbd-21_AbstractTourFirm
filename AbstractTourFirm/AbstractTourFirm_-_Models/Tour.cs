using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class Tour
    {
        public int Id { set; get; }
        public string TourName { set; get; }
        public string Country { set; get; }
        public int Cost { set; get; }
        public string Season { set; get; }
        public bool IsCredit { set; get; }
    }
}
