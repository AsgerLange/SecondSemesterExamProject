using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class VehicleBuilder
    {
        GameObject go;

        /// <summary>
        /// The vehicleBuilder builds a vehicle
        /// </summary>
        /// <param name="type">type of vehicle</param>
        public void Build(VehicleType type, Controls controls, int playerNumber, Alignment alignment)
        {
            go = new GameObject();
            go.Transform.Position = new Vector2(Constant.width / 2 + 1, Constant.hight / 2); //spawns in the middle
                    go.AddComponent(new Collider(go, alignment));//adds collider


            switch (type)
            {
                case VehicleType.Tank:
                    go.AddComponent(new SpriteRenderer(go, Constant.tankSpriteSheet + playerNumber, 0.1f));//Sprite that fits player
                    go.AddComponent(new Animator(go));//allows go to be animated

                    //Standard tank setup
                    go.AddComponent(new Tank(go, controls, Constant.tankHealth, Constant.tankMoveSpeed
                       , Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower, playerNumber));
                    break;

                case VehicleType.Bike:
                    go.AddComponent(new SpriteRenderer(go, Constant.bikeSpriteSheet + playerNumber, 0.1f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new Bike(go, controls, Constant.bikeHealth, Constant.bikeMoveSpeed
                       , Constant.bikeRotateSpeed, Constant.bikeStartGold, TowerType.BasicTower, playerNumber));
                    break;

                case VehicleType.Plane:
                    go.AddComponent(new SpriteRenderer(go, Constant.planeSpriteSheet + playerNumber, 0.1f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new Plane(go, controls, Constant.planeHealth, Constant.planeMoveSpeed
                       , Constant.planeRotateSpeed, Constant.planeStartGold, TowerType.BasicTower, playerNumber));
                    break;

                default:
                    break;
            }
        }
        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.GameObjectsToAdd.Add(go);

            foreach (Component comp in go.GetComponentList)
            {
                if (comp is Vehicle)
                {
                    GameWorld.Instance.Vehicles.Add(comp as Vehicle);

                    (comp as Vehicle).Stats = new Stats((comp as Vehicle));

                    break;
                }

            }
            return go;
        }
    }
}
