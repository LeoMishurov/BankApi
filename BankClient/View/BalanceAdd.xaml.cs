using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    public partial class BalanceAdd : UserControl
    {
        public BalanceAdd()
        {
            InitializeComponent();
            DataContext = new BalanceAddViewModel();
        }
    }
}
