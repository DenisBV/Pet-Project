using System;
using System.Drawing;

namespace Tanks
{
    class MyTank : IRun, ITurn, ITransparent
    {
        MyTankImg pacmanImg = new MyTankImg();
        Image img;

        int x, y, direct_x, direct_y, nextDirect_x, nextDirect_y;
        int sizeField;

        public int NextDirect_y
        {
            get { return nextDirect_y; }
            set
            {
                if (value == 0 || value == -1 || value == 1)
                    nextDirect_y = value;
                else
                    nextDirect_y = 0;
            }
        }
        public int NextDirect_x
        {
            get { return nextDirect_x; }
            set
            {
                if (value == 0 || value == -1 || value == 1)
                    nextDirect_x = value;
                else
                    nextDirect_x = 0;
            }
        }
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
        public int Y
        {
            get { return y; }
        }
        public int X
        {
            get { return x; }
        }
        public Image Img
        {
            get { return img; }
        }


        public MyTank(int sizeField)
        {
            this.sizeField = sizeField;
            x = 120;
            y = 240;
            Direct_x = 0;
            Direct_y = -1;
            NextDirect_x = 0;
            NextDirect_y = -1;

            PutImg();
        }
        public void Run()
        {
            x += direct_x;
            y += direct_y;
            if (Math.IEEERemainder(x, 40) == 0 && Math.IEEERemainder(y, 40) == 0)
                Turn();

            Transparent();
        }
        public void Turn()
        {
            Direct_x = NextDirect_x;
            Direct_y = NextDirect_y;

            PutImg();
        }
        public void Transparent()
        {
            if (x == -1)
                x = sizeField - 21;
            if (x == sizeField - 19)
                x = 1;

            if (y == -1)
                y = sizeField - 21;
            if (y == sizeField - 19)
                y = 1;
        }


        private void PutImg()
        {
            if (direct_x == 1)
                img = pacmanImg.Right;
            if (direct_x == -1)
                img = pacmanImg.Left;
            if (direct_y == 1)
                img = pacmanImg.Down;
            if (direct_y == -1)
                img = pacmanImg.Up;
        }
    }
}
