using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    class Sniper : Weapon
    {
        public Sniper(GameObject go) : base(go)
        {
            this.Ammo = Constant.sniperAmmo;
            this.fireRate = Constant.sniperFireRate;
            this.bulletType = Constant.sniperBulletType;
            this.weaponSpread = Constant.sniperSpread;
        }

        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
            base.Shoot(vector2, alignment, rotation);
        }

    }
}
