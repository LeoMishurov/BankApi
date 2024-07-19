using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class DailyLimitVievModel : INotifyPropertyChanged
    {
        Repository repository = new();

        public event PropertyChangedEventHandler PropertyChanged;

        public string TbCardNumber { get; set; }
        public string TbSum { get; set; }
        public string LbInformation { get; set; }

        public ICommand btnDailyLimit_Click => new SimpleCommand(async () =>
            {
                //проверка на введенное количество символов и на то чтобы введенное было число
                if (!repository.ErrorChecking(TbCardNumber) || !repository.SumChecking(TbSum))
                {
                    LbInformation = "Произошла ошибка. Проверьте введенные данные";
                    return;
                }

                var result = await repository.DailyLimit(TbSum, repository.AddSpace(TbCardNumber));
                if (result.IsSuccess)
                {
                    LbInformation = $"лимит карты {TbCardNumber} установлен на {TbSum}";

                    // Обновление окна с картами и пользователем
                    Main main = new();
                    WindowManager.ShowWindowMain(main);
                    TbCardNumber = "";
                    TbSum = "";
                }
                else
                {
                    LbInformation = "Произошла ошибка. Проверьте введенные данные"; ;
                }
            });
    }
}
