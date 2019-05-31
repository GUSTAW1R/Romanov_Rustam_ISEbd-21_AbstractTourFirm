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
    public class TourServiceDB : ITourService
    {
        private AbstractTourFirmDbContext context;

        public TourServiceDB(AbstractTourFirmDbContext context)
        {
            this.context = context;
        }
        public List<TourViewModel> GetList()
        {
            List<TourViewModel> result = context.Tours.Select(rec => new
           TourViewModel
            {
                Id = rec.Id,
                TourName = rec.TourName,
                Country = rec.Country,
                Cost = rec.Cost,
                Season = rec.Season,
                IsCredit = rec.IsCredit
            })
            .ToList();
            return result;
        }
        public TourViewModel GetElement(int id)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TourViewModel
                {
                    Id = element.Id,
                    TourName = element.TourName,
                    Country = element.Country,
                    Cost = element.Cost,
                    Season = element.Season,
                    IsCredit = element.IsCredit
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(TourBindingModel model)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.TourName ==
           model.TourName);
            if (element != null)
            {
                throw new Exception("Уже есть тур с таким названием");
            }
            context.Tours.Add(new Tour
            {
                TourName = model.TourName,
                Country = model.Country,
                Cost = model.Cost,
                Season = model.Season,
                IsCredit = model.IsCredit
            });
            context.SaveChanges();
        }
        public void UpdElement(TourBindingModel model)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.TourName ==
           model.TourName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Tours.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.TourName = model.TourName;
            element.Country = model.Country;
            element.Cost = model.Cost;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Tour element = context.Tours.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Tours.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
