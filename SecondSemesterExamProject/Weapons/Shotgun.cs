using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class Shotgun : Weapon
    {
        /// <summary>
        /// constructor for Shotgun
        /// </summary>
        /// <param name="go">The vehicle that owns the shotgun</param>
        public Shotgun(GameObject go) : base(go)
        {
            this.Ammo = Constant.shotGunAmmo;
            this.fireRate = Constant.shotGunFireRate;
            this.bulletType = Constant.shotgunBulletType;
            this.weaponSpread = Constant.shotGunSpread;
        }

        /// <summary>
        /// Shoots an amount of  pellets, with a wide spread
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="alignment"></param>
        /// <param name="rotation"></param>
        public override void Shoot( Alignment alignment, float rotation)
        {
            PlayShootSoundEffect();
            for (int i = 0; i < Constant.shotgunPelletAmount; i++)
            {
                BulletPool.CreateBullet(go, alignment, bulletType, rotation + (GameWorld.Instance.Rnd.Next(-weaponSpread, weaponSpread)));
            }
            Ammo--;
           vehicle. Stats.ShotgunFired++;
        }
        public override string ToString()
        {
            if (ammo > 1000)
            {
                return "Shotgun: LOTS!";
            }
            else
            {
                return "Shotgun: " + ammo.ToString();
            }
        }
        public override void LoadContent(ContentManager content)
        {
            shootSoundEffect = content.Load<SoundEffect>("ShotgunShot");

        }
    }
}
