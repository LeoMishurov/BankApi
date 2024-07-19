using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class CardAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
  
        Repository repository = new();
        public string LbCardNumber { get; set; }
        public string LbInformation { get; set; }

        /// <summary>
        /// выпуск карты
        /// </summary>
        public ICommand btnCardAdd_Click => new SimpleCommand(async () =>
        {
            var result = await repository.CardAdd();
            if (result.IsSuccess)
            {
                LbCardNumber = result.Value.CardNumber;
                LbInformation = "карта успешно выпущена";

                // Обновление окна с картами и пользователем
                Main main = new();
                WindowManager.ShowWindowMain(main);
            }
            else
            {
                LbCardNumber = "произошла ошибка";
                LbInformation = result.Error;
            }
        });
    }
}
