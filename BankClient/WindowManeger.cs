using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BankClient
{
    public static class WindowManeger
    {

      public static  MainWindow mainWindow;

        /// <summary>
        /// закрывает окно
        /// </summary>
        public static void ClouseWindow()
            
        {         
            mainWindow.ClouseWindow();
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
        /// загрузка списка карт пользователя в lbCards
        /// </summary>
        public static void ReturnCards() 
        {
            mainWindow.ReturnCards();
        }

        /// <summary>
        /// разблокировка кнопок после авторизации
        /// </summary>
        public static void UnlockButtons() 
        {
            mainWindow.UnlockButtons();
        }
    }
}
