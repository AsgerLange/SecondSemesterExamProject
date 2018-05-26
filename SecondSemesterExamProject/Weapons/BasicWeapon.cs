using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    class BasicWeapon : Weapon
    {

        public BasicWeapon(GameObject go) : base(go)
        {
            this.FireRate = Constant.basicWeaponFireRate;
            this.Ammo = Constant.basicWeaponAmmo;
            this.bulletType = Constant.basicWeaponBulletType;
        }
        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
            base.Shoot(vector2, alignment, rotation);
        }
    }
}
