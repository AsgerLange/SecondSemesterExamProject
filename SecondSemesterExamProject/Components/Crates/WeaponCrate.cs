using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class WeaponCrate : Crate
    {
        private WeaponType weaponType;
        
        /// <summary>
        /// constructor for weaponCreate, chooses a random weapontype to give to the player
        /// </summary>
        /// <param name="gameObject"></param>
        public WeaponCrate(GameObject gameObject) : base(gameObject)
        {
            int random = GameWorld.Instance.Rnd.Next(1,(Enum.GetNames(typeof(WeaponType)).Length));

            weaponType = (WeaponType)random;

        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        public override string ToString()
        {
            return "New " + weaponType.ToString();
        }

        protected override void CreateAnimation()
        {
            base.CreateAnimation();
        }

        protected override void Die()
        {
            base.Die();
        }
        /// <summary>
        /// Gives the player a new weapon
        /// </summary>
        /// <param name="vehicle"></param>
        protected override void GiveLoot(Vehicle vehicle)
        {
            base.GiveLoot(vehicle);
            switch (weaponType)
            {
                
                case WeaponType.MachineGun:
                    vehicle.Weapon = new MachineGun(vehicle.GameObject);
                    break;
                case WeaponType.Shotgun:
                    vehicle.Weapon = new Shotgun(vehicle.GameObject);

                    break;
                case WeaponType.Sniper:
                    vehicle.Weapon = new Sniper(vehicle.GameObject);

                    break;
                    
                default:
                    vehicle.Weapon = new BasicWeapon(vehicle.GameObject);
                    System.Diagnostics.Debug.WriteLine("error in weapon crate GiveLoot");
                    break;
            }
            vehicle.LatestLootCrate = this;
        }
        
    }
}
