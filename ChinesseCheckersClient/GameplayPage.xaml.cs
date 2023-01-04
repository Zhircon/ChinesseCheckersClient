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
    /// Lógica de interacción para GameplayPage.xaml
    /// </summary>
    public partial class GameplayPage : Page  , GameService.IChatMgtCallback
    {
        private MainWindow mainWindow;
        private GameService.ChatMgtClient chatMgt;
        public GameplayPage()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            chatMgt = new GameService.ChatMgtClient(new InstanceContext(this));
            JoinAndUpdateRoom();
        }
        private async void JoinAndUpdateRoom()
        {
            await chatMgt.JoinToChatAsync(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.IdPlayer);
        }
        private async void btSendMessage_Click(object sender, RoutedEventArgs e)
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
    }
}
