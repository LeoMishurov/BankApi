using BankClient.Model;
using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class UserАuthorizationViewModel : INotifyPropertyChanged
    {    
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();

        public string TbLogin { get; set; }
        public string TbPassword { get; set; }
        public string LbError { get; set; }

        // вызов окна регистрации
        public ICommand btnRegistr_Click => new SimpleCommand(() =>
        {
            UserRegistration userRegistration = new();
            WindowManager.ShowWindowMain(userRegistration);
        });
   
        //авторизация
        public ICommand btnAuthorization_Click => new SimpleCommand(async () =>
        {

            var isAuthorization = await repository.Authorization(TbLogin, TbPassword);

            //записываем login в класс с глобальными переменными

            GlobalVar.Login = TbLogin;


            if (isAuthorization)
            {
                Main main = new();
                WindowManager.ShowWindowMain(main);
            }
            else
                LbError = "не верный логин или пароль";
        });
    }
}
