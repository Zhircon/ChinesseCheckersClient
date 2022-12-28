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
using System.Windows.Threading;

namespace ChinesseCheckersClient
{

    public partial class LoginPage : Page
    {
        private readonly ImageBrush correctLogoSprite= new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/CorrectLogo.png")));
        private readonly ImageBrush incorrectLogoSprite= new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/IncorrectLogo.png"))); 
        private readonly ImageBrush infoLogoSprite = new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/InfoLogo.png")));
        private readonly ImageBrush loadingLogoSprite = new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/LoadingLogo.png")));
        
        private bool isSigninEmailValid = false;
        private bool isSigninNicknameValid = false;
        private bool isSigninPasswordValid = false;
        private bool isSigninDuplicatedPasswordValid = false;

        private int angle = 0;

        private DispatcherTimer gameTimer = new DispatcherTimer();
        public LoginPage()
        {
            InitializeComponent();
            SetUp();

            
        }

        public void SetUp()
        {
            gameTimer.Tick += GameLoop; //with each iteration of gameloop increment timer 
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); //Set up framerate
            gameTimer.Start(); //Start gameloop logic

            rCheckEmail.Fill = infoLogoSprite;
            rCheckNickname.Fill = infoLogoSprite;
            rCheckPassword.Fill = infoLogoSprite;
            rCheckDuplicatedPassword.Fill = infoLogoSprite;
            rCheckOperation.Fill = loadingLogoSprite;
            rCheckOperation.RenderTransformOrigin = new Point(0.5f, 0.5f);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (angle == 360) { angle = 0; }
            rCheckOperation.RenderTransform = new RotateTransform(angle+=1);
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox inputTextBox = (TextBox)sender;
            if (inputTextBox.Name == "tbSigninEmail") { updateSigninEmailStatus(inputTextBox); }
            if (inputTextBox.Name == "tbSigninNickname") { updateSigninNicknameStatus(inputTextBox); }
            if (inputTextBox.Name == "tbSigninPassword") { updateSigninPasswordStatus(inputTextBox); }
            if (inputTextBox.Name == "tbSigninDuplicatedPassword") { updateSigninDuplicatedPasswordStatus(inputTextBox); }
            btSignin.IsEnabled = isDataInputValid() ? true : false;
            //if (isDataInputValid()) { btSignin.IsEnabled = true; } else { btSignin.IsEnabled = false; }
        }

        private void updateSigninDuplicatedPasswordStatus(TextBox _inputTextBox)
        {
            if (_inputTextBox.Text.Length == 0) { 
                rCheckDuplicatedPassword.Fill = infoLogoSprite;
                return;
            }
            if (Validator.IsDuplicatePasswordValid(_inputTextBox.Text, tbSigninPassword.Text))
            {
                rCheckDuplicatedPassword.Fill = correctLogoSprite;
                isSigninDuplicatedPasswordValid = true;
            }
            else
            {
                rCheckDuplicatedPassword.Fill = incorrectLogoSprite;
                isSigninDuplicatedPasswordValid = false;
            }
        }

        private void updateSigninPasswordStatus(TextBox _inputTextBox)
        {
            if (_inputTextBox.Text.Length == 0) 
            { 
                rCheckPassword.Fill = infoLogoSprite;
                return;
            }
            if (Validator.IsPasswordValid(_inputTextBox.Text))
            {
                rCheckPassword.Fill = correctLogoSprite;
                isSigninPasswordValid = true;
            }
            else
            {
                rCheckPassword.Fill = incorrectLogoSprite;
                isSigninPasswordValid = false;
            }
        }

        private void updateSigninNicknameStatus(TextBox _inputTextBox)
        {
            if (_inputTextBox.Text.Length == 0) 
            { 
                rCheckNickname.Fill = infoLogoSprite;
                return;
            }
            if (Validator.IsNicknameValid(_inputTextBox.Text))
            {
                rCheckNickname.Fill = correctLogoSprite;
                isSigninNicknameValid = true;
            }
            else
            {
                rCheckNickname.Fill = incorrectLogoSprite;
                isSigninNicknameValid = false;
            }
            
        }

        private void updateSigninEmailStatus(TextBox _inputTextBox)
        {
            if (_inputTextBox.Text.Length == 0) 
            { 
                rCheckEmail.Fill = infoLogoSprite;
                return;            
            }
            if (Validator.IsEmailValid(_inputTextBox.Text))
            {
                rCheckEmail.Fill = correctLogoSprite;
                isSigninEmailValid = true;
            }
            else
            {
                rCheckEmail.Fill = incorrectLogoSprite;
                isSigninEmailValid = false;
            }
        }
        private bool isDataInputValid()
        {
            if (!isSigninEmailValid) { return false; }
            if (!isSigninNicknameValid) { return false; }
            if (!isSigninPasswordValid) { return false; }
            if (!isSigninDuplicatedPasswordValid) { return false; }
            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button) sender;
            if (buttonClicked.Name == "btSignin") { registerPlayer(); }
            if (buttonClicked.Name == "btLogin") { loginPlayer(); }
        }

        private void loginPlayer()
        {
            throw new NotImplementedException();
        }

        private async void registerPlayer()
        {
            
            string email = tbSigninEmail.Text;
            string nickname = tbSigninNickname.Text;
            string password = tbSigninPassword.Text;
            
            btSignin.IsEnabled = false;
            btLogin.IsEnabled = false;
            rCheckOperation.Visibility = Visibility.Visible;
            tryRegister(nickname, password, email);
            rCheckOperation.Visibility = Visibility.Hidden;
            btSignin.IsEnabled = true;
            btLogin.IsEnabled = true;
            tbSigninEmail.Text = "";
            tbSigninNickname.Text = "";
            tbSigninPassword.Text = "";
            tbSigninDuplicatedPassword.Text = "";
        }
        private async void tryRegister(string _nickname,string _password,string _email)
        {
            try
            {
                GameService.PlayerMgtClient playerMgt = new GameService.PlayerMgtClient();
                await playerMgt.RegisterPlayerAsync(_nickname, _password, _email);
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }

        }
    }
}
