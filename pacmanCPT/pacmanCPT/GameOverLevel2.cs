using System;
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
    public partial class frmGameOverLevel2 : Form
    {
        //sound player
        private SoundPlayer GameOverSound = new SoundPlayer();

        public frmGameOverLevel2()
        {
            InitializeComponent();
            GameOverSound = new SoundPlayer("Game over.wav");
            GameOverSound.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
