using BankClient.Model;
using BankClient.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace BankClient
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserRegistration : UserControl
    {
        public UserRegistration()
        {
            InitializeComponent();
            DataContext = new UserRegistrationViewModel();
        }     
        
    }
}
