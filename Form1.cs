using Super_Maroi_Game.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Super_Maroi_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        bool goLeft, goRight, jumping, hasKey;

        int jumpSpeed = 10;
        int force = 8;
        byte score = 0;

        int playerSpeed = 10;
        int backgroundSpeed = 8;


        private void Form1_Load(object sender, EventArgs e)
        {
         
            pictureBox34.Parent = Background;
          //  lbScore.Parent = Background;

          //  MakeCoinsNice();
        }

        private void MakeCoinsNice()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    x.Parent = Background;
                }
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            lbScore.Text = "Score: " + score;

            // linking the jumpspeed integer with the player picture boxes to location
            Player.Top += jumpSpeed;

            // refresh the player picture box consistently
            Player.Refresh();

            // if jumping is true and force is less than 0
            // then change jumping to false
            if (jumping && force < 0)
            {
                jumping = false;
            }


            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }


            if (goLeft == true && Player.Left > 60)
            {
                Player.Left -= playerSpeed;
            }

            if(goRight ==  true && Player.Left + ( Player.Width + 60) < this.ClientSize.Width)
            {
                Player.Left += playerSpeed;
            }

            if(goLeft == true && Background.Left < 0)
            {
                Background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }

            if(goRight == true && Background.Left > -1595)
            {
                Background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }


            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "Platform")
                {

                    if(Player.Bounds.IntersectsWith(x.Bounds) && jumping == false)
                    {
                        force = 8;
                        Player.Top = x.Top - Player.Height;
                        jumpSpeed = 0;
                    }

                    x.BringToFront();

                }

                if (x is PictureBox && (string)x.Tag == "coin")
                {
                    if (Player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                    }

                }

                //if (x is PictureBox && (string)x.Tag == "Gold")
                //{
                //    if (Player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                //    {
                //        x.Visible = false;
                //        score += 100;
                //    }

                //}
            }


            if (Player.Bounds.IntersectsWith(Key.Bounds))
            {
                Key.Visible = false;
                hasKey = true;
            }

            if(Player.Bounds.IntersectsWith(door.Bounds) && hasKey)
            {
                door.Image = Resources.door_open;
                GameTimer.Stop();
                MessageBox.Show("Well Done, Your Journey is Complete :-)" + Environment.NewLine + "Click Ok To Play Again !!");
                RestartGame();
            }

            if(Player.Top + Player.Height > this.ClientSize.Height)
            {
                GameTimer.Stop();
                MessageBox.Show("You Died! :-(" + Environment.NewLine + "Click Ok To Play Again !!");
                RestartGame();
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

            if(e.KeyCode == Keys.Space && !jumping)
            {
                jumping = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }

            if (jumping == true)
            {
                jumping = false;
            }
        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }

        private void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag == "Platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "Key" || x is PictureBox && (string)x.Tag == "door")
                {
                    if(direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }

                    if(direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }
                }
            }

        }


    }
}
