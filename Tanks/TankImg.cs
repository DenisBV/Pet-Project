using System.Drawing;

namespace Tanks
{
    class TankImg
    {
        Image up = Properties.Resources.Tank0_1;
        Image down = Properties.Resources.Tank01;
        Image right = Properties.Resources.Tank10;
        Image left = Properties.Resources.Tank_10;

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
