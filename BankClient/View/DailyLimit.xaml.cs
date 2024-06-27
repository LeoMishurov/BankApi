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
        //public DailyLimit()
        //{
        //    InitializeComponent();
        //}
        //Repository repository = new();



        //private async void btnDailyLimit_Click(object sender, RoutedEventArgs e)
        //{
        //    //проверка на введенное количество символов и на то чтобы введенное было число
        //    if (!repository.ErrorChecking(tbCardNumber.Text) || !repository.SumChecking(tbSum.Text))
        //    {
        //        lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
        //        return;
        //    }

        //    var result = await repository.DailyLimit(tbSum.Text, repository.AddSpace(tbCardNumber.Text));
        //    if (result.IsSuccess)
        //    {               
        //        lbInformation.Content = $"лимит карты {tbCardNumber.Text} установлен на {tbSum.Text}";
        //        WindowManager.ReturnCards();
        //    }
        //    else
        //    {
        //        lbInformation.Content = "Произошла ошибка. Проверьте введенные данные"; ;
        //    }
        //}


    }
}
