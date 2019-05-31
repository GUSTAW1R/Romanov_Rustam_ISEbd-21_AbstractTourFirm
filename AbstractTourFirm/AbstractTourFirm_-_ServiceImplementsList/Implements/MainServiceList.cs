using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___Models;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.Interface;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceImplementsList.Implements
{
    public class MainServiceList : IMainService
    {
        private DataListSingleton source;
        public MainServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetList()
        {
            List<OrderViewModel> result = source.Orders
              .Select(rec => new OrderViewModel
              {
                  Id = rec.Id,
                  CustomerId = rec.CustomerId,
                  TravelId = rec.TravelId,
                  DateCreate = rec.DateCreate.ToLongDateString(),
                  DateImplement = rec.DateImplement?.ToLongDateString(),
                  Status = rec.Status.ToString(),
                  Count = rec.Count,
                  Sum = rec.Sum,
                  Name = source.Customers.FirstOrDefault(recC => recC.Id == rec.CustomerId)?.Name,
                  TravelName = source.Travels.FirstOrDefault(recP => recP.Id == rec.TravelId)?.Name_Travel,
              }).ToList();
            return result;
        }
        public void CreateOrder(OrderBindingModel model)
        {
            int maxId = source.Orders.Count > 0 ? source.Orders.Max(rec => rec.Id) : 0;
            source.Orders.Add(new Order
            {
                Id = maxId + 1,
                CustomerId = model.CustomerId,
                TravelId = model.TravelId,
                DateCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = TravelStatus.Рассматривается
            });
        }
        public void TakeOrderInWork(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
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
        }
        public void TakeOrderInCredit(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != TravelStatus.Рассматривается)
            {
                throw new Exception("Заказ не в статусе \"Рассматривается\"");
            }

            element.DateImplement = DateTime.Now;
            element.Status = TravelStatus.;
        }
        public void FinishOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != TravelStatus.Ждёт_оплаты)
            {
                throw new Exception("Заказ не в статусе \"Обрабатывается\"");
            }
            element.Status = TravelStatus.Ждёт_оплаты;
        }
        public void PayOrder(OrderBindingModel model)
        {
            Order element = source.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            if (element.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            element.Status = OrderStatus.Оплачен;
        }
    }
}
