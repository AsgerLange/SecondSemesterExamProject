﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{

    class Weapon
    {
        protected float fireRate; //rate of fire

        protected int ammo; //amount of ammo

        protected BulletType bulletType; //the type of bullet

        protected int weaponSpread; //the bullet spread of the weapon

        protected Vehicle vehicle;

        protected GameObject go; //game object that owne the weapon

        public float FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }

        /// <summary>
        /// property for Ammo, if reaches zero, the vehicle get the basic weapon
        /// </summary>
        public int Ammo
        {
            get { return ammo; }
            set
            {
                ammo = value;
                if (ammo <= 0)
                {
                    SwitchBackToBasicWeapon();
                }
            }
        }
        public Weapon(GameObject go)
        {
            this.go = go;

            foreach (Component comp in go.GetComponentList)
            {
                if (comp is Vehicle)
                {
                    this.vehicle = (comp as Vehicle);
                    break;
                }
            }
        }
        /// <summary>
        /// handles shooting
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="alignment"></param>
        /// <param name="rotation"></param>
        public virtual void Shoot(Vector2 vector2, Alignment alignment, float rotation)
        {

            BulletPool.CreateBullet(vector2, alignment,
                       bulletType, rotation + (GameWorld.Instance.Rnd.Next(-weaponSpread, weaponSpread)));
            Ammo--;

        }

        /// <summary>
        /// Gives the vehicle the basic weapon
        /// </summary>
        protected void SwitchBackToBasicWeapon()
        {
            foreach (Component comp in go.GetComponentList)
            {
                if (comp is Vehicle)
                {
                    (comp as Vehicle).Weapon = new BasicWeapon(go.GameObject);
                }
            }
        }

    }
}
