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
        /// <summary>
        /// constructor for machinegun
        /// </summary>
        /// <param name="go">the vehicle that owns the weapon</param>
        public MachineGun(GameObject go) : base(go)
        {
            this.Ammo = Constant.MachineGunGunAmmo;
            this.fireRate = Constant.MachineGunFireRate;
            this.bulletType = Constant.MachineGunBulletType;
            this.weaponSpread = Constant.MachineGunSpread;
        }
        /// <summary>
        /// Handles shooting for Machingun
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="alignment"></param>
        /// <param name="rotation"></param>
        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
            base.Shoot(vector2,alignment,rotation);
        }
        public override string ToString()
        {
            if (ammo > 1000)
            {
                return "Machinegun: LOTS!";
            }
            else
            {

                return "Machinegun: " + ammo.ToString();
            }
        }
    }
}
