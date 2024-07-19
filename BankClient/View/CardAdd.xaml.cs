using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    public partial class CardAdd : UserControl
    {
        public CardAdd()
        {
            InitializeComponent();
            DataContext = new CardAddViewModel();
        }

    }
}
