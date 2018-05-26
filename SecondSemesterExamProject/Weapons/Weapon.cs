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
            set
            {
                ammo = value;
                if (ammo <= 0)
                {
                    SwitchBackToBasicWeapon();
                }
            }
        }
        public Weapon(GameObject go)
        {
            this.go = go;
        }
        public virtual void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {

            BulletPool.CreateBullet(vector2, alignment,
                       bulletType, rotation);
            Ammo--;

        }
        protected void SwitchBackToBasicWeapon()
        {
            foreach (Component comp in go.GetComponentList)
            {
                if (comp is Vehicle)
                {
                    (comp as Vehicle).Weapon = new BasicWeapon(go.GameObject);
                }
            }
        }

    }
}
