using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AbstractTourFirm___ServiceDAL.Interface;
using AbstractTourFirm___ServiceImplementsDataBase.Implements;
using Unity;

namespace AbstractTourFirm
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IMainService, MainServiceDB>();
            container.RegisterType<ITourService, TourServiceDB>();
            container.RegisterType<ITravelService, TravelServiceDB>();
            container.RegisterType<ICustomerService, CustomerServiceDB>();
            var mainWindow = container.Resolve<SingInWindow>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }
    }
}
