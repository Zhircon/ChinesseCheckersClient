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
    /// Provides funtionality to reach other pages and funtions
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
            Commands.PlayMusicMenu();
            LoadVolMusicValue();
            LoadProfile();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button)sender;
            if (buttonClicked.Name == "btProfile") { UpdateVisibilityProfilePanel(); }
            if (buttonClicked.Name == "btTwoPlayers") { await CreateTwoPlayersMatch(); }
            if (buttonClicked.Name == "btThreePlayers") { await CreateThreePlayersMatch(); }
            if (buttonClicked.Name == "btJoinMatch") { UpdateVisibilitySearchPanel(); }
            if (buttonClicked.Name == "btCreateMatch") { UpdateVisibilityPlayersButtons(); }
            if (buttonClicked.Name == "btConfiguration") { UpdateVisibilityConfigurationPanel(); }
        }

        private async Task CreateTwoPlayersMatch()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.PlayerToWait = 2;
            await JoinLocalToRoom();
        }
        private async Task CreateThreePlayersMatch()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.PlayerToWait = 3;
            await JoinLocalToRoom();
        }
        private async Task JoinLocalToRoom()
        {
            GameService.Room room;
            try
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                string idCode = await mainWindow.RoomMgt.CreateRoomAsync();
                room = await  mainWindow.RoomMgt.SearchRoomAsync(idCode);
                if (room != null)
                {
                    mainWindow.Room = room;
                    mainWindow.PlayerConectedInRoom = await mainWindow.RoomMgt.JoinToRoomAsync(room.IdRoom, mainWindow.Session.PlayerLoged);
                    NavigationCommands.GoToLobby();
                }
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }
        }

        private void UpdateVisibilitySearchPanel()
        {
            if (searchRoomPanel.Visibility == Visibility.Hidden)
            {
                searchRoomPanel.Visibility = Visibility.Visible;
            }
            else
            {
                searchRoomPanel.Visibility = Visibility.Hidden;
            }
        }

        private void UpdateVisibilityPlayersButtons()
        {
            if (btTwoPlayers.Visibility == Visibility.Hidden)
            {
                btTwoPlayers.Visibility = Visibility.Visible;
                btThreePlayers.Visibility = Visibility.Visible;
            }
            else
            {
                btTwoPlayers.Visibility = Visibility.Hidden;
                btThreePlayers.Visibility = Visibility.Hidden;
            }
        }

        private void LoadProfile()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            profilePanel.tbEmail.Text = mainWindow.Session.PlayerLoged.Email;
            profilePanel.tbNickname.Text = mainWindow.Session.PlayerLoged.Nickname;
            profilePanel.pbPassword.Password = mainWindow.Session.PlayerLoged.Password;
            profilePanel.pbValidatePassword.Password = mainWindow.Session.PlayerLoged.Password;
        }
        private void LoadVolMusicValue()
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            configurationPanel.volMusic.Value = mainWindow.Session.PlayerConfiguration.volMusic;
            mainWindow.MediaPlayer.Volume = configurationPanel.volMusic.Value* configurationPanel.volMusic.Value;
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
                policiesPanel.Visibility = Visibility.Visible;
            }
            else
            {
                profilePanel.Visibility = Visibility.Hidden;
                policiesPanel.Visibility = Visibility.Hidden;
            }
        }
    }
}
