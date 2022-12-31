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
    public partial class UpdateProfilePanel : UserControl
    {
        private readonly ImageBrush correctLogoSprite = new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/CorrectLogo.png")));
        private readonly ImageBrush incorrectLogoSprite = new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/IncorrectLogo.png")));
        private readonly ImageBrush infoLogoSprite = new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/InfoLogo.png")));
        
        private bool isEmailValid;
        private bool isNicknameValid;
        private bool isPasswordValid;
        private bool isDuplicatedPasswordValid;

        public UpdateProfilePanel()
        {
            InitializeComponent();
        }
        private void UpdateDuplicatedPasswordStatus(PasswordBox _inputTextBox)
        {
            if (_inputTextBox.Password.Length == 0)
            {
                rCheckDuplicatedPassword.Fill = infoLogoSprite;
                isDuplicatedPasswordValid = false;
                return;
            }
            if (Validator.IsDuplicatePasswordValid(_inputTextBox.Password, pbPassword.Password))
            {
                rCheckDuplicatedPassword.Fill = correctLogoSprite;
                isDuplicatedPasswordValid = true;
            }
            else
            {
                rCheckDuplicatedPassword.Fill = incorrectLogoSprite;
                isDuplicatedPasswordValid = false;
            }
        }

        private void UpdatePasswordStatus(PasswordBox _inputTextBox)
        {
            if (_inputTextBox.Password.Length == 0)
            {
                rCheckPassword.Fill = infoLogoSprite;
                isPasswordValid = false;
                return;
            }
            if (Validator.IsPasswordValid(_inputTextBox.Password))
            {
                rCheckPassword.Fill = correctLogoSprite;
                isPasswordValid = true;
            }
            else
            {
                rCheckPassword.Fill = incorrectLogoSprite;
                isPasswordValid = false;
            }
        }

        private void UpdateNicknameStatus(TextBox _inputTextBox)
        {
            if (_inputTextBox.Text.Length == 0)
            {
                rCheckNickname.Fill = infoLogoSprite;
                isNicknameValid = false;
                return;
            }
            if (Validator.IsNicknameValid(_inputTextBox.Text))
            {
                rCheckNickname.Fill = correctLogoSprite;
                isNicknameValid = true;
            }
            else
            {
                rCheckNickname.Fill = incorrectLogoSprite;
                isNicknameValid = false;
            }

        }

        private void UpdateEmailStatus(TextBox _inputTextBox)
        {
            if (_inputTextBox.Text.Length == 0)
            {
                rCheckEmail.Fill = infoLogoSprite;
                isEmailValid = false;
                return;
            }
            if (Validator.IsEmailValid(_inputTextBox.Text))
            {
                rCheckEmail.Fill = correctLogoSprite;
                isEmailValid = true;
            }
            else
            {
                rCheckEmail.Fill = incorrectLogoSprite;
                isEmailValid = false;
            }
        }
        private void btSave_Click(object sender, RoutedEventArgs e)
        { 
            UpdatePlayer(); 
        }
        private bool IsDataInputValid()
        {
            if (!isEmailValid) { return false; }
            if (!isNicknameValid) { return false; }
            if (!isPasswordValid) { return false; }
            if (!isDuplicatedPasswordValid) { return false; }
            return true;
        }
        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox inputTextBox = (TextBox)sender;
            if (inputTextBox.Name == "tbEmail") { UpdateEmailStatus(inputTextBox); }
            if (inputTextBox.Name == "tbNickname") { UpdateNicknameStatus(inputTextBox); }
            btSave.IsEnabled = IsDataInputValid() ? true : false;
        }
        private void PasswordBox_TextChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox inputTextBox = (PasswordBox)sender;
            if (inputTextBox.Name == "pbPassword") { UpdatePasswordStatus(inputTextBox); }
            if (inputTextBox.Name == "pbValidatePassword") { UpdateDuplicatedPasswordStatus(inputTextBox); }
            btSave.IsEnabled = IsDataInputValid() ? true : false;
        }
        private async void UpdatePlayer()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var playerUpdated = mainWindow.Session.PlayerLoged;
            string email = tbEmail.Text;
            string nickname = tbNickname.Text;
            string password = pbPassword.Password;
            playerUpdated.Email=email;
            playerUpdated.Nickname = nickname;
            playerUpdated.Password = password;
            btSave.Content = "Saving";
            DisableAllInput();
            try
            {
                GameService.PlayerMgtClient playerMgt = new GameService.PlayerMgtClient();
                GameService.OperationResult operationResult = await playerMgt.UpdatePlayerAsync(playerUpdated);
                EnableAllInput();
                if (operationResult == GameService.OperationResult.Sucessfull)
                {
                    btSave.Content = "Saved";
                }
                else
                {
                    btSave.Content = "Fail";
                }
            }
            catch (EndpointNotFoundException)
            {
                NavigationCommands.GoToConnectionLostPage();
            }
        }
        private void DisableAllInput()
        {
            tbEmail.IsEnabled = false;
            tbNickname.IsEnabled = false;
            pbPassword.IsEnabled = false;
            pbValidatePassword.IsEnabled = false;
            btSave.IsEnabled = false;
        }
        private void EnableAllInput()
        {
            tbEmail.IsEnabled = true;
            tbNickname.IsEnabled = true;
            pbPassword.IsEnabled = true;
            pbValidatePassword.IsEnabled = true;
            btSave.IsEnabled = true;
        }

    }
}
