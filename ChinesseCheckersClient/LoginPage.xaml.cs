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
    /// <summary>
    /// provides funtionality signin and login
    /// </summary>
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
            rCheckOperation.Visibility = Visibility.Hidden;
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

            btLogin.IsEnabled = IsDataInputLoginValid() ? true : false;
            btSignin.IsEnabled = IsDataInputSigninValid() ? true : false;
        }

        private void UpdateSigninDuplicatedPasswordStatus(PasswordBox _inputTextBox)
        {
            if (_inputTextBox.Password.Length == 0)
            {
                rCheckDuplicatedPassword.Fill = infoLogoSprite;
                isSigninDuplicatedPasswordValid = false;
                return;
            }
            if (Validator.IsDuplicatePasswordValid(_inputTextBox.Password, pbSigninPassword.Password))
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

        private void UpdateSigninPasswordStatus(PasswordBox _inputTextBox)
        {
            if (_inputTextBox.Password.Length == 0)
            {
                rCheckPassword.Fill = infoLogoSprite;
                isSigninPasswordValid = false;
                return;
            }
            if (Validator.IsPasswordValid(_inputTextBox.Password))
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
                isSigninNicknameValid = false;
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
                isSigninEmailValid = false;
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
        private bool IsDataInputSigninValid()
        {
            if (!isSigninEmailValid) { return false; }
            if (!isSigninNicknameValid) { return false; }
            if (!isSigninPasswordValid) { return false; }
            if (!isSigninDuplicatedPasswordValid) { return false; }
            return true;
        }
        private bool IsDataInputLoginValid()
        {
            if (tbLoginEmail.Text.Length == 0) { return false; }
            if (pbLoginPassword.Password.Length == 0) { return false; }
            return true;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button) sender;
            if (buttonClicked.Name == "btSignin") {await  RegisterPlayer(); }
            if (buttonClicked.Name == "btLogin") { await LoginPlayer(); }
        }

        private async Task LoginPlayer()
        {
            string email = tbLoginEmail.Text;
            string password = pbLoginPassword.Password;
            DisableAllInput();
            lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_Executing;
            rCheckOperation.Visibility = Visibility.Visible;
            try
            {
                GameService.PlayerMgtClient playerMgt = new GameService.PlayerMgtClient();
                GameService.Session session = await playerMgt.LoginAsync(email, password);
                EnableAllInput();
                if (session != null)
                {
                    MainWindow mainWindow= (MainWindow)Application.Current.MainWindow;
                    mainWindow.Session = session;
                    lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_OperationSuccessful;
                    rCheckOperation.Visibility = Visibility.Hidden;
                    ClearTextBoxes();
                    NavigationCommands.GoToMainMenu();
                }
                else
                {
                    lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_NotFoundCredential;
                    rCheckOperation.Visibility = Visibility.Hidden;
                }
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }
        }

        private async Task RegisterPlayer()
        {

            string email = tbSigninEmail.Text;
            string nickname = tbSigninNickname.Text;
            string password = pbSigninPassword.Password;
            DisableAllInput();
            lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_Executing;
            rCheckOperation.Visibility = Visibility.Visible;
            var emailMgt = new GameService.EmailMgtClient();
            string code = await emailMgt.SendVerificationCodeAsync(email);
            var verificationCodePage = new VerificationCodePage(code);
            verificationCodePage.Owner = Application.Current.MainWindow;
            verificationCodePage.ShowDialog();
            if (verificationCodePage.IsVerificated)
            {
                try
                {
                    GameService.PlayerMgtClient playerMgt = new GameService.PlayerMgtClient();
                    GameService.OperationResult operationResult = await playerMgt.RegisterPlayerAsync(nickname, password, email);
                    EnableAllInput();
                    if (operationResult == GameService.OperationResult.Sucessfull)
                    {
                        lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_OperationSuccessful;
                        rCheckOperation.Visibility = Visibility.Hidden;
                        ClearTextBoxes();
                    }
                    else
                    {
                        lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_ErrorDatabase;
                        rCheckOperation.Visibility = Visibility.Hidden;
                    }

                }
                catch (EndpointNotFoundException)
                {
                    NavigationCommands.GoToConnectionLostPage();
                }
            }
            else
            {
                lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_VerificationFail;
                EnableAllInput();
            }
        }
        private void DisableAllInput()
        {
            tbLoginEmail.IsEnabled = false;
            pbLoginPassword.IsEnabled = false;

            tbSigninEmail.IsEnabled = false;
            tbSigninNickname.IsEnabled = false;
            pbSigninPassword.IsEnabled = false;
            pbSigninDuplicatedPassword.IsEnabled = false;
            btSignin.IsEnabled = false;
            btLogin.IsEnabled = false;
        }
        private void EnableAllInput()
        {
            tbLoginEmail.IsEnabled = true;
            pbLoginPassword.IsEnabled = true;
            tbSigninEmail.IsEnabled = true;
            tbSigninNickname.IsEnabled = true;
            pbSigninPassword.IsEnabled = true;
            pbSigninDuplicatedPassword.IsEnabled = true;
            btSignin.IsEnabled = true;
            btLogin.IsEnabled = true;
        }
        private void ClearTextBoxes()
        {
            tbLoginEmail.Text = "";
            pbLoginPassword.Password = "";
            tbSigninEmail.Text = "";
            tbSigninNickname.Text = "";
            pbSigninPassword.Password = "";
            pbSigninDuplicatedPassword.Password= "";
            btSignin.IsEnabled = false;
        }

        private void rPolicies_MouseEnter(object sender, MouseEventArgs e)
        {
            policiesPanel.Visibility = Visibility.Visible;
        }

        private void rPolicies_MouseLeave(object sender, MouseEventArgs e)
        {
            policiesPanel.Visibility = Visibility.Hidden;
        }

        private void PasswordBox_TextChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox inputTextBox = (PasswordBox)sender;
            if (inputTextBox.Name == "pbSigninPassword") { UpdateSigninPasswordStatus(inputTextBox); }
            if (inputTextBox.Name == "pbSigninDuplicatedPassword") { UpdateSigninDuplicatedPasswordStatus(inputTextBox); }
            btLogin.IsEnabled = IsDataInputLoginValid() ? true : false;
            btSignin.IsEnabled = IsDataInputSigninValid() ? true : false;
        }
    }
}
