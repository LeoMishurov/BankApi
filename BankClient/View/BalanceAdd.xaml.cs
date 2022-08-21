﻿using System;
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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class BalanceAdd : UserControl
    {
        public BalanceAdd()
        {
            InitializeComponent();
        }
        Repository repository = new();

        
        /// <summary>
        /// пополнение баланса каарты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnBalanseAdd_Click(object sender, RoutedEventArgs e)
        {

            var result = await repository.BalanceAdd(tbSum.Text, tbCardNumber.Text);
            if (result.IsSuccess)
            {
                
                lbInformation.Content = $"баланс карты {tbCardNumber.Text} попполнен на {tbSum.Text}";
                WindowManeger.ReturnCards();
                tbCardNumber.Clear();
                tbSum.Clear();
            }
            else
            {
                lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";               
            }
          
        }
    }
}
