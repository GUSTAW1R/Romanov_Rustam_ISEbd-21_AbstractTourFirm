using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public class Customer
    {
        public int Id { set; get; }
        [Required]
        public string Surname { set; get; }
        [Required]
        public string Name { set; get; }
        [Required]
        public string E_mail {set; get;}
        [Required]
        public string Login { set; get; }
        [Required]
        public string Password { set; get; }
    }
}
