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
using System.ServiceModel;

namespace ChinesseCheckersClient
{
    /// <summary>
    /// Lógica de interacción para FriendListPage.xaml
    /// </summary>
    public partial class FriendListPage : Page
    {
        private readonly MainWindow mainWindow;
        private GameService.Player playerSelected;
        private bool isSubjectValid;
        private bool isBodyValid;
        public FriendListPage()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
            FillFriendshipsAsync();
            UpdateEnables();
        }
        public async void  FillFriendshipsAsync()
        {
            var relationshipMgt = new GameService.RelationshipMgtClient();
            var playerMgt = new GameService.PlayerMgtClient();
            var relationships = await relationshipMgt.GetRelationshipsAsync(mainWindow.Session.PlayerLoged.IdPlayer);
            foreach(GameService.Relationship element in relationships)
            {
                if (!element.IsBadRelation)
                {
                    try
                    {
                        var playerGuest = await playerMgt.SearchPlayerByIdAsync(element.idGuest);
                        var listBoxItem = new ListBoxItem();
                        FriendButton friendButton = new FriendButton
                        {
                            IdPlayer = playerGuest.IdPlayer,
                            Nickname = playerGuest.Nickname,
                            Email = playerGuest.Email,
                            Content = playerGuest.Nickname,
                        };
                        friendButton.Click += FriendButton_Click;
                        listBoxItem.Content = friendButton;
                        listBoxFriends.Items.Add(listBoxItem);
                    }
                    catch (EndpointNotFoundException)
                    {
                        listBoxFriends.Items.Clear();
                        NavigationCommands.GoToConnectionLostPage();
                    }
                }
            }
        }
        
        private void FriendButton_Click(object sender, RoutedEventArgs e)
        {
            FriendButton friendButton = (FriendButton)sender;
            playerSelected = new GameService.Player
            {
                IdPlayer = friendButton.IdPlayer,
                Nickname = friendButton.Nickname,
                Email = friendButton.Email
            };
            UpdateLabels();
            UpdateEnables();
        }
        private void UpdateEnables()
        {
            if (playerSelected != null)
            {
                btBanFriend.IsEnabled = true;
                tbMessage.IsEnabled = true;
                tbSubject.IsEnabled = true;
            }
            else
            {
                btBanFriend.IsEnabled = false;
                tbMessage.IsEnabled = false;
                tbSubject.IsEnabled = false;
                btSendMessage.IsEnabled = false;
            }
        }
        private void UpdateLabels()
        {
            lbMessage.Content = "Message to: " + playerSelected.Nickname + "(" + playerSelected.Email + ")";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button.Name == "btSendMessage") { await SendEmailMessage(); }
            if (button.Name == "btBanFriend") { await BanPlayer(); }
        }

        private async Task BanPlayer()
        {
            var relationshipsMgt = new GameService.RelationshipMgtClient();
            try
            {
                await relationshipsMgt.DeclarateBadRelationshipAsync(mainWindow.Session.PlayerLoged.IdPlayer, playerSelected.IdPlayer);
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }
            
        }

        private async Task SendEmailMessage()
        {
            var emailMgt = new GameService.EmailMgtClient();
            try
            {
                await emailMgt.SendEmailAsync(playerSelected.Email, tbSubject.Text, tbMessage.Text);
                tbSubject.Text = "";
                tbMessage.Text = "";
            }
            catch (EndpointNotFoundException)
            {
                listBoxFriends.Items.Clear();
                NavigationCommands.GoToConnectionLostPage();
            }
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if (textbox.Name == "tbSubject") { isSubjectValid = Validator.IsSubjectValid(textbox.Text);}
            if (textbox.Name == "tbMessage") { isBodyValid = Validator.IsSubjectValid(textbox.Text); }
            btSendMessage.IsEnabled = (isSubjectValid && isBodyValid) ? true : false;
        }

    }
}
