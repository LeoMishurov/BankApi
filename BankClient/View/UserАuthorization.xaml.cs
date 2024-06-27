using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserАuthorization : UserControl
    {
        public UserАuthorization()
        {
            InitializeComponent();
            DataContext = new UserАuthorizationViewModel();
        }
       
    }
}
