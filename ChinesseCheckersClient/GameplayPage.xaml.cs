using ChinesseCheckersClient.GameService;
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
    /// This class provide gameplay and chat funtionality
    /// </summary>
    public partial class GameplayPage : Page  , GameService.IChatMgtCallback
    {
        private readonly MainWindow mainWindow;
        private readonly GameService.ChatMgtClient chatMgt;
        public GameplayPage()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            
           
            chatMgt = new GameService.ChatMgtClient(new InstanceContext(this));
            JoinAndUpdateRoom();
            FillListBoxFriendList();
        }
        private async void FillListBoxFriendList()
        {
            var roomMgt = new GameService.RoomMgtClient(new InstanceContext(mainWindow));
            mainWindow.Room = await roomMgt.SearchRoomAsync(mainWindow.Room.IdRoom);
            foreach (GameService.Player player in mainWindow.Room.Players.Values)
            {
                var listBoxItem = new ListBoxItem();
                var button = new Button();
                button.Content = player.Nickname;
                button.Click += Button_Click;
                listBoxItem.Content = button;
                listBoxFriendButtons.Items.Add(listBoxItem);
            }
        }
        private async void JoinAndUpdateRoom()
        {
            await chatMgt.JoinToChatAsync(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.IdPlayer);
        }
        private async Task SendChatMessage()
        {
            string message = tbMessage.Text;
            await chatMgt.SendMessageAsync(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.Nickname, message);
            tbMessage.Text = "";
        }
        void IChatMgtCallback.ReceiveMessage(string _nickname, string _message)
        {
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = _nickname + ": " + _message;
            listBoxMessage.Items.Add(listBoxItem);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name == "btSendMessage") {await  SendChatMessage(); }
        }
    }
}
