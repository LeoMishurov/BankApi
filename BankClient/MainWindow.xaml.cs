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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControl1 uc1 = new();
        public MainWindow()
        {
            InitializeComponent();
            Grid1.Children.Add(uc1);
        }
        Repository repository = new();

        /// <summary>
        /// регистрация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // обращаться к асинхронному методу мы можем из асинхронного метода
        private async void btnRegistr_Click(object sender, RoutedEventArgs e)
        {

            await repository.Registration(uc1.tbLogin.Text, uc1.tbPassword.Text);
            await repository.Authorization(uc1.tbLogin.Text, uc1.tbPassword.Text);
            Grid1.Children.Remove(uc1);
            //await repository.Registration(tbLogin.Text, tbPassword.Text);
        }
        /// <summary>
        /// авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
          await repository.Authorization(uc1.tbLogin.Text, uc1.tbPassword.Text);
            Grid1.Children.Remove(uc1.Grid2);
        }
    }
}
