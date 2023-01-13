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
using System.Collections;

namespace ChinesseCheckersClient
{
    /// <summary>
    /// This class provide gameplay and chat funtionality
    /// </summary>
    public partial class GameplayPage : Page  , GameService.IChatMgtCallback ,GameService.IGameplayMgtCallback
    {

        private const char RED = 'R';
        private const char YELLOW = 'M';
        private const char ORANGE = 'N';
        private const char WHITE = 'W';
        private const char NOTHING = 'X';
        private const char FREE = 'O';

        private readonly MainWindow mainWindow;
        private readonly GameService.ChatMgtClient chatMgt;
        private readonly GameplayMgtClient gameplayMgt;
        private readonly GameBoard gameBoard = new GameBoard();
        private List<System.Drawing.Point> lastPosiblesMoves = new List<System.Drawing.Point>();
        private TokenButton tokenSeleted;
        private char playerColor;
        private char colorTurn;
        public GameplayPage()
        {
            InitializeComponent();
            mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.IsInGameplay = true;
            mainWindow.btBack.Visibility = Visibility.Hidden;
            chatMgt = new GameService.ChatMgtClient(new InstanceContext(this));
            gameplayMgt = new GameplayMgtClient(new InstanceContext(this));
            ConfigureGameboard();
            JoinAndUpdateRoom();
            
            FillListBoxFriendList();
            RepresentGameBoard();
            GetTurn();
            AssingColor();
        }
        private string ConvertColorToString(char _color)
        {
            String colorString;
            switch (_color)
            {
                case FREE:
                    colorString = ChinesseCheckersClient.Properties.Resources.Common_Free;
                    break;
                case ORANGE:
                    colorString = ChinesseCheckersClient.Properties.Resources.Common_Orange;
                    break;
                case RED:
                    colorString = ChinesseCheckersClient.Properties.Resources.Common_Red;
                    break;
                case WHITE:
                    colorString = ChinesseCheckersClient.Properties.Resources.Common_White;
                    break;
                case YELLOW:
                    colorString = ChinesseCheckersClient.Properties.Resources.Common_Yellow;
                    break;
                default:
                    colorString = ChinesseCheckersClient.Properties.Resources.Common_NotColor;
                    break;
            }
            return colorString;
        }
        private void GetTurn()
        {
            colorTurn = gameplayMgt.GetCurrentTurn(mainWindow.Room.IdRoom);
            lbTurn.Content = ChinesseCheckersClient.Properties.Resources.Common_Turn +": " + ConvertColorToString(colorTurn);
        }
        private  void AssingColor()
        {
            playerColor = gameplayMgt.AssingColor(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.IdPlayer);
            lbYourColor.Content = ChinesseCheckersClient.Properties.Resources.Common_YourColor +": " + ConvertColorToString(playerColor);
        }
        private void ConfigureGameboard()
        {
            if (mainWindow.Room.NumberOfAllowedPlayers == 2)
            {
                gameBoard.ConfigureForTwoPlayers();
            }
            else
            {
                gameBoard.ConfigureForThrePlayers();
            }
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
                    if (token != NOTHING)
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
                case FREE:
                    _buttonToken.Background = new SolidColorBrush(Colors.Gray);
                    break;
                case ORANGE:
                    _buttonToken.Background = new SolidColorBrush(Colors.Orange);
                    break;
                case RED:
                    _buttonToken.Background = new SolidColorBrush(Colors.Red);
                    break;
                case WHITE:
                    _buttonToken.Background = new SolidColorBrush(Colors.White);
                    break;
                case YELLOW:
                    _buttonToken.Background = new SolidColorBrush(Colors.Yellow);
                    break;
                default:
                    _buttonToken.Background = new SolidColorBrush(Colors.Transparent);
                    break;
            }
        }
        private async void ButtonToken_Click(object sender, RoutedEventArgs e)
        {
            var buttonToken = (TokenButton)sender;
            if (colorTurn != playerColor) { return; }
            if (buttonToken.IsPosibleMovement && tokenSeleted.HideContent==playerColor)
            {
                RecoloredPosibleMoves();
                await MoveTo(tokenSeleted.HideContent, buttonToken.Position);
                await gameplayMgt.TerminateTurnAsync(mainWindow.Room.IdRoom);
                Console.WriteLine("winner"+gameBoard.CheckWinColor());
            }
            else
            {
                tokenSeleted = (TokenButton)sender;
                if (tokenSeleted.HideContent != FREE && tokenSeleted.HideContent == playerColor)
                {
                    RecoloredPosibleMoves();
                    var posiblesMoves = gameBoard.GetAllPosiblesMoves(buttonToken.Position);
                    lastPosiblesMoves = posiblesMoves;
                    foreach (System.Drawing.Point point in posiblesMoves)
                    {
                        int column = (int)point.X;
                        int row = (int)point.Y;
                        var element = gridGameBoard.Children
                        .Cast<TokenButton>().First(r => Grid.GetRow(r) == row && Grid.GetColumn(r) == column);
                        element.Background = new SolidColorBrush(Colors.Violet);
                        element.IsPosibleMovement = true;
                    }
                }
                else
                {
                    RecoloredPosibleMoves();
                }
            }

        }
        public async Task MoveTo(char _charToken, System.Drawing.Point _to)
        {
            var _compatibleTo = new System.Windows.Point();
            var _compatibleFrom = new System.Windows.Point();
            _compatibleFrom.X = tokenSeleted.Position.X;
            _compatibleFrom.Y = tokenSeleted.Position.Y;
            _compatibleTo.X = _to.X;
            _compatibleTo.Y = _to.Y;
            await gameplayMgt.MoveTokenAsync(mainWindow.Room.IdRoom, _charToken, _compatibleFrom, _compatibleTo);
        }
        private void RecoloredPosibleMoves()
        {
            foreach (System.Drawing.Point point in lastPosiblesMoves)
            {
                var token = gameBoard.GetPosition(point);
                int column = (int)point.X;
                int row = (int)point.Y;
                var element = gridGameBoard.Children
                .Cast<TokenButton>().First(r => Grid.GetRow(r) == row && Grid.GetColumn(r) == column);
                element.IsPosibleMovement = false;
                ColoringTokensButtons(token, ref element);
            }
        }

        private async void FillListBoxFriendList()
        {
            var roomMgt = new GameService.RoomMgtClient(new InstanceContext(mainWindow));
            mainWindow.Room = await roomMgt.SearchRoomAsync(mainWindow.Room.IdRoom);
            foreach (GameService.Player player in mainWindow.Room.Players.Values)
            {
                if (player.IdPlayer != mainWindow.Session.PlayerLoged.IdPlayer)
                {
                    var listBoxItem = new ListBoxItem();
                    var friendButton = new FriendButton();
                    friendButton.Content = player.Nickname;
                    friendButton.IdPlayer = player.IdPlayer;
                    friendButton.Nickname = player.Nickname;
                    friendButton.Email = player.Email;
                    friendButton.Click += FriendButton_Click;
                    listBoxItem.Content = friendButton;
                    listBoxFriendButtons.Items.Add(listBoxItem);
                }
            }
        }

        private async void FriendButton_Click(object sender, RoutedEventArgs e)
        {
            var friendButton = (FriendButton)sender;
            await SendFriendRequest(mainWindow.Session.PlayerLoged.IdPlayer, mainWindow.Session.PlayerLoged.Nickname, friendButton.IdPlayer,friendButton.Nickname);
        }
        private async Task SendFriendRequest(int _idApplicantPlayer,string _nicknameApplicantPlayer,int _idPlayerAddressed,string _nicknameAddressedPlayer)
        {
            var listBoxItem = new ListBoxItem();
            try
            {
                await chatMgt.SendFrienRequestAsync(mainWindow.Room.IdRoom, _idApplicantPlayer, _nicknameApplicantPlayer, _idPlayerAddressed);
                listBoxItem.Content = ChinesseCheckersClient.Properties.Resources.Common_FriendRequestSended  +_nicknameAddressedPlayer;
                listBoxMessage.Items.Add(listBoxItem);
            }
            catch (EndpointNotFoundException)
            {  
                listBoxItem.Content = ChinesseCheckersClient.Properties.Resources.Common_RequestNotSended;
                listBoxMessage.Items.Add(listBoxItem);
            }
        }

        private async void JoinAndUpdateRoom()
        {
            await chatMgt.JoinToChatAsync(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.IdPlayer);
            await gameplayMgt.JoinToGameplayAsync(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.IdPlayer);
        }

        private async Task SendChatMessage()
        {
            string message = tbMessage.Text;
            try
            {
                await chatMgt.SendMessageAsync(mainWindow.Room.IdRoom, mainWindow.Session.PlayerLoged.Nickname, message);
            }catch(EndpointNotFoundException){
                tbMessage.Text = "";
            }
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

        void IChatMgtCallback.ReceiveFriendRequest(int _idApplicantPlayer, string _nicknameApplicantPlayer)
        {
            var listBoxItem = new ListBoxItem();
            var friendButton = new FriendButton();
            friendButton.Content = "(Click) Aceptar amigo : " + _nicknameApplicantPlayer;
            friendButton.IdPlayer = _idApplicantPlayer;
            friendButton.Click += FriendRequest_Click;
            listBoxItem.Content = friendButton;

            listBoxMessage.Items.Add(listBoxItem);
        }

        private async void FriendRequest_Click(object sender, RoutedEventArgs e)
        {
            var friendButton = (FriendButton)sender;
            var relationshipMgt =new GameService.RelationshipMgtClient();
            try
            {
                await relationshipMgt.CreateRelationshipAsync(friendButton.IdPlayer, mainWindow.Session.PlayerLoged.IdPlayer);
                var listBoxItem = new ListBoxItem();
                listBoxItem.Content = ChinesseCheckersClient.Properties.Resources.Common_FriendAdded;
                listBoxMessage.Items.Add(listBoxItem);
            }
            catch (EndpointNotFoundException)
            {
                var listBoxItem = new ListBoxItem();
                listBoxItem.Content = ChinesseCheckersClient.Properties.Resources.Commin_FailToCreateRelation;
                listBoxMessage.Items.Add(listBoxItem);
            }
            
        }

        void IGameplayMgtCallback.MoveAllPlayers(char _charToken, System.Windows.Point _from, System.Windows.Point _to)
        {
            System.Drawing.Point _compatibleFrom = new System.Drawing.Point();
            System.Drawing.Point _compatibleTo = new System.Drawing.Point();
            _compatibleFrom.X= Convert.ToInt32(_from.X);
            _compatibleFrom.Y = Convert.ToInt32(_from.Y);
            _compatibleTo.X  = Convert.ToInt32(_to.X);
            _compatibleTo.Y = Convert.ToInt32(_to.Y);

            var gridFrom = gridGameBoard.Children
            .Cast<TokenButton>().First(r => Grid.GetRow(r) == _compatibleFrom.Y && Grid.GetColumn(r) == _compatibleFrom.X);
            gridFrom.HideContent = FREE;
            var gridTo = gridGameBoard.Children
            .Cast<TokenButton>().First(r => Grid.GetRow(r) == _compatibleTo.Y && Grid.GetColumn(r) == _compatibleTo.X);
            gridTo.HideContent = _charToken;

            gameBoard.SetPosition(FREE, _compatibleFrom);
            gameBoard.SetPosition(_charToken, _compatibleTo);

            ColoringTokensButtons(_charToken, ref gridTo);
            ColoringTokensButtons(FREE, ref gridFrom);

        }

        void IGameplayMgtCallback.ChangeTurn(char _colorTurn)
        {
            colorTurn = _colorTurn;
            lbTurn.Content = ChinesseCheckersClient.Properties.Resources.Common_Turn  +" : "+ ConvertColorToString(colorTurn);
        }

        void IGameplayMgtCallback.GameOver(string _playerNicknameDeclare, string _message)
        {
            throw new NotImplementedException();
        }
    }
}
