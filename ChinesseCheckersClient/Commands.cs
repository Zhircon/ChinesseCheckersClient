using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChinesseCheckersClient
{
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
        public static void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
        public static void PlayMusicMenu()
        {
            MainWindow mainWindow =(MainWindow) Application.Current.MainWindow;
            mainWindow.MediaPlayer.Open(new Uri("pack://siteoforigin:,,,/Assets/Music/UnoOst.mp3"));
            mainWindow.MediaPlayer.Volume = mainWindow.Session.PlayerConfiguration.volMusic/100;
            Console.WriteLine(mainWindow.MediaPlayer.Volume);
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
            mainWindowNew.LastPage = mainWindow.LastPage;
            mainWindow.MediaPlayer.Stop();
            mainWindowNew.frame.Source = new Uri("/MainMenuPage.xaml", UriKind.Relative);
            mainWindowNew.Show();
            mainWindow.Close();
            Application.Current.MainWindow = mainWindowNew;
            /*
            System.Windows.Application.Current.Shutdown();
            System.Windows.Forms.Application.Restart();
            */
        }
    }
}
