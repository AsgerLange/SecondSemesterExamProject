using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class TowerCrate : Crate
    {
        private TowerType towerType;
        private int amount;


        public TowerCrate(GameObject gameObject) : base(gameObject)
        {
            int random = GameWorld.Instance.Rnd.Next(1, (Enum.GetNames(typeof(TowerType)).Length));

            towerType = (TowerType)random;
        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        public override string ToString()
        {
            return "+" + amount + " " + towerType.ToString();
        }

        protected override void CreateAnimation()
        {
            base.CreateAnimation();
        }

        protected override void Die()
        {
            base.Die();
        }

        protected override void GiveLoot(Vehicle vehicle)
        {
            switch (towerType)
            {

                case TowerType.ShotgunTower:
                    if (vehicle.TowerPlacer.GetTowerType == TowerType.ShotgunTower)
                    {
                        vehicle.TowerPlacer.TowerAmount += Constant.shotgunTowerAmount;
                    }
                    else
                    {
                        amount = Constant.shotgunTowerAmount;
                        vehicle.TowerPlacer = new TowerPlacer(vehicle, towerType, amount);
                    }
                    break;
                case TowerType.SniperTower:
                    if (vehicle.TowerPlacer.GetTowerType == TowerType.SniperTower)
                    {
                        vehicle.TowerPlacer.TowerAmount += Constant.shotgunTowerAmount;
                    }
                    else
                    {
                        amount = Constant.sniperTowerAmount;
                        vehicle.TowerPlacer = new TowerPlacer(vehicle, towerType, amount);
                    }
                    break;
                case TowerType.MachineGunTower:
                    if (vehicle.TowerPlacer.GetTowerType == TowerType.MachineGunTower)
                    {
                        vehicle.TowerPlacer.TowerAmount += Constant.machineGunTowerAmount;
                    }
                    else
                    {
                        amount = Constant.machineGunTowerAmount;
                        vehicle.TowerPlacer = new TowerPlacer(vehicle, towerType, amount);
                    }
                    break;
                default:
                    break;
            }
            vehicle.LatestLootCrate = this;
        }
    }
}
