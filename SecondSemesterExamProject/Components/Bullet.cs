using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    enum BulletType {BaiscBullet };
    class Bullet : Component
    {
        #region Attributes for object pool
        private bool canRelease;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;
    }
}
