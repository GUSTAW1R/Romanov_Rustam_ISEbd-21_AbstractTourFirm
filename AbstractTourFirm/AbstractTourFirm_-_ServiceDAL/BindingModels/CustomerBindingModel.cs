using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___ServiceDAL.BindingModels
{
    public class CustomerBindingModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Login { set; get; }
        public string Password { set; get; }
    }
}
