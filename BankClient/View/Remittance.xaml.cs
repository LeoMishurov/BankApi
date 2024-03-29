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
    public partial class Remittance : UserControl
    {
        public Remittance()
        {
            InitializeComponent();
        }
        Repository repository = new();

        
        /// <summary>
        /// пополнение баланса каарты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Remittance_Click(object sender, RoutedEventArgs e)
        {
            //проверка на введенное количество символов и на то чтобы введенное было число
            if (!repository.ErrorChecking(tbFromCardNumber.Text) || !repository.ErrorChecking(tbInCardNumber.Text)  
                || !repository.SumChecking(tbSum.Text))
            {
                lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
                return;
            }
            var result = await repository.Remittance(tbSum.Text,repository.AddSpace(tbFromCardNumber.Text), 
                repository.AddSpace(tbInCardNumber.Text));

                if (result.IsSuccess)
                {

                    lbInformation.Content = $"перевод на сумму  {tbSum.Text} прошел успешно";
                    WindowManager.ReturnCards();
                    tbFromCardNumber.Clear();
                    tbInCardNumber.Clear();
                    tbSum.Clear();
                }
                else
                {

                    //lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
                    lbInformation.Content = result.Error;
                }
        }

       
    }
}
