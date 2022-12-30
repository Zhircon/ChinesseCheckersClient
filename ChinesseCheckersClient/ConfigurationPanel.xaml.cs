using System;
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
    /// Lógica de interacción para ConfigurationPanel.xaml
    /// </summary>
    public partial class ConfigurationPanel : UserControl
    {
        public ConfigurationPanel()
        {
            InitializeComponent();
        }

        private void volMusic_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MediaPlayer.Volume = volMusic.Value*volMusic.Value;
            lbVolMusic.Content = Properties.Resources.Common_VolMusic + ": " + (int)(mainWindow.MediaPlayer.Volume*100);
        }
    }
}
