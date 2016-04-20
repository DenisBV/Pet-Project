using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Tanks
{
    partial class View : UserControl
    {
        Model model;

        public View(Model model)
        {
            InitializeComponent();

            this.model = model;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e);
        }


        private void Draw(PaintEventArgs e)
        {
            DrawWall(e);
            DrawApple(e);
            DrawTank(e);
            DrawMyTank(e);
            DrawBullet(e);

            if (model.gameStatus != GameStatus.playing)
                return;

            Thread.Sleep(model.speedGame);
            Invalidate();
        }
        private void DrawBullet(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.Bullet.Img, new Point(model.Bullet.X, model.Bullet.Y));
        }
        private void DrawMyTank(PaintEventArgs e)
        {
            e.Graphics.DrawImage(model.MyTank.Img, new Point(model.MyTank.X, model.MyTank.Y));
        }
        private void DrawApple(PaintEventArgs e)
        {
            for (int i = 0; i < model.Apples.Count; i++)
                e.Graphics.DrawImage(model.Apples[i].Img, new Point(model.Apples[i].X, model.Apples[i].Y));
        }
        private void DrawTank(PaintEventArgs e)
        {
            for (int i = 0; i < model.Tanks.Count; i++)
                e.Graphics.DrawImage(model.Tanks[i].Img, new Point(model.Tanks[i].X, model.Tanks[i].Y));
        }
        private void DrawWall(PaintEventArgs e)
        {
            for (int y = 20; y < 260; y += 40)
                for (int x = 20; x < 260; x += 40)
                    e.Graphics.DrawImage(model.Wall.Img, new Point(x, y));
        }
    }
}
