using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class Travel
    {
        public int Id { set; get; }
        public string Name_Travel { set; get; }
        public int Final_Cost { set; get; }
        public bool Taxi { set; get; }
        public bool AllInclusive { set; get; }
        public bool Private_Guide { set; get; }
    }
}
