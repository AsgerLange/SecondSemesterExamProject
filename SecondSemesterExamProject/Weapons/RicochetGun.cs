using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    class RicochetGun : Weapon
    {
        public RicochetGun(GameObject go) : base(go)
        {
            this.Ammo = Constant.RichochetGunAmmo;
            this.fireRate = Constant.RichochetGunFireRate;
            this.bulletType = Constant.RichochetGunBulletType;
            this.weaponSpread = Constant.RichochetGunSpread;
        }

        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
            base.Shoot(vector2, alignment, rotation);
        }

    }
}
