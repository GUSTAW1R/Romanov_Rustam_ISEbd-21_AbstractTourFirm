using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AbstractTourFirm___ServiceDAL.BindingModels;
using AbstractTourFirm___ServiceDAL.Interface;
using AbstractTourFirm___ServiceDAL.ViewModels;
using Unity;

namespace AbstractTourFirm
{
    /// <summary>
    /// Логика взаимодействия для SingInWindow.xaml
    /// </summary>
    public partial class SingInWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ICustomerService service;
        private int? id;

        public SingInWindow(ICustomerService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    CustomerViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxLogin.Text = view.Login;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxLogin.Text))
            {
                MessageBox.Show("Заполните поле Login", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPass.Text))
            {
                MessageBox.Show("Заполните поле Password", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.CheckCustomer(new CustomerBindingModel
                    {
                        Id = id.Value,
                        Login = textBoxLogin.Text,
                        Password = textBoxPass.Text
                    });
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<RegistrationWindow>();
            window.ShowDialog();
        }
    }
}
