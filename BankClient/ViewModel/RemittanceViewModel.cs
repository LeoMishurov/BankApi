using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class RemittanceViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();

        public string TbFromCardNumber { get; set; }
        public string TbInCardNumber { get; set; }
        public string TbSum {  get; set; }
        public string LbInformation { get; set; }

        /// <summary>
        /// пополнение баланса каарты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public ICommand Remittance_Click => new SimpleCommand(async () =>
            {
                //проверка на введенное количество символов и на то чтобы введенное было число
                if (!repository.ErrorChecking(TbFromCardNumber) || !repository.ErrorChecking(TbInCardNumber)
                    || !repository.SumChecking(TbSum))
                {
                    LbInformation = "Произошла ошибка. Проверьте введенные данные";
                    return;
                }
                var result = await repository.Remittance(TbSum, repository.AddSpace(TbFromCardNumber),
                    repository.AddSpace(TbInCardNumber));

                if (result.IsSuccess)
                {
                    LbInformation = $"перевод на сумму  {TbSum} прошел успешно";

                    // Обновление окна с картами и пользователем
                    Main main = new();
                    WindowManager.ShowWindowMain(main);

                    TbFromCardNumber = "";
                    TbInCardNumber = "";
                    TbSum = "";
                }
                else
                {
                    //lbInformation.Content = "Произошла ошибка. Проверьте введенные данные";
                    LbInformation = result.Error;
                }
            });
    }
}
