using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient.View
{
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
