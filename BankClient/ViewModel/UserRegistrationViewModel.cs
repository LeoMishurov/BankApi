using BankClient.Model;
using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    internal class UserRegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();
        public string TbLogin { get; set; }
        public string TbPassword { get; set; }

        /// <summary>
        /// регистрация
        /// </summary>
        public ICommand btnRegistr_Click => new SimpleCommand(async() =>
        {
            if (TbLogin != null && TbPassword != null)
            {
                await repository.Registration(TbLogin, TbPassword);
                await repository.Authorization(TbLogin, TbPassword);

                //записываем login в класс с глобальными переменными
                GlobalVar.Login = TbLogin;

                Main main = new();
                WindowManager.ShowWindowMain(main);
            }
        });

        /// <summary>
        /// Назад к авторизации
        /// </summary>
        public ICommand back_Click => new SimpleCommand(() =>
        {
            UserАuthorization userАuthorization = new();
            WindowManager.ShowWindowMain(userАuthorization);
        });
    }
}
