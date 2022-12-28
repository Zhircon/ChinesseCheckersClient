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
        private readonly DispatcherTimer gameTimer = new DispatcherTimer();

        //By default everthing bool is false
        private bool isSigninEmailValid;
        private bool isSigninNicknameValid;
        private bool isSigninPasswordValid;
        private bool isSigninDuplicatedPasswordValid;

        private int angle;

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
            rCheckOperation.RenderTransform = new RotateTransform(angle);
            angle++;
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox inputTextBox = (TextBox)sender;
            if (inputTextBox.Name == "tbSigninEmail") { UpdateSigninEmailStatus(inputTextBox); }
            if (inputTextBox.Name == "tbSigninNickname") { UpdateSigninNicknameStatus(inputTextBox); }
            if (inputTextBox.Name == "tbSigninPassword") { UpdateSigninPasswordStatus(inputTextBox); }
            if (inputTextBox.Name == "tbSigninDuplicatedPassword") { UpdateSigninDuplicatedPasswordStatus(inputTextBox); }
            btSignin.IsEnabled = IsDataInputValid() ? true : false;
        }

        private void UpdateSigninDuplicatedPasswordStatus(TextBox _inputTextBox)
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

        private void UpdateSigninPasswordStatus(TextBox _inputTextBox)
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

        private void UpdateSigninNicknameStatus(TextBox _inputTextBox)
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

        private void UpdateSigninEmailStatus(TextBox _inputTextBox)
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
        private bool IsDataInputValid()
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
            if (buttonClicked.Name == "btSignin") { RegisterPlayer(); }
            if (buttonClicked.Name == "btLogin") { LoginPlayer(); }
        }

        private void LoginPlayer()
        {
            throw new NotImplementedException();
        }

        private async void RegisterPlayer()
        {

            string email = tbSigninEmail.Text;
            string nickname = tbSigninNickname.Text;
            string password = tbSigninPassword.Text;
            btSignin.IsEnabled = false;
            btLogin.IsEnabled = false;
            rCheckOperation.Visibility = Visibility.Visible;
            try
            {
                GameService.PlayerMgtClient playerMgt = new GameService.PlayerMgtClient();
                await playerMgt.RegisterPlayerAsync(nickname, password, email);
                rCheckOperation.Visibility = Visibility.Hidden;
                btSignin.IsEnabled = true;
                btLogin.IsEnabled = true;
                tbSigninEmail.Text = "";
                tbSigninNickname.Text = "";
                tbSigninPassword.Text = "";
                tbSigninDuplicatedPassword.Text = "";
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }


        }



    }
}
