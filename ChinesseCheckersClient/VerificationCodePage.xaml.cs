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
using System.Windows.Shapes;

namespace ChinesseCheckersClient
{
    /// <summary>
    /// Lógica de interacción para VerificationCodePage.xaml
    /// </summary>
    public partial class VerificationCodePage : Window
    {
        private string verificationCodeTyped;
        private readonly string verificationCode;
        private bool isVerificated;
        public bool IsVerificated
        {
            get { return isVerificated; }
        }
        public string VerificationCodeTyped
        {
            get { return verificationCodeTyped; }
            set { verificationCodeTyped = value; }
        }

        public VerificationCodePage(string _code)
        {
            InitializeComponent();
            verificationCode = _code;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            verificationCodeTyped = tb1.Text + tb2.Text + tb3.Text + tb4.Text;
            if (verificationCode.Equals(verificationCodeTyped))
            {
                isVerificated = true;
                lbStatus.Content = "Accepted";
                this.Close();
            }
            else
            {
                lbStatus.Content = "Code not Match";
            }
            
        }

        private void TextBox_TextChange(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Name == "tb1" && tb1.Text!="" ) { tb2.Focus(); }
            if (textBox.Name == "tb2" && tb1.Text != "") { tb3.Focus(); }
            if (textBox.Name == "tb3" && tb1.Text != "") { tb4.Focus(); }
        }
    }
}
