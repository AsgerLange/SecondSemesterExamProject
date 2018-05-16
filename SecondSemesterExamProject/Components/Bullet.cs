using Microsoft.Xna.Framework;
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
        private BulletType bulletType;
        private Vector2 direction;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;

        public Bullet(GameObject gameObject, BulletType type) : base(gameObject)
        {
            canRelease = true;
            this.bulletType = type;
            this.direction= new Vector2(0, 0);
        }
    }
}
