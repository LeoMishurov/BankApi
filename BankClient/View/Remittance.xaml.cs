using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    public partial class Remittance : UserControl
    {
        public Remittance() 
        {
            InitializeComponent();
            DataContext = new RemittanceViewModel();
        }
    }
}
