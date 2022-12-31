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
        private bool isMusicVolEditable;
        public ConfigurationPanel()
        {
            InitializeComponent();
            LoadLanguage();
            //LoadVolMusicValue();
        }

        private void volMusic_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.WriteLine(isMusicVolEditable);
            if (isMusicVolEditable) { UpdateVolMusicValue(); }
            isMusicVolEditable = true;
        }
        private void LoadVolMusicValue()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MediaPlayer.Volume = mainWindow.Session.PlayerConfiguration.volMusic;
            volMusic.Value = mainWindow.Session.PlayerConfiguration.volMusic ;
        }
        private void UpdateVolMusicValue()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MediaPlayer.Volume = volMusic.Value * volMusic.Value;
            mainWindow.Session.PlayerConfiguration.volMusic = mainWindow.MediaPlayer.Volume;
            Console.WriteLine("config:"+mainWindow.Session.PlayerConfiguration.volMusic);
            Console.WriteLine("Media:" + mainWindow.MediaPlayer.Volume);
            Console.WriteLine("slice:" + volMusic.Value);
            lbVolMusic.Content = Properties.Resources.Common_VolMusic + ": " + (int)(mainWindow.MediaPlayer.Volume * 100);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonInput = (Button)sender;
            if (buttonInput.Name == "btChangeLanguage") { ChangeLanguage(); }
            if (buttonInput.Name == "btAccept") { SaveConfiguration(); }
        }

        private void SaveConfiguration()
        {
            throw new NotImplementedException();
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
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                btChangeLanguage.Style = (Style)FindResource("ButtonChangeLanguageUsaStyle");
                
                Commands.RestartAplication();
            }
            else{
                mainWindow.Session.PlayerConfiguration.language = "es";
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
                btChangeLanguage.Style = (Style)FindResource("ButtonChangeLanguageMxStyle");
                Commands.RestartAplication();
            }
        }


    }
}
