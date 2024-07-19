using BankClient.ViewModel;
using System.Windows.Controls;

namespace BankClient
{
    public partial class Block : UserControl
    {
        public Block() 
        {
            InitializeComponent();
            DataContext = new BlockViewModel();
        }

    }
}
