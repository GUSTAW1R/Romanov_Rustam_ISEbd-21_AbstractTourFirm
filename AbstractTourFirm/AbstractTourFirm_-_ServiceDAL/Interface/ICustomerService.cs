using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceDAL.Interface
{
    public interface ICustomerService
    {
        List<CustomerViewModel> GetList();
        CustomerViewModel GetElement(int id);
        void AddNewCustomer(CustomerBindingModel model);
        void CheckCustomer(CustomerBindingModel model);
        void DeleteCustomer(int id);
    }
}
