using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChinesseCheckersClient
{
    ///<sumary>
    ///This class provide command to the main window
    ///<sumary>
    public static class Commands
    {
        public static void ChangeWindowState()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }
        public static void MinimizateWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        public static void LeaveRoom()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.Room != null)
            {
                mainWindow.RoomMgt.LeaveRoom(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.IdPlayer,mainWindow.IsInGameplay);
            }
        }
        public static void CloseWindow()
        {
            LeaveRoom();
            Application.Current.MainWindow.Close();
        }
        public static void PlayMusicMenu()
        {
            MainWindow mainWindow =(MainWindow) Application.Current.MainWindow;
            mainWindow.MediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Assets/Music/UnoOst.mp3"));
            mainWindow.MediaPlayer.Volume = mainWindow.Session.PlayerConfiguration.volMusic;
            mainWindow.MediaPlayer.Play();
        }
        public static void RestartAplication()
        {
            MainWindow mainWindow;
            MainWindow mainWindowNew = new MainWindow();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindowNew.Session = mainWindow.Session;
            mainWindowNew.WindowState = mainWindow.WindowState;
            mainWindowNew.WindowStyle = mainWindow.WindowStyle;
            mainWindowNew.MediaPlayer.Volume = mainWindow.MediaPlayer.Volume;
            mainWindowNew.btBack.Visibility = mainWindow.btBack.Visibility;
            mainWindow.MediaPlayer.Stop();
            mainWindowNew.frame.Source = new Uri("/MainMenuPage.xaml", UriKind.Relative);
            mainWindowNew.Show();
            mainWindow.Close();
            Application.Current.MainWindow = mainWindowNew;

        }
    }
}
