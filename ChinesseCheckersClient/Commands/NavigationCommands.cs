using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChinesseCheckersClient
{
    public static class NavigationCommands
    {
        public static void GoToLogin()
        {
            MainWindow mainWindow = (MainWindow) Application.Current.MainWindow;
            mainWindow.frame.Source= new Uri("/LoginPage.xaml", UriKind.Relative); 
        }
        public static void GoToLastPage()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Commands.LeaveRoom();
            mainWindow.frame.GoBack();
        }
        public static void GoToConnectionLostPage()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Source = new Uri("/ConnectionLostPage.xaml", UriKind.Relative);
        }
        public static void GoToMainMenu()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.btBack.Visibility = Visibility.Visible;
            mainWindow.frame.Source = new Uri("/MainMenuPage.xaml", UriKind.Relative);
        }
        public static void GoToLobby()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            mainWindow.frame.Source = new Uri("/LobbyPage.xaml", UriKind.Relative);
        }
        public static void GoToGameplay()
        {

            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.frame.Source = new Uri("/GameplayPage.xaml", UriKind.Relative);
        }
    }
}
