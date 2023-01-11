using ChinesseCheckersClient.GameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows;

namespace ChinesseCheckersClient
{
    /// <summary>
    /// This class provide gameplay and chat funtionality
    /// </summary>
    public partial class GameplayPage : Page  , GameService.IChatMgtCallback
    {
        private readonly MainWindow mainWindow;
        private readonly GameService.ChatMgtClient chatMgt;
        private GameBoard gameBoard = new GameBoard();
        private List<System.Drawing.Point> lastPosiblesMoves = new List<System.Drawing.Point>();
        public GameplayPage()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            
           
            chatMgt = new GameService.ChatMgtClient(new InstanceContext(this));
            JoinAndUpdateRoom();
            FillListBoxFriendList();
            RepresentGameBoard();
        }
        private void RepresentGameBoard()
        {
            char token;

            for (int row = 0; row < 15; row++)
            {
                for (int column = 0; column < 21; column++)
                {
                    var position = new System.Drawing.Point(column, row);
                    token = gameBoard.GetPosition(position);
                    if (token != 'X')
                    {
                        var buttonToken = new TokenButton();
                        buttonToken.Content = position.X + "," + position.Y;
                        buttonToken.Position = position;
                        buttonToken.Click += ButtonToken_Click;
                        buttonToken.HideContent = token;
                        ColoringTokensButtons(token,ref buttonToken);
                        Grid.SetColumn(buttonToken, column);
                        Grid.SetRow(buttonToken, row);
                        gridGameBoard.Children.Add(buttonToken);
                    }

                }

            }
        }
        private void ColoringTokensButtons(char _token,ref TokenButton _buttonToken)
        {
            switch (_token)
            {
                case 'O':
                    _buttonToken.Background = new SolidColorBrush(Colors.Gray);
                    break;
                case 'A':
                    _buttonToken.Background = new SolidColorBrush(Colors.Blue);
                    break;
                case 'N':
                    _buttonToken.Background = new SolidColorBrush(Colors.Orange);
                    break;
                case 'R':
                    _buttonToken.Background = new SolidColorBrush(Colors.Red);
                    break;
                case 'V':
                    _buttonToken.Background = new SolidColorBrush(Colors.Green);
                    break;
                case 'B':
                    _buttonToken.Background = new SolidColorBrush(Colors.White);
                    break;
                case 'M':
                    _buttonToken.Background = new SolidColorBrush(Colors.Yellow);
                    break;
            }
        }
        private void ButtonToken_Click(object sender, RoutedEventArgs e)
        {
            foreach (System.Drawing.Point point in lastPosiblesMoves)
            {
                var token = gameBoard.GetPosition(point);
                int column = (int)point.X;
                int row = (int)point.Y;
                var element = gridGameBoard.Children
                .Cast<TokenButton>().First(r => Grid.GetRow(r) == row && Grid.GetColumn(r) == column);
                ColoringTokensButtons(token, ref element);
            }
            var buttonToken = (TokenButton)sender; 
            var posiblesMoves = gameBoard.GetAllPosiblesMoves(buttonToken.Position);
            lastPosiblesMoves = posiblesMoves;
            foreach(System.Drawing.Point point in posiblesMoves)
            {
                var token = gameBoard.GetPosition(point);
                int column = (int)point.X;
                int row = (int)point.Y;
                var element = gridGameBoard.Children
                .Cast<TokenButton>().First(r => Grid.GetRow(r) == row && Grid.GetColumn(r) == column);
                element.Background = new SolidColorBrush(Colors.Violet);
            }
        }

        private async void FillListBoxFriendList()
        {
            var roomMgt = new GameService.RoomMgtClient(new InstanceContext(mainWindow));
            mainWindow.Room = await roomMgt.SearchRoomAsync(mainWindow.Room.IdRoom);
            foreach (GameService.Player player in mainWindow.Room.Players.Values)
            {
                var listBoxItem = new ListBoxItem();
                var friendButton = new FriendButton();
                friendButton.Content = player.Nickname;
                friendButton.IdPlayer = player.IdPlayer;
                friendButton.Nickname = player.Nickname;
                friendButton.Email = player.Email;
                friendButton.Click += FriendButton_Click;
                listBoxItem.Content = friendButton.Nickname;
                listBoxFriendButtons.Items.Add(listBoxItem);
            }
        }

        private void FriendButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
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
