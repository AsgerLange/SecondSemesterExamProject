using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Weapon
    {
        protected float fireRate;

        protected int ammo;

        protected BulletType bulletType;

        protected GameObject go;

        public float FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }

        public int Ammo 
        {
            get { return ammo; }
            set { ammo= value; }
        }
        public Weapon(GameObject go)
        {
            this.go = go;
        }
        public virtual void Shoot(Vector2 vector2, Alignment alignment
                       , float rotation)
        {

            BulletPool.CreateBullet(vector2, alignment,
                       bulletType, rotation);
        }

    }
}
