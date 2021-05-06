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
        // detta är en timer som jag har hämtat från Threading classen
        DispatcherTimer gameTimer = new DispatcherTimer();
        
        // hitboxerna för spelarna, marken och hindren.
        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

     
        bool jumping;

        // int tal som sepifiserar 
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




        /// <summary>
        /// Denna metoden gör en klocka 
        /// </summary>
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

            Canvas.SetLeft(background, Canvas.GetLeft(background) - 5);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 5);

            // dessa två if satserna gjör så att bakgrunderna repeterar, när en av bakgrunderna slutar så böjar nästa
            if (Canvas.GetLeft(background) < -1262)
            {
                Canvas.SetLeft(background, Canvas.GetLeft(background2) + background2.Width);
            }

            if (Canvas.GetLeft(background2) < -1262)
            {
                Canvas.SetLeft(background2, Canvas.GetLeft(background) + background.Width);
            }

            // flyttar spelaren med den hastighet den har
            Canvas.SetTop(player, Canvas.GetTop(player) + speed);

            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);

            ScoreText.Content = "Score: " + score;

            // gör storleken på hitboxerna 
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width - 15, obstacle.Height);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width - 15, ground.Height);

            // gör så när spelaren träffar boxen så stannar spelranen.
            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);

                jumping = false;

                spriteIndex += .5;

                // när man har dött så stannar animationen på spelaren
                if (spriteIndex > 8)
                {
                    spriteIndex = 1;

                }

                RunSprits(spriteIndex);

            }

            // om man hoppar så bilr man långsamare och hoppar från marken
            if (jumping == true)

            {
                speed = -9;

                force -= 1;
            }
            else
            {
                speed = 12;
            }
            
            // om man inte hoppar så är man mpå marken
            if (force < 0)
            {
                jumping = false;
            }

            // denna if satsen gjör ett nytt obejekt och kollar om man har hoppat höver ett obejekt. Om man har hoppat över så får man ett poäng
            if (Canvas.GetLeft(obstacle) < -50)
            {
                Canvas.SetLeft(obstacle, 950);

                Canvas.SetTop(obstacle, obstacalePostive[rnd.Next(0, obstacalePostive.Length)]);

                score += 1;
            }

            // om man har förlora så stoppas timmern
            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;

                gameTimer.Stop();
            }
            
            // när man har förlorat så skapas det ruta runt hindret och spelaren
            if (gameOver == true)
            {

                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                player.Stroke = Brushes.Red;
                player.StrokeThickness = 1;

                ScoreText.Content = "Score: " + score + "press ENTER to play again";

            }
            else
            {
                player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }

        }

        /// <summary>
        /// när man har förlorat och klickar på enter så startarsom spelet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameOver == true)
            {
                StartGame();
            }

        }
        /// <summary>
        /// när man klickar space och inte redan hoppar så hoppar man
        /// </summary>
        /// <param name="sender"> obejektet </param>
        /// <param name="e"> sender (space) </param>
        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;

                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/newRunner_02.gif"));
            }
        }
        /// <summary>
        /// DEnna metoden startar spelet. 
        /// </summary>
         private void StartGame()
        {
            // sätter bakgrunden på rätt plats
            Canvas.SetLeft(background, 0);
            Canvas.SetLeft(background2, 140);

            // Sätter spelaren så man böjar i luften 
            Canvas.SetLeft(player, 110);
            Canvas.SetTop(player, 310);

            // sätter första två hinderna på plats.
            Canvas.SetLeft(obstacle, 950);
            Canvas.SetLeft(obstacle, 510);

            // böjar RunSprits på bild 1
            RunSprits(1);

            // hindrerna får bilden obstacle.png som texture
            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/obstacle.png"));
            obstacle.Fill = obstacleSprite;

            
             jumping = false;
             gameOver = false;

            // sätter score till 0
             score = 0;

            // visar hur mycker score man har
            ScoreText.Content = "score " + score;

            //startar timmern
            gameTimer.Start();





        }

        /// <summary>
        /// Denna metoden ändrar bilden på karaktären mede en switch.
        /// </summary>
        /// <param name="i"> i böjar som 1 i metoden startgame, sedan blir i spriteIndex som finns i metoden GameEngine</param>

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
