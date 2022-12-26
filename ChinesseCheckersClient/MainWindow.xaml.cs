﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChinesseCheckersClient
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var loginPage = new LoginPage();
            frame.Source = new Uri("/LoginPage.xaml", UriKind.Relative);
            this.DragMove();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            Commands.CloseWindow();
        }

        private void btChangeState_Click(object sender, RoutedEventArgs e)
        {
            Commands.ChangeWindowState();
        }

        private void btMinimize_Click(object sender, RoutedEventArgs e)
        {
            Commands.MinimizateWindow();
        }


    }
}
