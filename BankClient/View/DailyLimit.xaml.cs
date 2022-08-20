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
    public partial class DailyLimit : UserControl
    {
        public DailyLimit()
        {
            InitializeComponent();
        }
        Repository repository = new();

        

        private async void btnDailyLimit_Click(object sender, RoutedEventArgs e)
        {

            var result = await repository.DailyLimit(tbSum.Text, tbCardNumber.Text);
            if (result.IsSuccess)
            {               
                lbInformation.Content = $"лимит карты {tbCardNumber.Text} установлен на {tbSum.Text}";
            }
            else
            {
                lbInformation.Content = "Произошла ошибка. Проверьте номер карты"; ;
            }
        }

        
    }
}
