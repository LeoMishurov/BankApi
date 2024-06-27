using BankClient.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();
        public string Login { get; set; } = GlobalVar.Login;

        /// <summary>
        /// запускает окно "выпуск карты"
        /// </summary>
        public ICommand btnCardAdd_Click => new SimpleCommand(() =>
        {
            CardAdd cardAdd = new();
            WindowManager.ShowWindow(cardAdd);          
        });

        /// <summary>
        /// запускает окно "главная"
        /// </summary>
        public ICommand main_Click => new SimpleCommand(() =>
        {
            WindowManager.CloseWindow();
        });

        /// <summary>
        /// запускает окно блокировки карты
        /// </summary>
        public ICommand btnBlock_Click => new SimpleCommand(() =>
        {
            Block block = new();
            WindowManager.ShowWindow(block);
        });

        /// <summary>
        /// запускает окно установки дневного лмимта
        /// </summary>
        public ICommand DailyLimit_Click => new SimpleCommand(() =>
        {
            DailyLimit dailyLimit = new();
            WindowManager.ShowWindow(dailyLimit);
        });

        /// <summary>
        /// запускает окно оплаты
        /// </summary>
        public ICommand Pay_Click => new SimpleCommand(() =>
        {
            Pay pay = new();
            WindowManager.ShowWindow(pay);
        });

        /// <summary>
        /// запускает окно перевода по номеру карты
        /// </summary>
        public ICommand Remittance_Click => new SimpleCommand(() =>
        {
            Remittance remittance = new();
            WindowManager.ShowWindow(remittance);
        });

        /// <summary>
        /// запускает окно выход
        /// </summary>
        public ICommand back_Click => new SimpleCommand(() =>
        {
            WindowManager.CloseWindow();
            UserАuthorization userАuthorization = new();
            WindowManager.ShowWindowMain(userАuthorization);
        });

    }
}
