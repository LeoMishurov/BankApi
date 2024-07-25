using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class BalanceAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();

        public string TbCardNumber { get; set; }
        public string TbSum { get; set; }
        public string LbInformation { get; set; }

        /// <summary>
        /// пополнение баланса каарты
        /// </summary>
        public ICommand btnBalanseAdd_Click => new SimpleCommand(async () =>
        {
            //проверка на введенное количество символов и на то чтобы введенное было число
            if (!repository.ErrorChecking(TbCardNumber) || !repository.SumChecking(TbSum))
            {
                LbInformation = "Произошла ошибка. Проверьте введенные данные";
                return;
            }

            var result = await repository.BalanceAdd(TbSum, repository.AddSpace(TbCardNumber));

            if (result.IsSuccess)
            {
                LbInformation = $"баланс карты {TbCardNumber} пополнен на {TbSum}";

                // Обновление окна с картами и пользователем
                Main main = new();
                WindowManager.ShowWindowMain(main);

                TbCardNumber = "";
                TbSum = "";
            }
            else
            {
                LbInformation = "Произошла ошибка. Проверьте введенные данные";
            }
        });
    }
}
