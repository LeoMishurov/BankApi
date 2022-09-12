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
    public partial class CardAdd : UserControl
    {
        public CardAdd()
        {
            InitializeComponent();
        }
        Repository repository = new();

       /// <summary>
       /// выпуск карты
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private async void btnCardAdd_Click(object sender, RoutedEventArgs e)
        {
            var result = await repository.CardAdd();
            if (result.IsSuccess)
            {
                lbCardNumber.Content = result.Value.CardNumber;
                lbInformation.Content = "карта успешно выпущена";
                WindowManager.ReturnCards();
            }
            else
            {
                lbCardNumber.Content = "произошла ошибка";
                lbInformation.Content = result.Error;
            }
        }
    }
}
