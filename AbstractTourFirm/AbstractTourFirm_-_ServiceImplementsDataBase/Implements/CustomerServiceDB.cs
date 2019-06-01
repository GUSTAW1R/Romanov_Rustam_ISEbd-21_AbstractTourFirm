using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___Models;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.Interface;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceImplementsDataBase.Implements
{
    public class CustomerServiceDB : ICustomerService
    {
        private AbstractTourFirmDbContext context;

        public CustomerServiceDB(AbstractTourFirmDbContext context)
        {
            this.context = context;
        }
        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = context.Customers.Select(rec => new
           CustomerViewModel
            {
                Id = rec.Id,
                Name = rec.Name,
                Login = rec.Login
            })
            .ToList();
            return result;
        }
        public CustomerViewModel GetElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    Name = element.Name,
                    Login = element.Login
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddNewCustomer(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Name ==
           model.Name);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Customers.Add(new Customer
            {
                Name = model.Name,
                Login = model.Login,
                Password = model.Password
            });
            context.SaveChanges();
        }
        public void CheckCustomer(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Login ==
           model.Login && rec.Id == model.Id && rec.Password == model.Password);
            if (element == null)
            {
                throw new Exception("Клиент с таким логином и паролем отсутствует");
            }
            context.SaveChanges();
        }
        public void DeleteCustomer(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
