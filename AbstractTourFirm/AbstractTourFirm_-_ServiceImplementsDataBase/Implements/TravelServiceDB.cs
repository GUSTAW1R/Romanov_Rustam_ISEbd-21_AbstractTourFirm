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
    public class TravelServiceDB : ITravelService
    {
        private AbstractTourFirmDbContext context;
        public TravelServiceDB(AbstractTourFirmDbContext context)
        {
            this.context = context;
        }
        public List<TravelViewModel> GetList()
        {
            List<TravelViewModel> result = context.Travels.Select(rec => new
           TravelViewModel
            {
                Id = rec.Id,
                Name_Travel = rec.Name_Travel,
                Final_Cost = rec.Final_Cost,
                TourForTravels = context.TourForTravels
            .Where(recPC => recPC.TravelId == rec.Id)
           .Select(recPC => new TourForTravelViewModel
           {
               Id = recPC.Id,
               TravelId = recPC.TravelId,
               TourId = recPC.TourId,
               Count = recPC.Count,
               Date_Start = recPC.Date_Start
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public TravelViewModel GetElement(int id)
        {
            Travel element = context.Travels.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new TravelViewModel
                {
                    Id = element.Id,
                Name_Travel = element.Name_Travel,
                Final_Cost = element.Final_Cost,
                TourForTravels = context.TourForTravels
            .Where(recPC => recPC.TravelId == recPC.Id)
           .Select(recPC => new TourForTravelViewModel
           {
               Id = recPC.Id,
               TravelId = recPC.TravelId,
               TourId = recPC.TourId,
               Count = recPC.Count,
               Date_Start = recPC.Date_Start
           })
           .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(TravelBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec =>
                   rec.Name_Travel == model.Name_Travel);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Travel
                    {
                        Name_Travel = model.Name_Travel,
                        Final_Cost = model.Final_Cost
                    };
                    context.Travels.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.TourForTravels
                     .GroupBy(rec => rec.TourId)
                    .Select(rec => new
                    {
                        TourId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.TourForTravels.Add(new TourForTravel
                        {
                            TravelId = element.Id,
                            TourId = groupComponent.TourId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(TravelBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec =>
                   rec.Name_Travel == model.Name_Travel && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Travels.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.Name_Travel = model.Name_Travel;
                    element.Final_Cost = model.Final_Cost;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.TourForTravels.Select(rec =>
                   rec.TourId).Distinct();
                    var updateComponents = context.TourForTravels.Where(rec =>
                   rec.TourId == model.Id && compIds.Contains(rec.TourId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.TourForTravels.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.TourForTravels.RemoveRange(context.TourForTravels.Where(rec =>
                    rec.TourId == model.Id && !compIds.Contains(rec.TourId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.TourForTravels
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.TourId)
                   .Select(rec => new
                   {
                       ComponentId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupComponent in groupComponents)
                    {
                        TourForTravel elementPC =
                       context.TourForTravels.FirstOrDefault(rec => rec.TourId == model.Id &&
                       rec.TourId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.TourForTravels.Add(new TourForTravel
                            {
                                TravelId = model.Id,
                                TourId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Travel element = context.Travels.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.TourForTravels.RemoveRange(context.TourForTravels.Where(rec =>
                        rec.TravelId == id));
                        context.Travels.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
