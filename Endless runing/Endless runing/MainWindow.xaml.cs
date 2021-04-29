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
using System.Windows.Threading;


namespace Endless_runing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window


    {

        DispatcherTimer gameTimer = new DispatcherTimer();

        Rect playerHitBox;
        Rect groundHitBox;

        bool jumping;

        int force = 20;
        int speed = 5;

        Random rnd = new Random();

        bool gameOver;

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgrundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstacalePostive = { 320 ,310 ,300, 315};

        int score = 0;





        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();

            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            
            backgrundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,image/background.gif"));

            background.Fill = backgrundSprite;
            background2.Fill = backgrundSprite;

            StartGame();


        }

        private void GameEngine(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

        }

         private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 140);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 310);

            RunSprits(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,image/obstacle.gif"));
            obstacle.Fill = obstacleSprite;

             jumping = false;
             gameOver = false;

             score = 0;





        }

        private void RunSprits(double i)
        {

        }
    }
}
