using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MachineGun : Weapon
    {
        public MachineGun(GameObject go) : base(go)
        {
            this.Ammo = Constant.MachineGunGunAmmo;
            this.fireRate = Constant.MachineGunFireRate;
            this.bulletType = Constant.MachineGunBulletType;
        }

        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
           
                BulletPool.CreateBullet(vector2, alignment, bulletType, rotation + (GameWorld.Instance.Rnd.Next(-7, 7)));
            
            Ammo--;
        }
    }
}
