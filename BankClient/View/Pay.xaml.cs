using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    public partial class Pay : UserControl
    {
        public Pay() 
        {
            InitializeComponent();
            DataContext = new PayViewModel();
        }
    }
}
