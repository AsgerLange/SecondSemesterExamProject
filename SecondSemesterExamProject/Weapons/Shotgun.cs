﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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
        public override void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {
            for (int i = 0; i < Constant.shotgunPelletAmount; i++)
            {
                BulletPool.CreateBullet(vector2, alignment, bulletType, rotation + (GameWorld.Instance.Rnd.Next(-weaponSpread, weaponSpread)));
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
    }
}
