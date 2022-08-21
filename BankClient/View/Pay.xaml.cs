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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class Pay : UserControl
    {
        public Pay()
        {
            InitializeComponent();
        }
        Repository repository = new();

        
        /// <summary>
        /// пополнение баланса каарты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Pay_Click(object sender, RoutedEventArgs e)
        {
            //провера на введенное количество символов и на то чтобы введенное было число
            int res;
            bool isInt = int.TryParse(tbCardNumber.Text, out res);
            //bool isInt = Int32.TryParse(tbCardNumber.Text, out res);
            if (tbCardNumber.Text.Length != 19 || !isInt)
            {
                lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
                return;
            }          
                var result = await repository.Pay(tbSum.Text, tbCardNumber.Text);

                if (result.IsSuccess)
                {

                    lbInformation.Content = $"оплата на сумму  {tbSum.Text} прошла успешно";
                    WindowManeger.ReturnCards();
                    tbCardNumber.Clear();
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
