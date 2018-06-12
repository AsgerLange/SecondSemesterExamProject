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

    class Weapon : ILoadable
    {
        protected float fireRate; //rate of fire

        protected int ammo; //amount of ammo

        protected BulletType bulletType; //the type of bullet

        protected int weaponSpread; //the bullet spread of the weapon

        protected Vehicle vehicle;

        protected GameObject go; //game object that owne the weapon

        protected SoundEffect shootSoundEffect;


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
                    vehicle.GetBasicGun();
                }
            }
        }
        /// <summary>
        /// Base constructor for any weapon
        /// </summary>
        /// <param name="go"></param>
        public Weapon(GameObject go)
        {
            LoadContent(GameWorld.Instance.Content);
            this.go = go;
            if (go != null)
            {
                foreach (Component comp in go.GetComponentList)
                {
                    if (comp is Vehicle)
                    {
                        this.vehicle = (comp as Vehicle);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// handles shooting
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="alignment"></param>
        /// <param name="rotation"></param>
        public virtual void Shoot(Alignment alignment, float rotation)
        {

            PlayShootSoundEffect();
            BulletPool.CreateBullet(go, alignment,
                       bulletType, rotation + (GameWorld.Instance.Rnd.Next(-weaponSpread, weaponSpread)));
            Ammo--;

        }

        /// <summary>
        /// Gives the vehicle the basic weapon
        /// </summary>
        protected void SwitchBackToBasicWeapon()
        {
            vehicle.Weapon = new BasicWeapon(go.GameObject);
        }

        /// <summary>
        /// Plays sound effect for weapons's shooting ability
        /// </summary>
        protected virtual void PlayShootSoundEffect()
        {
            shootSoundEffect.Play(1f, 0, 0); //Plays shooting soundeffect

        }

        /// <summary>
        /// base 
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            shootSoundEffect = content.Load<SoundEffect>("BasicWeaponShot");
        }
    }

}

