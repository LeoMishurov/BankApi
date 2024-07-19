using BankClient.Model;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace BankClient
{
    public static class WindowManager
    {

       public static  MainWindow mainWindow;

        /// <summary>
        /// закрывает окно
        /// </summary>
        public static void CloseWindow()

        {
            mainWindow.CloseWindow();
        }

        //Возвращает список карт пользователя,
        //вызывается автоматически при загрузке Xaml Main.
        //биндится к listBox 
        public static ObservableCollection<CardDTO> ReturnCards()
        {
            Repository repository = new();

            var result = repository.ReturnCards().Result.Value;

            ObservableCollection<CardDTO> LbCards = new();

            LbCards.Clear();

            foreach (CardDTO cardDTO in result)
            {
                LbCards.Add(cardDTO);
            }

            return LbCards;
        }

        /// <summary>
        /// закрывает окно и открывает преданное 
        /// </summary>
        /// <param name="control"></param>
        public static void ShowWindow(UserControl control)

        {
            mainWindow.ShowWindow(control);
        }

        /// <summary>
        /// закрывает главное окно и открывает преданное 
        /// </summary>
        /// <param name="control"></param>
        public static void ShowWindowMain(UserControl control)

        {
            mainWindow.ShowWindowMain(control);
        }
    }
}
