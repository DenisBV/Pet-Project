using System;
using System.Threading;
using System.Windows.Forms;


[assembly: CLSCompliant(true)]
namespace Tanks
{
    public partial class ControllerMainForm : Form
    {
        View view;
        Model model;
        Thread modelplay;
        string message = @"
            Игра 'Танчики' версия 1.0.

             Для управления танчиком
   используйте клавиши 'W', 'A', 'S', 'D', 'K'";


        public ControllerMainForm(int sizeField = 260, int amountTanks = 7, int amountApples = 5, int speedGame = 40)
        {
            InitializeComponent();
            model = new Model(sizeField, amountTanks, amountApples, speedGame);

            model.changeStreep += ChangeStatusStripLabel;
            view = new View(model);
            this.Controls.Add(view);
        }
        
        private void StartPause_btn_Click(object sender, EventArgs e)
        {
            if (model.gameStatus == GameStatus.playing)
            {
                modelplay.Abort();
                model.gameStatus = GameStatus.stoping;
                ChangeStatusStripLabel();
            }
            else
            {
                StartPause_picBox.Focus();
                model.gameStatus = GameStatus.playing;
                modelplay = new Thread(model.Play);
                modelplay.Start();
                ChangeStatusStripLabel();
                view.Invalidate();
            }

        }

        private void Controller_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modelplay != null)
            {
                model.gameStatus = GameStatus.stoping;
                modelplay.Abort();
            }
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите выйти?", "Танки", 
                                                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk, 
                                                MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
            if (dr == DialogResult.Yes)
                e.Cancel = false;
            else
                e.Cancel = true;
        }
        
        private void StartPause_picBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData.ToString())
            {
                case "A":
                    {
                        model.MyTank.NextDirect_x = -1;
                        model.MyTank.NextDirect_y = 0;
                    }
                    break;
                case "D":
                    {
                        model.MyTank.NextDirect_x = 1;
                        model.MyTank.NextDirect_y = 0;
                    }
                    break;
                case "W":
                    {
                        model.MyTank.NextDirect_x = 0;
                        model.MyTank.NextDirect_y = -1;
                    }
                    break;
                case "S":
                    {
                        model.MyTank.NextDirect_x = 0;
                        model.MyTank.NextDirect_y = 1;
                    }
                    break;

                case "K":
                    {
                        SetStartCoordinatesForBullet();
                    }
                    break;
            }
        }

        private void SetStartCoordinatesForBullet()
        {
            model.Bullet.Direct_x = model.MyTank.Direct_x;
            model.Bullet.Direct_y = model.MyTank.Direct_y;

            if (model.MyTank.Direct_y == -1)
            {
                model.Bullet.X = model.MyTank.X + 7;
                model.Bullet.Y = model.MyTank.Y;
            }
            if (model.MyTank.Direct_y == 1)
            {
                model.Bullet.X = model.MyTank.X + 7;
                model.Bullet.Y = model.MyTank.Y + 17;
            }
            if (model.MyTank.Direct_x == -1)
            {
                model.Bullet.X = model.MyTank.X;
                model.Bullet.Y = model.MyTank.Y + 7;
            }
            if (model.MyTank.Direct_x == 1)
            {
                model.Bullet.X = model.MyTank.X + 17;
                model.Bullet.Y = model.MyTank.Y + 7;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.NewGame();
            view.Refresh();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(message, "Танки", MessageBoxButtons.OK, 
                            MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void ChangeStatusStripLabel()
        {
            toolStripStatusLabel1.Text = model.gameStatus.ToString();
        }
    }
}
