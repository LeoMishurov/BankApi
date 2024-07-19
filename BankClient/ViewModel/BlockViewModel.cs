using BankClient.View;
using System.ComponentModel;
using System.Windows.Input;

namespace BankClient.ViewModel
{
    public class BlockViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Repository repository = new();

        public string tbCardNumber { get; set; }
        public string lbInformation { get; set; }

        /// <summary>
        /// блокировка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public ICommand btnBlock_Click => new SimpleCommand(async () =>
            {
                //проверка на введенное количество символов и на то чтобы введенное было число
                if (!repository.ErrorChecking(tbCardNumber))
                {
                    lbInformation = "Произошла ошибка. Проверьте введенные данные";
                    return;
                }

                var result = await repository.Block(repository.AddSpace(tbCardNumber));

                if (result.IsSuccess)
                {
                    lbInformation = $"карта {tbCardNumber} заблокирована";

                    // Обновление окна с картами и пользователем
                    Main main = new();
                    WindowManager.ShowWindowMain(main);
                    tbCardNumber = "";
                }
                else
                {
                    lbInformation = "Произошла ошибка. Проверьте номер карты";
                }
            });

        /// <summary>
        /// разблокировка карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public ICommand btnUnBlock_Click => new SimpleCommand(async () =>
            {
                //проверка на введенное количество символов и на то чтобы введенное было число
                if (!repository.ErrorChecking(tbCardNumber))
                {
                    lbInformation = "Произошла ошибка. Проверьте введенные данные";
                    return;
                }

                var result = await repository.UnBlock(repository.AddSpace(tbCardNumber));

                if (result.IsSuccess)
                {
                    lbInformation = $"карта {tbCardNumber} разблоктрована";

                    // Обновление окна с картами и пользователем
                    Main main = new();
                    WindowManager.ShowWindowMain(main);
                    tbCardNumber = "";
                }
                else
                {
                    lbInformation = "Произошла ошибка. Проверьте введенные данные";
                }
            });
    }
}
