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
    /// Enable to join a room
    /// </summary>
    public partial class SearchRoomPanel : UserControl 
    {
        private const int PLAYER_NOT_ALLOWED=-1;
        public SearchRoomPanel()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string idRoom = tbIdRoom.Text;
            await JoinLocalToRoom(idRoom);
        }
        private async Task JoinLocalToRoom(string _idRoom)
        {
            GameService.Room room;
            try
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_SearchingRoom;
                room = await mainWindow.RoomMgt.SearchRoomAsync(_idRoom);
                if(room != null)
                {
                    bool isPlayerInRoom=await  mainWindow.RoomMgt.IsPlayerInRoomAsync(_idRoom, mainWindow.Session.PlayerLoged.IdPlayer);
                    if (!isPlayerInRoom)
                    {
                        lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_TryToJoin;
                        mainWindow.Room = room;
                        mainWindow.PlayerConectedInRoom = await mainWindow.RoomMgt.JoinToRoomAsync(room.IdRoom, mainWindow.Session.PlayerLoged);
                        if (mainWindow.PlayerConectedInRoom != PLAYER_NOT_ALLOWED)
                        {
                            lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_JoinSucessfull;
                            NavigationCommands.GoToLobby();
                        }
                        else
                        {
                            lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_RoomFull;
                        }
                    }
                    else
                    {
                        lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_PlayerInside;
                    }

                }
                else
                {
                    lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_NotFoundRoom;
                }
                
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }
        }


    }
}
