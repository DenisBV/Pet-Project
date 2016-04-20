using System.Drawing;

namespace Tanks
{
    class BulletImg
    {
        Image up = Properties.Resources.bulletUp2;
        Image down = Properties.Resources.bulletDown2;
        Image right = Properties.Resources.bulletRight2;
        Image left = Properties.Resources.bulletLeft2;

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
