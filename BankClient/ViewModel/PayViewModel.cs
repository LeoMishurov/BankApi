using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class PayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();

        public string TbCardNumber {  get; set; }
        public string TbSum { get; set; }
        public string LbInformation { get; set; }

        /// <summary>
        /// оплата картой
        /// </summary>     
        public ICommand Pay_Click => new SimpleCommand(async () =>
            {
                //проверка на введенное количество символов и на то чтобы введенное было число
                if (!repository.ErrorChecking(TbCardNumber) || !repository.SumChecking(TbSum))
                {
                    LbInformation = "Произошла ошибка. Проверьте введенные данные";
                    return;
                }
                var result = await repository.Pay(TbSum, repository.AddSpace(TbCardNumber));

                if (result.IsSuccess)
                {

                    LbInformation = $"оплата на сумму  {TbSum} прошла успешно";

                    // Обновление окна с картами и пользователем
                    Main main = new();
                    WindowManager.ShowWindowMain(main);
                    TbCardNumber = "";
                    TbSum = "";
                }
                else
                {
                    LbInformation = result.Error;
                }
            });
    }
}
