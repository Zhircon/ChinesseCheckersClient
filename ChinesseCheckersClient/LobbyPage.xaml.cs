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
    /// Lógica de interacción para LobbyPage.xaml
    /// </summary>
    public partial class LobbyPage : Page
    {
        private readonly DispatcherTimer gameTimer = new DispatcherTimer();
        private MainWindow mainWindow;
        private int angle;
        public LobbyPage()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
            lbIdRoom.Content = "IdRoom: " + mainWindow.Room.IdRoom;
            SetUp();
        }
        
    

    public void SetUp()
    {
        gameTimer.Tick += GameLoop; //with each iteration of gameloop increment timer 
        gameTimer.Interval = TimeSpan.FromMilliseconds(16); //Set up framerate
        gameTimer.Start(); //Start gameloop logic
        rLoading.RenderTransformOrigin = new Point(0.5f, 0.5f);
    }

        private void GameLoop(object sender, EventArgs e)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            if (angle == 360) { angle = 0; }
            rLoading.RenderTransform = new RotateTransform(angle);
            angle++;
            lbStatus.Content = ChinesseCheckersClient.Properties.Resources.Common_PlayersConnected + mainWindow.PlayerConectedInRoom;
            if(mainWindow.PlayerConectedInRoom == mainWindow.PlayerToWait) { btLaunchMatch.Visibility = Visibility.Visible; }
        }

        private async void btLaunchMatch_Click(object sender, RoutedEventArgs e)
        {
            await NotifyAllPlayerReady();
        }
        private async Task NotifyAllPlayerReady()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var roomMgt = new GameService.RoomMgtClient(new InstanceContext(mainWindow));
            mainWindow.Room = await roomMgt.SearchRoomAsync(mainWindow.Room.IdRoom);
            await roomMgt.NotifyAllPlayersReadyAsync(mainWindow.Room.IdRoom);
        }
    }
}
