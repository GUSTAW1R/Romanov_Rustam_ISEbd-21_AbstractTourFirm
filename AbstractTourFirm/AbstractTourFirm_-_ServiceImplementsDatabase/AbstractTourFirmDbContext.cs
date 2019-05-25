using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___Models;

namespace AbstractTourFirm___ServiceImplementsDatabase
{
    public class AbstractTourFirmDbContext : DBContext
    {
        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<TourForTravel> TourForTravels { get; set; }
    }
}
