using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    class Shotgun : Weapon
    {
        public Shotgun(GameObject go) : base(go)
        {
            this.Ammo = Constant.shotGunAmmo;
            this.fireRate = Constant.shotGunFireRate;
            this.bulletType = Constant.shotgunBulletType;
        }

        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
            for (int i = 0; i < 12; i++)
            {
                BulletPool.CreateBullet(vector2, alignment, bulletType, rotation + (GameWorld.Instance.Rnd.Next(-15, 15)));
            }
            Ammo--;
        }
    }
}
