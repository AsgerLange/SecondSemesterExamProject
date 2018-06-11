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
    class Sniper : Weapon
    {
        public Sniper(GameObject go) : base(go)
        {
            this.Ammo = Constant.sniperAmmo;
            this.fireRate = Constant.sniperFireRate;
            this.bulletType = Constant.sniperBulletType;
            this.weaponSpread = Constant.sniperSpread;
        }

        /// <summary>
        /// handles shooting for sniper weapon
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="rotation"></param>
        public override void Shoot(Alignment alignment, float rotation)
        {
            vehicle.Stats.SniperFired++;
            base.Shoot(alignment, rotation);
        }
        /// <summary>
        /// Returns name and amount of ammo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (ammo > 1000)
            {
                return "Sniper: LOTS!";
            }
            else
            {

                return "Sniper: " + ammo.ToString();
            }
        }
        /// <summary>
        /// loads content for sniper
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            shootSoundEffect = content.Load<SoundEffect>("SniperShot");

        }


        /// <summary>
        /// Plays sound effect for weapons's shooting ability
        /// </summary>
        protected override void PlayShootSoundEffect()
        {
            shootSoundEffect.Play(0.9f, 0f, 0); //Plays shooting soundeffect
        }
    }
}
