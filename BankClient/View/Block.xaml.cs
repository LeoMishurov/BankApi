using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class Block : UserControl
    {
        public Block()
        {
            InitializeComponent();
        }
        Repository repository = new();

        /// <summary>
        /// блокировка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnBlock_Click(object sender, RoutedEventArgs e)
        {
            //проверка на введенное количество символов и на то чтобы введенное было число
            if (!repository.ErrorChecking(tbCardNumber.Text))
            {
                lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
                return;
            }
 
                var result = await repository.Block(repository.AddSpace(tbCardNumber.Text));

                if (result.IsSuccess)
                {
                    lbInformation.Content = $"карта {tbCardNumber.Text} заблокирована";
                    WindowManeger.ReturnCards();
                }

                else
                {
                    lbInformation.Content = "Произошла ошибка. Проверьте номер карты";
                }
        }

        /// <summary>
        /// разблокировка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnUnBlock_Click(object sender, RoutedEventArgs e)
        {
            //проверка на введенное количество символов и на то чтобы введенное было число
            if (!repository.ErrorChecking(tbCardNumber.Text))
            {
                lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
                return;
            }
         
                var result = await repository.UnBlock(repository.AddSpace(tbCardNumber.Text));

            if (result.IsSuccess)
            {
                lbInformation.Content = $"карта {tbCardNumber.Text} разблоктрована";
                WindowManeger.ReturnCards();
            }
            else
                lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";           
        }

    }
}
