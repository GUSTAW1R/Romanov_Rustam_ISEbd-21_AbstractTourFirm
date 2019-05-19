using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceDAL.Interface
{
    public interface ITourService
    {
        List<TourViewModel> GetList();
        TourViewModel GetElement(int id);
        void AddElement(TourBindingModel model);
        void UpdElement(TourBindingModel model);
        void DelElement(int id);
        bool GetCreditInfo(int id);
    }
}
