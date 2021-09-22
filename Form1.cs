using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dino_Game
{
    public partial class Form1 : Form
    {

        bool jumping = false;
        int jumpSpeed;
        int force = 12; //force = the limit of how far the player can jumo
        int score = 0;
        int obstacleSpeed = 10;
        Random rand = new Random();
        int position;
        bool isGameOver = false;

        public Form1()
        {
            InitializeComponent();

            gameReset();
        }

        private void MainGameTimer(object sender, EventArgs e)
        {

            dino.Top += jumpSpeed;

            txtScore.Text = "Score: " + score;

            if (jumping == true && force < 0)
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

            if (dino.Top > 365 && jumping == false)
            {
                force = 12;
                dino.Top = 366;
                jumpSpeed = 0;
            }


            //obstacle:
            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    //respawning
                    if (x.Left < -100)
                    {
                        x.Left = this.ClientSize.Width + rand.Next(200, 500) + (x.Width * 15);
                        score++;
                    }

                    //if dino hits the obstacles
                    if (dino.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop(); //game has ended
                        dino.Image = Properties.Resources.dead; //uses the dead image of dino
                        txtScore.Text += ". Press R to restart the game!";
                        isGameOver = true;
                    }
                }
            }


            if (score > 2)
            {
                obstacleSpeed = 15;
            }



        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //if space key is pressed; j - false to jump once
            if (e.KeyCode == Keys.Space && jumping == false)
            {
                jumping = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //if space is not pressed anymore, stop jumping
            if (jumping == true)
            {
                jumping = false;
            }

            // if R is pressed, reset the game
            if (e.KeyCode == Keys.R && isGameOver == true)
            {
                gameReset();
            }
        }


        private void gameReset()
        {
            force = 12;
            jumpSpeed = 0;
            jumping = false;
            score = 0;
            obstacleSpeed = 10;
            txtScore.Text = "Score: " + score;
            dino.Image = Properties.Resources.running;
            isGameOver = false;
            dino.Top = 366; //starting position of dino

            foreach (Control x in Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle") //if x is a type of PictureBox and he has the tag of obstacle
                {
                    position = this.ClientSize.Width + rand.Next(500, 800) + (x.Width * 10) ;

                    x.Left = position;

                }
            }


            gameTimer.Start();

            
        }

    }
}
