using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class TowerBuilder
    {
        private GameObject go;
        /// <summary>
        /// Constructs a new tower
        /// </summary>
        /// <param name="position">postion</param>
        /// <param name="type">type of tower</param>
        public void Build(Vector2 position, TowerType type, Vehicle vehicle, Alignment alignment)
        {
            go = new GameObject();
            go.Transform.Position = position;
            go.AddComponent(new Collider(go, alignment));


            switch (type)
            {
                case TowerType.BasicTower:

                    go.AddComponent(new SpriteRenderer(go, Constant.basicTowerSpriteSheet, 0.9f));
                    go.AddComponent(new BasicTower(go));
                    vehicle.Stats.BasicTowerBuilt++;
                    break;

                case TowerType.ShotgunTower:
                    vehicle.Stats.ShotgunTowerbuilt++;
                    go.AddComponent(new SpriteRenderer(go, Constant.ShotgunTowerSpriteSheet, 0.9f));
                    go.AddComponent(new ShotgunTower(go));
                    break;

                case TowerType.SniperTower:
                    vehicle.Stats.SniperTowerBuilt++;
                    go.AddComponent(new SpriteRenderer(go, Constant.sniperTowerSpriteSheet, 0.9f));
                    go.AddComponent(new SniperTower(go));
                    break;
                case TowerType.MachineGunTower:
                    vehicle.Stats.MachinegunTowerbuilt++;
                    go.AddComponent(new SpriteRenderer(go, Constant.machineGunTowerSpriteSheet, 0.9f));
                    go.AddComponent(new MachineGunTower(go));
                    break;
                default:
                    break;
            }
            go.AddComponent(new Animator(go));
        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }


}
