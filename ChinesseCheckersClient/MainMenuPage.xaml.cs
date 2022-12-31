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
    /// Lógica de interacción para MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
            Commands.PlayMusicMenu();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            configurationPanel.volMusic.Value = mainWindow.Session.PlayerConfiguration.volMusic / 100;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button)sender;
            if (buttonClicked.Name == "btProfile") { UpdateVisibilityProfilePanel(); }
            if (buttonClicked.Name == "btConfiguration") { UpdateVisibilityConfigurationPanel(); }
        }

        private void UpdateVisibilityConfigurationPanel()
        {
            if (configurationPanel.Visibility == Visibility.Hidden)
            {
                configurationPanel.Visibility = Visibility.Visible;
            }
            else
            {
                configurationPanel.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateVisibilityProfilePanel()
        {
            if(profilePanel.Visibility == Visibility.Hidden)
            {
                profilePanel.Visibility = Visibility.Visible;
            }
            else
            {
                profilePanel.Visibility = Visibility.Hidden;
            }
        }
    }
}
