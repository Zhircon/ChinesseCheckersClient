﻿using System;
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

namespace ChinesseCheckersClient
{

    public partial class LoginPage : Page
    {
        private ImageBrush correctLogoSprite= new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/CorrectLogo.png")));
        private ImageBrush incorrectLogoSprite= new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/IncorrectLogo.png"))); 
        private ImageBrush infoLogoSprite = new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Assets/Images/InfoLogo.png")));
        public LoginPage()
        {
            InitializeComponent();
            rCheckEmail.Fill = infoLogoSprite;
            rCheckNickname.Fill = infoLogoSprite;
            rCheckPassword.Fill = infoLogoSprite;
            rCheckValidatePassword.Fill = infoLogoSprite;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
