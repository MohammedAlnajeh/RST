﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace pacmanCPT
{
    public partial class introGame : Form
    {
        //sound player
        private SoundPlayer bgsoundPlayer = new SoundPlayer();

        public introGame()
        {
            InitializeComponent();
            bgsoundPlayer = new SoundPlayer("pacman_beginning.wav");
            bgsoundPlayer.Play();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //loading bar increment
            prgst.Increment(1);
            //this.Refresh();

            if (prgst.Value == 100)
                timer1.Stop();
        }
    }
}
