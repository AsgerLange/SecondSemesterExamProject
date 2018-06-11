using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
        public override void Shoot(Alignment alignment, float rotation)
        {
            vehicle.Stats.MachinegunFired++;
            base.Shoot(alignment,rotation);
        }
        /// <summary>
        /// Returns name and amount of ammo
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// loads content for machinegun
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            shootSoundEffect = content.Load<SoundEffect>("MachinegunShot2");

        }

        /// <summary>
        /// Plays sound effect for weapons's shooting ability
        /// </summary>
        protected override void PlayShootSoundEffect()
        {


            shootSoundEffect.Play(0.5f,0.5f,0); //Plays shooting soundeffect

        }
    }
}
