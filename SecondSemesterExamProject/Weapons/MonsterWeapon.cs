using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MonsterWeapon : Weapon
    {
        /// <summary>
        /// Constructor for basic weapon
        /// </summary>
        /// <param name="go">The gameobject who has the weapon</param>
        public MonsterWeapon(GameObject go) : base(go)
        {
            this.FireRate = Constant.monsterWeaponFireRate;
            this.Ammo = Constant.monsterWeaponAmmo;
            this.bulletType = Constant.monsterWeaponBulletType;
            this.weaponSpread = Constant.monsterWeaponSpread;
        }
        /// <summary>
        /// handles shooting for basic weapon
        /// </summary>
        /// <param name="vector2">position</param>
        /// <param name="alignment">alignment of the bullet</param>
        /// <param name="rotation">rotation of the vehicle that shot the bullet</param>
        public override void Shoot(Alignment alignment, float rotation)
        {
            
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
                return "ShockWave: LOTS!";
            }
            else
            {

                return "ShockWave: " + ammo.ToString();
            }
        }
        /// <summary>
        /// loads content for basic weapon
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            shootSoundEffect = content.Load<SoundEffect>("MonsterSoundEffect");

        }

        /// <summary>
        /// Plays sound effect for weapons's shooting ability
        /// </summary>
        protected override void PlayShootSoundEffect()
        {
            if (vehicle.Control == Controls.WASD)
            {

                shootSoundEffect.Play(0.7f, -0.3f, 0); //Plays shooting soundeffect
            }
            else
            {
                shootSoundEffect.Play(0.7f, -0.5f, 0); //Plays shooting soundeffect

            }
        }
    }
}
