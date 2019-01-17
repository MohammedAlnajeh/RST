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
    public partial class frmGameOver : Form
    {
        //sound player
        private SoundPlayer youlosesoundPlayer = new SoundPlayer();
        public frmGameOver()
        {
            InitializeComponent();
            youlosesoundPlayer = new SoundPlayer("youlose.wav");
            youlosesoundPlayer.Play();

        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            using (var frmGameOver = new frmpacmanCPT())
            {
                Visible = false;
                frmGameOver.ShowDialog();
                Close();
            }
        }

        private void btnNextLevel_Click(object sender, EventArgs e)
        {
            using (var frmGameOver = new frmGameOverLevel2())
            {
                Visible = false;
                frmGameOver.ShowDialog();
                Close();
            }
        }
    }
}
