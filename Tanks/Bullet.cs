using System.Drawing;

namespace Tanks
{
    class Bullet
    {
        private BulletImg bulletImg = new BulletImg();
        private Image img;
        private int km;

        int x, y, direct_x, direct_y;

        public int Direct_x
        {
            get { return direct_x; }
            set
            {
                if (value == 0 || value == -1 || value == 1)
                    direct_x = value;
                else
                    direct_x = 0;
            }
        }
        public int Direct_y
        {
            get { return direct_y; }
            set
            {
                if (value == 0 || value == -1 || value == 1)
                    direct_y = value;
                else
                    direct_y = 0;
            }
        }
        public Image Img
        {
            get { return img; }
            set { img = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }


        public Bullet()
        {
            img = bulletImg.Up;
            DefaultSettings();
        }
        public void DefaultSettings()
        {
            x = y = -10;
            Direct_x = Direct_y = 0;
            km = 0;
        }
        public void Run()
        {
            if (Direct_x == 0 && Direct_y == 0)
                return;
            km += 3;
            PutImg();
            x += Direct_x * 3;
            y += Direct_y * 3;
            if (km > 140)
                DefaultSettings();
        }


        protected void PutImg()
        {
            if (direct_x == 1)
                Img = bulletImg.Right;
            if (direct_x == -1)
                Img = bulletImg.Left;
            if (direct_y == 1)
                Img = bulletImg.Down;
            if (direct_y == -1)
                Img = bulletImg.Up;
        }
    }
}
