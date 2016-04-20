using System.Drawing;

namespace Tanks
{
    class MyTankImg
    {
        Image up = Properties.Resources.PacmanUp;
        Image down = Properties.Resources.PacmanDown;
        Image right = Properties.Resources.PacmanRight;
        Image left = Properties.Resources.PacmanLeft;

        public Image Right
        {
            get { return right; }
        }
        public Image Left
        {
            get { return left; }
        }
        public Image Up
        {
            get { return up; }
        }
        public Image Down
        {
            get { return down; }
        }
    }
}
