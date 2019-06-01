using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___Models;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.Interface;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceImplementsDataBase.Implements
{
    public class MainServiceDB : IMainService
    {
        private AbstractTourFirmDbContext context;

        public MainServiceDB(AbstractTourFirmDbContext context)
        {
            this.context = context;
        }
        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = context.Orders.Select(rec => new OrderViewModel
            {
                Id = rec.Id,
                CustomerId = rec.CustomerId,
                TravelId = rec.TravelId,
                DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
            SqlFunctions.DateName("mm", rec.DateCreate) + " " +
            SqlFunctions.DateName("yyyy", rec.DateCreate),
                DateImplement = rec.DateImplement == null ? "" :
            SqlFunctions.DateName("dd",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("mm",
           rec.DateImplement.Value) + " " +
            SqlFunctions.DateName("yyyy",
           rec.DateImplement.Value),
                Status = rec.Status.ToString(),
                Count = rec.Count,
                Sum = rec.Sum,
                Name = rec.Customers.Name,
                TravelName = rec.Travels.Name_Travel
            })
            .ToList();
            return result;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            context.Orders.Add(new Order
            {
                CustomerId = model.CustomerId,
                TravelId = model.TravelId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = TravelStatus.Рассматривается
            });
            context.SaveChanges();
        }
        public void TakeOrderInWork(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != TravelStatus.Рассматривается)
            {
                throw new Exception("Заказ не в статусе \"Рассматривается\"");
            }
            element.DateImplement = DateTime.Now;
            element.Status = TravelStatus.Ждёт_оплаты;
            context.SaveChanges();

        }
        public void TakeOrderInCredit(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id ==
            model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != TravelStatus.Рассматривается)
            {
                throw new Exception("Заказ не в статусе \"Рассматривается\"");
            }
            element.DateImplement = DateTime.Now;
            element.Status = TravelStatus.В_Рассрочке;
            context.SaveChanges();
        }

        public void FinishOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != TravelStatus.Ждёт_оплаты)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            element.Status = TravelStatus.Ждёт_оплаты;
            context.SaveChanges();
        }
        public void PayOrder(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != TravelStatus.Ждёт_оплаты)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = TravelStatus.Заказ_оплачен;
            context.SaveChanges();
        }
        public void GoTravel(OrderBindingModel model)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if(element.Status == TravelStatus.В_Рассрочке)
            {
                throw new Exception("Рассрочка не оплачена");
            }
            if (element.Status != TravelStatus.Заказ_оплачен)
            {
                throw new Exception("Заказ не в статусе \"Оплачено\"");
            }
            element.Status = TravelStatus.Заказ_оплачен;
            context.SaveChanges();
        }
    }
}
