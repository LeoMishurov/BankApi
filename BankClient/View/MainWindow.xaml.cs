using System.Windows;
using System.Windows.Controls;


namespace BankClient
{
    public partial class MainWindow : Window
    {
        UserАuthorization userАuthorization = new();
        public MainWindow() 
        {
            InitializeComponent();

            WindowManager.mainWindow = this;

            // Запускаем окно с авторизацией
            GridMain.Children.Add(userАuthorization);
        }

        /// <summary>
        /// закрывает окно
        /// </summary>
        public void CloseWindow()
        {
            Grid1.Children.Clear();
        }

        /// <summary>
        /// закрывает главное окно
        /// </summary>
        public void CloseWindowMain()
        {
            GridMain.Children.Clear();
        }

        /// <summary>
        /// очищает окно и открывает следующее
        /// </summary>
        /// <param name="control"></param>
        public void ShowWindow(UserControl control)
        {
            CloseWindow();
            Grid1.Children.Add(control);
        }

        /// <summary>
        /// очищает окно главное и открывает следующее
        /// </summary>
        /// <param name="control"></param>
        public void ShowWindowMain(UserControl control)
        {
            CloseWindowMain();
            GridMain.Children.Add(control);
        }
    }
}
