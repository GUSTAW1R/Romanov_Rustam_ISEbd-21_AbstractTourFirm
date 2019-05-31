using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractTourFirm___Models
{
    public enum TravelStatus
    {
        Рассматривается = 0,
        Ждёт_оплаты = 1,
        Рассрочка_не_выплачена = 2,
        Заказ_оплачен = 3,
        В_Рассрочке = 4,
        СЧАСТЛИВОГО_ПУТИ = 5
    }
}
