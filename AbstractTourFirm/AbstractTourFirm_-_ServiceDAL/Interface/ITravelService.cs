using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.ViewModels;

namespace AbstractTourFirm___ServiceDAL.Interface
{
    public interface ITravelService
    {
        List<TravelViewModel> GetList();
        TravelViewModel GetElement(int id);
        void AddElement(TravelBindingModel model);
        void UpdElement(TravelBindingModel model);
        void DelElement(int id);
        bool GetCredit(int id);
    }
}
