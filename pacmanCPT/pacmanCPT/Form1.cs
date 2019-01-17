using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace pacmanCPT
{
    public partial class frmpacmanCPT : Form
    {
        //sound player
        private SoundPlayer bgsoundPlayer = new SoundPlayer();
        
        //start the variable 
        bool goup = false;
        bool godown = false;
        bool goleft = false;
        bool goright = false;

        int speedlevel1 = 5;

        //ghost 1 and 2 variables. these guys are sane well sort of
        int ghost1 = 6;
        int ghost2 = 6;

        //ghost 3 crazy variables
        int ghost3x = 6;
        int ghost3y = 6;

        int score = 0;

        int timerMaxVal = 45;
        int timerValue;
        

        public frmpacmanCPT()
        {
            InitializeComponent();

            Thread splashscreen = new Thread(new ThreadStart(splashStrat));
            splashscreen.Start();

            Thread.Sleep(5000);
            splashscreen.Abort();

            label2.Visible = false;

            bgsoundPlayer = new SoundPlayer("pacman_beginning.wav");
            bgsoundPlayer.Play();
            
        }

        public void splashStrat()
        {
            Application.Run(new introGame());
        }

        private void keyisdown1(object sender, KeyEventArgs e)
        {
            Console.WriteLine("***keyisdown1 called");
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
                pacman.Image = Properties.Resources.PACMANnleft1;

            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;

                pacman.Image = Properties.Resources.PACMANleft1;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = true;
                pacman.Image = Properties.Resources.MrPacmanUp;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = true;
                pacman.Image = Properties.Resources.PACMANdown1;
            }
        }

        private void keyisup1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goup = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                godown = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //showing the score on the board
            label1.Text = "Score:" + score;

            if (goleft)
            {
                //moving player to the left.
                pacman.Left -= speedlevel1;
            }
            if (goright)
            {
                //moving to the right
                pacman.Left += speedlevel1;
            }
            if (goup)
            {
                //moving to the top
                pacman.Top -= speedlevel1;
            }
            if (godown)
            {
                //moving down
                pacman.Top += speedlevel1;
            }
            //player movements code end

            //moving ghosts and bumping with the walls
            redGhost.Left += ghost1;
            yellowGhost.Left += ghost2;

            //if the red ghost hits the picture box 4 then we reverse the speed
            if (redGhost.Bounds.IntersectsWith(pictureBox1.Bounds))
            {
                ghost1 = -ghost1;
            }
            //if the red ghost hits the picture box 3 then we reverse the speed
            else if (redGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
            {
                ghost1 = -ghost1;
            }
            //if the yellow ghost hits the picture box 1 then we reverse the speed
            if (yellowGhost.Bounds.IntersectsWith(pictureBox3.Bounds))
            {
                ghost2 = -ghost2;
            }
            //if the yellow ghost hits the picture box 2 then we reverse the speed
            if (yellowGhost.Bounds.IntersectsWith(pictureBox4.Bounds))
            {
                ghost2 = -ghost2;
            }
            //moving ghosts and bumping with the walls end

            //for loop to check walls, ghost and points
            foreach (Control x in this.Controls)
            {
                // check if x is a PictureBox
                if (x is PictureBox)
                {
                    // get the tag for x as a string
                    string aTag = (string)x.Tag;

                    // if the tag is a wall or a ghost then..
                    if (aTag == "wall" || aTag == "ghost")
                    {
                        // get x as a PictureBox
                        PictureBox aPictureBox = (PictureBox)x;

                        // if pacman collides with a wall or a ghost or 
                        if (aPictureBox.Bounds.IntersectsWith(pacman.Bounds) == true)
                        {
                            pacman.Left = 0;
                            pacman.Top = 25;
                            label2.Text = "Game Over";
                            label2.Visible = true;
                            timer1.Stop();

                            using (var form1 = new frmGameOver())
                            {
                                Visible = false;
                                form1.ShowDialog();
                                Close();
                            }
                        }
                    }
                    // if the tag is a coin
                    else if (aTag == "coin")
                    {
                        // get x as a PictureBox
                        PictureBox aPictureBox = (PictureBox)x;

                        // if pacman collided with a coin, then...
                        if (aPictureBox.Bounds.IntersectsWith(pacman.Bounds) == true)
                        {
                            //SoundPlayer

                            //remove the coin
                            Controls.Remove(x);

                            //add to the score 
                            score++;

                            if (score == 30)
                            {
                                label2.Text = "You Win";
                                label2.Visible = true;
                                timer1.Stop();

                                using (var form1 = new frmGameOver())
                                {
                                    Visible = false;
                                    form1.ShowDialog();
                                    Close();
                                }
                            }
                        }
                    }

                }
                //creating boundaries
                if (x is PictureBox && x.Tag == "wallRight")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        pacman.Left -= speedlevel1;
                    }
                }
                if (x is PictureBox && x.Tag == "wallLeft")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        pacman.Left += speedlevel1;
                    }
                }
                if (x is PictureBox && x.Tag == "wallDown")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        pacman.Top -= speedlevel1;
                    }
                }
                if (x is PictureBox && x.Tag == "wallUp")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(pacman.Bounds))
                    {
                        pacman.Top += speedlevel1;
                    }
                }

            }

            //end of the loop checking walls, points and ghosts

            //ghost 3 going crazy here 
            pinkGhost.Left += ghost3x;
            pinkGhost.Top += ghost3y;

            if (pinkGhost.Left < 1 ||
                pinkGhost.Left + pinkGhost.Width > ClientSize.Width - 2 ||
                    (pinkGhost.Bounds.IntersectsWith(pictureBox4.Bounds)) ||
                    (pinkGhost.Bounds.IntersectsWith(pictureBox3.Bounds)) ||
                    (pinkGhost.Bounds.IntersectsWith(pictureBox1.Bounds)) ||
                    (pinkGhost.Bounds.IntersectsWith(pictureBox2.Bounds))
                    )
            {
                ghost3x = -ghost3x;
            }
            if (pinkGhost.Top < 1 || pinkGhost.Top + pinkGhost.Height > ClientSize.Height - 2)
            {
                ghost3y = -ghost3y;
            }
            //end of the crazy ghost movement
        }
        
        
    }
}
    

