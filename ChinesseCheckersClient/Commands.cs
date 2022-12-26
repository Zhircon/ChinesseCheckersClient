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
    }
}
