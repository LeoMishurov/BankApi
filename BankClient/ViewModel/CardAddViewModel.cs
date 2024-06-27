using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankClient.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BankClient.View;
using System.Windows.Controls;

namespace BankClient.ViewModel
{
    public class CardAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
  
        Repository repository = new();
        public string LbCardNumber { get; set; }
        public string LbInformation { get; set; }

        public CardAddViewModel() // После теста убрать!
        {
            LbCardNumber = "8888-8888-8888-8888";
            LbInformation = "карта успешно выпущена";
        }

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
