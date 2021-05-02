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

            
            backgrundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/background.gif"));

            background.Fill = backgrundSprite;
            background2.Fill = backgrundSprite;

            StartGame();


        }

        private void GameEngine(object sender, EventArgs e)
        {

            Canvas.SetLeft(background, Canvas.GetLeft(background) - 3);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 3);

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && jumping == true && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_02.gif"));
            }
        }

         private void StartGame()
        {
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 140);

            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 310);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetLeft(obstacle, 310);

            RunSprits(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/obstacle.png"));
            obstacle.Fill = obstacleSprite;

             jumping = false;
             gameOver = false;

             score = 0;

            ScoreText.Content = "score " + score;

            gameTimer.Start();





        }

        private void RunSprits(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_01.gif"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_02.gif"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_03.gif"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_04.gif"));
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_05.gif"));
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_06.gif"));
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_07.gif"));
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_08.gif"));
                    break;
            }

            player.Fill = playerSprite;
        }
    }
}
