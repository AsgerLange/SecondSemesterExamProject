using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class TowerPlacer
    {
        private int towerAmount; //amount of towers

        private TowerType towerType; //the type of tower

        private int towerBuildCost; //price of the tower

        private Vehicle vehicle; //game object that ownes the tower

        public TowerType GetTowerType
        {
            get { return towerType; }
        }

        /// <summary>
        /// property for amount of towers, if reaches zero, the vehicle get the basic tower
        /// </summary>
        public int TowerAmount
        {
            get { return towerAmount; }
            set
            {
                towerAmount = value;
                if (towerAmount <= 0)
                {
                    SwitchBackToBasicTower();
                }
            }
        }
        /// <summary>
        /// constructor for a towerplacer
        /// </summary>
        /// <param name="go">owner of the towerplacer</param>
        /// <param name="type">type of tower</param>
        /// <param name="amount">amount of towers</param>
        public TowerPlacer(Vehicle go, TowerType type, int amount)
        {
            this.vehicle = go;
            this.towerType = type;
            this.towerAmount = amount;
            SetTowerBuildCost();

        }
        /// <summary>
        /// handles placing towers
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="alignment"></param>
        /// <param name="rotation"></param>
        public void PlaceTower()
        {

            if (vehicle.Money >= towerBuildCost)
            {
                GameObject towerGO;

                //Gameobjectdirector builds a new tower
                towerGO = GameObjectDirector.Instance.Construct(new Vector2(vehicle.GameObject.Transform.Position.X + 1,
                    vehicle.GameObject.Transform.Position.Y + 1), towerType);

                //its content is loaded
                towerGO.LoadContent(GameWorld.Instance.Content);

                //it's added to gameworld next update cycle
                GameWorld.Instance.GameObjectsToAdd.Add(towerGO);

                //pays for the tower
                vehicle.Money -= towerBuildCost;


                TowerAmount--;

            }



        }

        /// <summary>
        /// sets the price for building a tower
        /// </summary>
        private void SetTowerBuildCost()
        {
            switch (towerType)
            {
                case TowerType.BasicTower:
                    towerBuildCost = Constant.basicTowerPrice;
                    break;
                case TowerType.ShotgunTower:
                    towerBuildCost = Constant.shotgunTowerPrice;
                    break;
                case TowerType.MachineGunTower:
                    towerBuildCost = Constant.machineGunTowerPrice;
                    break;
                case TowerType.SniperTower:
                    towerBuildCost = Constant.sniperTowerPrice;
                    break;
                default:
                    towerBuildCost = Constant.basicTowerPrice;
                    break;
            }
        }

        /// <summary>
        /// Gives the vehicle the basic tower
        /// </summary>
        private void SwitchBackToBasicTower()
        {
            vehicle.TowerPlacer = new TowerPlacer(vehicle, TowerType.BasicTower, int.MaxValue);
        }

        public override string ToString()
        {
            if (towerAmount > 1000)
            {
                return towerType.ToString() + ": LOTS!";
            }
            else
            {

                return towerType.ToString() + ": " + towerAmount.ToString() + " ($" + towerBuildCost + ")";
            }
        }
    }
}
