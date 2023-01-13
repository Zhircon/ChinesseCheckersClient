using ChinesseCheckersClient.GameService;
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
    /// This Windows funtionality like a web browser loading pages in frame. 
    /// </summary>
    public partial class MainWindow : Window , GameService.IRoomMgtCallback 
    {
        private GameService.Session session;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private GameService.Room room;
        private int playerConectedInRoom;
        private int playerToWait;
        private GameService.RoomMgtClient roomMgt;
        private int angle;
        private readonly DispatcherTimer gameTimer = new DispatcherTimer();
        private bool isInGameplay;
        private bool isHost;
        public bool IsHost
        {
            get { return isHost; }
            set { isHost = value; }
        }
        public bool IsInGameplay
        {
            get { return isInGameplay; }
            set { isInGameplay = value; }
        }
        public GameService.RoomMgtClient RoomMgt
        {
            get { return roomMgt; }
            set { roomMgt = value; }
        }
        public int PlayerToWait
        {
            get { return playerToWait; }
            set { playerToWait = value; }
        }

        public int PlayerConectedInRoom
        {
            get { return playerConectedInRoom; }
            set { playerConectedInRoom = value; }
        } 
 
        public GameService.Session Session {
            get { return session; }
            set { session = value; }
        }
        public MediaPlayer MediaPlayer
        {
            get { return mediaPlayer; }
        }
        public GameService.Room Room
        {
            get { return room; }
            set { room = value; }
        }
        public MainWindow()
        {
            InitializeComponent();
            roomMgt = new RoomMgtClient(new InstanceContext(this));
            SetUp();
        }
        public void SetUp()
        {
            gameTimer.Tick += GameLoop; //with each iteration of gameloop increment timer 
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); //Set up framerate
            gameTimer.Start(); //Start gameloop logic
            rSun.RenderTransformOrigin = new Point(0.5, 0.5);
            rHalo1.RenderTransformOrigin = new Point(0.5, 0.5);
            rHalo2.RenderTransformOrigin = new Point(0.5, 0.5);

        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (angle == 360) { angle = 0; }
            angle++;
            rSun.RenderTransform = new RotateTransform(angle);
            rHalo1.RenderTransform = new RotateTransform(angle);
            rHalo2.RenderTransform = new RotateTransform(angle);
        }

        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            Commands.CloseWindow();
        }
        private void btChangeState_Click(object sender, RoutedEventArgs e)
        {
            Commands.ChangeWindowState();
        }
        private void btMinimize_Click(object sender, RoutedEventArgs e)
        {
            Commands.MinimizateWindow();
        }

        void IRoomMgtCallback.SendAllPlayerToGameplay()
        {
            NavigationCommands.GoToGameplay();
        }

        void IRoomMgtCallback.UpdateNumberOfPlayersConected(int _numberOfPlayers)
        {
            playerConectedInRoom = _numberOfPlayers;
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationCommands.GoToLastPage();
        }
    }
}
