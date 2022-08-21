using BankClient.Model;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private ListItems listItems = new();
        public MainWindow()
        {
            InitializeComponent();

            lbCards.ItemsSource = listItems.Cards;// привязка данных

            WindowManeger.mainWindow = this;
            Grid1.Children.Add(input);
            
        }
        Repository repository = new();

        
        private class ListItems
        {
            // ObservableCollection - автоммтически создает событие и обновляет
            // коллекцию item после удаления или добавления элимента
            public ObservableCollection<CardDTO> Cards { get; set; } = new();
           
        }
        /// <summary>
        /// загрузка списка карт пользователя в lbCards
        /// </summary>
        public async void ReturnCards() 
        {
           
            var result = await repository.ReturnCards();


            if (result.IsSuccess)
            {
                listItems.Cards.Clear();

                foreach (CardDTO cardDTO in result.Value) 
                {
                    listItems.Cards.Add(cardDTO);
                }
               
            }
            
        }

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

        /// <summary>
        /// запускает окно оплаты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            Pay pay = new();
            ShowWindow(pay);
        }
        /// <summary>
        /// запускает окно перевода по номеру карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remittance_Click(object sender, RoutedEventArgs e)
        {
            Remittance remittance = new();
            ShowWindow(remittance);
        }
    }
}
