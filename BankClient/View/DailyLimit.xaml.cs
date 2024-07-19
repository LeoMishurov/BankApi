using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    public partial class DailyLimit : UserControl
    {
       public DailyLimit() 
        {
            InitializeComponent();
            DataContext = new DailyLimitVievModel();
        }
    }
}
