﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    /// <summary>
    /// The basic weapon
    /// </summary>
    class BasicWeapon : Weapon
    {
        /// <summary>
        /// Constructor for basic weapon
        /// </summary>
        /// <param name="go">The gameobject who has the weapon</param>
        public BasicWeapon(GameObject go) : base(go)
        {
            this.FireRate = Constant.basicWeaponFireRate;
            this.Ammo = Constant.basicWeaponAmmo;
            this.bulletType = Constant.basicWeaponBulletType;
            this.weaponSpread = Constant.basicWeaponSpread;
        }
        /// <summary>
        /// handles shooting for basic weapon
        /// </summary>
        /// <param name="vector2">position</param>
        /// <param name="alignment">alignment of the bullet</param>
        /// <param name="rotation">rotation of the vehicle that shot the bullet</param>
        public override void Shoot(Alignment alignment, float rotation)
        {
            vehicle.Stats.BasicWeaponFired++;
            base.Shoot(alignment, rotation);

        }
        public override string ToString()
        {
            if (ammo > 1000)
            {
                return "Basic Weapon: LOTS!";
            }
            else
            {

                return "Basic Weapon: " + ammo.ToString();
            }
        }
        public override void LoadContent(ContentManager content)
        {
            shootSoundEffect = content.Load<SoundEffect>("BasicWeaponShot");

        }

        /// <summary>
        /// Plays sound effect for weapons's shooting ability
        /// </summary>
        protected override void PlayShootSoundEffect()
        {
            shootSoundEffect.Play(1, 0, 0); //Plays shooting soundeffect
        }
    }
}
