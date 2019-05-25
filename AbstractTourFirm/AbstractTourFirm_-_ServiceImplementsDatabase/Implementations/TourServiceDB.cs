using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___ServiceDAL.Interface;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceImplementsDatabase.Implementations
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
                DocumentsName = rec.DocumentsName,
                Price = rec.Price,
                DocumentBlank = context.DocumentBlanks
            .Where(recPC => recPC.DocumentsId == rec.Id)
           .Select(recPC => new DocumentBlankViewModel
           {
               Id = recPC.Id,
               DocumentsId = recPC.DocumentsId,
               BlankId = recPC.BlankId,
               BlankName = recPC.Blank.BlankName,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public DocumentsViewModel GetElement(int id)
        {
            Documents element = context.Documents.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new DocumentsViewModel
                {
                    Id = element.Id,
                    DocumentsName = element.DocumentsName,
                    Price = element.Price,
                    DocumentBlank = context.DocumentBlanks
 .Where(recPC => recPC.DocumentsId == element.Id)
 .Select(recPC => new DocumentBlankViewModel
 {
     Id = recPC.Id,
     DocumentsId = recPC.DocumentsId,
     BlankId = recPC.DocumentsId,
     BlankName = recPC.Blank.BlankName,
     Count = recPC.Count
 })
 .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(DocumentsBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Documents element = context.Documents.FirstOrDefault(rec =>
                   rec.DocumentsName == model.DocumentsName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Documents
                    {
                        DocumentsName = model.DocumentsName,
                        Price = model.Price
                    };
                    context.Documents.Add(element);
                    context.SaveChanges();
                    // убираем дубли по компонентам
                    var groupComponents = model.DocumentBlank
                     .GroupBy(rec => rec.BlankId)
                    .Select(rec => new
                    {
                        BlankId = rec.Key,
                        Count = rec.Sum(r => r.Count)
                    });
                    // добавляем компоненты
                    foreach (var groupComponent in groupComponents)
                    {
                        context.DocumentBlanks.Add(new DocumentBlank
                        {
                            DocumentsId = element.Id,
                            BlankId = groupComponent.BlankId,
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
        public void UpdElement(DocumentsBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Documents element = context.Documents.FirstOrDefault(rec =>
                   rec.DocumentsName == model.DocumentsName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Documents.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.DocumentsName = model.DocumentsName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.DocumentBlank.Select(rec =>
                   rec.BlankId).Distinct();
                    var updateComponents = context.DocumentBlanks.Where(rec =>
                   rec.BlankId == model.Id && compIds.Contains(rec.BlankId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.DocumentBlank.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.DocumentBlanks.RemoveRange(context.DocumentBlanks.Where(rec =>
                    rec.BlankId == model.Id && !compIds.Contains(rec.BlankId)));
                    context.SaveChanges();
                    // новые записи
                    var groupComponents = model.DocumentBlank
                    .Where(rec => rec.Id == 0)
                   .GroupBy(rec => rec.BlankId)
                   .Select(rec => new
                   {
                       ComponentId = rec.Key,
                       Count = rec.Sum(r => r.Count)
                   });
                    foreach (var groupComponent in groupComponents)
                    {
                        DocumentBlank elementPC =
                       context.DocumentBlanks.FirstOrDefault(rec => rec.BlankId == model.Id &&
                       rec.BlankId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.DocumentBlanks.Add(new DocumentBlank
                            {
                                DocumentsId = model.Id,
                                BlankId = groupComponent.ComponentId,
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
                    Documents element = context.Documents.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.DocumentBlanks.RemoveRange(context.DocumentBlanks.Where(rec =>
                        rec.DocumentsId == id));
                        context.Documents.Remove(element);
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
