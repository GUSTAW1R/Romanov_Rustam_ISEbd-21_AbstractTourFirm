using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class Travel
    {
        public int Id { set; get; }
        [Required]
        public string Name_Travel { set; get; }
        [Required]
        public int Final_Cost { set; get; }
        public bool Taxi { set; get; }
        public bool AllInclusive { set; get; }
        public bool Private_Guide { set; get; }
    }
}
