using BankClient.Model;
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
        UserControl1 input = new();
        public MainWindow()
        {
            InitializeComponent();

            WindowManeger.mainWindow = this;
            Grid1.Children.Add(input);
            
        }
        Repository repository = new();

        /// <summary>
        /// закрывает окно
        /// </summary>
        public void ClouseWindow()
        {
            //записывает в lable Login
            lbUser.Content = GlobalVar.Login;

            Grid1.Children.Clear();
          
        }/// <summary>
        /// очищает окно и открывает следующее
        /// </summary>
        /// <param name="control"></param>
        public void ShowWindow(UserControl control)
        {
            Grid1.Children.Clear();
            Grid1.Children.Add(control);

            //записывает в lable Login
            lbUser.Content = GlobalVar.Login;
        }
        /// <summary>
        /// запускает окно "выпуск карты"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCardAdd_Click(object sender, RoutedEventArgs e)
        {
            CardAdd cardAdd = new();
            ShowWindow(cardAdd);
        }

        /// <summary>
        /// запускает окно "выпуск карты"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBalanceAdd_Click(object sender, RoutedEventArgs e)
        {
            BalanceAdd balanceAdd = new();
            ShowWindow(balanceAdd);
        }
      /// <summary>
      /// запускает окно блокировки карты
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            Block block = new();
            ShowWindow(block);
        }
        /// <summary>
        /// запускает окно установки дневного лмимта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DailyLimit_Click(object sender, RoutedEventArgs e)
        {
            DailyLimit dailyLimit = new();
            ShowWindow(dailyLimit);
        }
    }
}
