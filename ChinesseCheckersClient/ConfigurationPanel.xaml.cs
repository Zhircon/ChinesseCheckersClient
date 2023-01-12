using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
    /// This class enable a way to change configuration in real time
    /// </summary>
    public partial class ConfigurationPanel : UserControl
    {
        public ConfigurationPanel()
        {
            InitializeComponent();
            LoadLanguage();
        }

        private void volMusic_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateVolMusicValue();
        }

        private void UpdateVolMusicValue()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MediaPlayer.Volume = volMusic.Value * volMusic.Value;
            mainWindow.Session.PlayerConfiguration.volMusic = volMusic.Value;
            lbVolMusic.Content = Properties.Resources.Common_VolMusic + ": " + (int)(mainWindow.MediaPlayer.Volume * 100);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonInput = (Button)sender;
            if (buttonInput.Name == "btChangeLanguage") { ChangeLanguage(); }
            if (buttonInput.Name == "btAccept") { await SaveConfiguration(); }
        }

        private async Task SaveConfiguration()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            var playerMgtClient =new  GameService.PlayerMgtClient();
            try
            {
                btAccept.IsEnabled = false;
                btAccept.Content = "Saving";
                GameService.OperationResult operationResult= await playerMgtClient.UpdateConfigurationAsync(mainWindow.Session.PlayerConfiguration);
                if (operationResult == GameService.OperationResult.Sucessfull)
                {
                    btAccept.IsEnabled = true;
                    btAccept.Content = "Save";
                }
                else
                {
                    btAccept.IsEnabled = true;
                    btAccept.Content = "Fail";
                }
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }
            

        }
        private void LoadLanguage()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.Session.PlayerConfiguration.language == "es")
            {
                btChangeLanguage.Style = (Style)FindResource("ButtonChangeLanguageMxStyle");
            }
            else
            {
                btChangeLanguage.Style = (Style)FindResource("ButtonChangeLanguageUsaStyle");
            }
        }
        private void ChangeLanguage()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            if (mainWindow.Session.PlayerConfiguration.language == "es") {
                mainWindow.Session.PlayerConfiguration.language = "en";
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-Mx");
                btChangeLanguage.Style = (Style)FindResource("ButtonChangeLanguageUsaStyle");
                
                Commands.RestartAplication();
            }
            else{
                mainWindow.Session.PlayerConfiguration.language = "es";
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                btChangeLanguage.Style = (Style)FindResource("ButtonChangeLanguageMxStyle");
                Commands.RestartAplication();
            }
        }


    }
}
