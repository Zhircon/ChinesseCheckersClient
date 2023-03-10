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
    /// this page provide funtionality for reconnect server
    /// </summary>
    public partial class ConnectionLostPage : Page
    {
        
        private readonly DispatcherTimer gameTimer = new DispatcherTimer();
        private int angle;
        public ConnectionLostPage()
        {
            InitializeComponent();
            SetUp();
        }
        public void SetUp()
        {
            gameTimer.Tick += GameLoop; //with each iteration of gameloop increment timer 
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); //Set up framerate
            gameTimer.Start(); //Start gameloop logic

            rLoadingLogo.RenderTransformOrigin = new Point(0.5f, 0.5f);
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (angle == 360) { angle = 0; }
            rLoadingLogo.RenderTransform = new RotateTransform(angle);
            angle++;
        }

        private async Task tryToConnectToServerAsync()
        {
            
            var connectionTestMgt = new GameService.ConnectionTestMgtClient();
            try
            {
                rLoadingLogo.Visibility = Visibility.Visible;
                canvas.Background = new SolidColorBrush(Colors.Yellow);
                btTry.Content = ChinesseCheckersClient.Properties.Resources.Common_WaitingForServerReply;
                btTry.IsEnabled = false;
                await connectionTestMgt.IsConnectionUpAsync();  // if this failed then go to exception 
                rLoadingLogo.Visibility = Visibility.Hidden;    
                btTry.Content = ChinesseCheckersClient.Properties.Resources.Common_ConnectionUp;
                canvas.Background = new SolidColorBrush(Colors.Green);
                NavigationCommands.GoToLastPage();

            }
            catch(EndpointNotFoundException)
            {
                rLoadingLogo.Visibility = Visibility.Hidden;
                canvas.Background = new SolidColorBrush(Color.FromArgb(0xFF,0x95,0x10,0x10));
                btTry.Content = ChinesseCheckersClient.Properties.Resources.Common_Retry;
                btTry.IsEnabled = true;
            }
        }

        private async void btTry_Click(object sender, RoutedEventArgs e)
        {
            await tryToConnectToServerAsync();
        }
    }
}
