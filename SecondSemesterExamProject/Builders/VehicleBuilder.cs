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

        private int playerCount = 1;

        /// <summary>
        /// The vehicleBuilder builds a vehicle
        /// </summary>
        /// <param name="type">type of vehicle</param>
        public void Build(VehicleType type, Controls controls)
        {

            switch (type)
            {
                case VehicleType.Tank:
                    go = new GameObject();
                    go.Transform.Position = new Vector2(Constant.width / 2 + 1, Constant.higth / 2); //spawns in the middle
                    go.AddComponent(new SpriteRenderer(go, Constant.tankSpriteSheet + playerCount, 0.1f));//Sprite that fits player
                    go.AddComponent(new Animator(go));//allows go to be animated

                    //Standard tank setup
                    go.AddComponent(new Tank(go, controls, new Sniper(go), Constant.tankHealth, Constant.tankMoveSpeed
                       , Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower));
                    go.AddComponent(new Collider(go, Alignment.Friendly));//adds collider
                    break;
                case VehicleType.Bike:
                    go = new GameObject();
                    go.Transform.Position = new Vector2(Constant.width / 2 + 1, Constant.higth / 2);
                    go.AddComponent(new SpriteRenderer(go, Constant.bikeSpriteSheet + playerCount, 0.1f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new Bike(go, controls, new Shotgun(go), Constant.bikeHealth, Constant.bikeMoveSpeed
                       , Constant.bikeRotateSpeed, Constant.bikeStartGold, TowerType.ShotgunTower));
                    go.AddComponent(new Collider(go, Alignment.Friendly));
                    break;
                case VehicleType.Plane:
                    go = new GameObject();
                    go.Transform.Position = new Vector2(Constant.width / 2 + 1, Constant.higth / 2);
                    go.AddComponent(new SpriteRenderer(go, Constant.planeSpriteSheet + playerCount, 0.1f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new Plane(go, controls, new MachineGun(go), Constant.planeHealth, Constant.planeMoveSpeed
                       , Constant.planeRotateSpeed, Constant.planeStartGold, TowerType.BasicTower));
                    go.AddComponent(new Collider(go, Alignment.Friendly));

                    break;

                default:
                    break;
            }
        }
        public GameObject GetResult()
        {
            playerCount++;
           

            go.LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.GameObjectsToAdd.Add(go);

            if (playerCount > Constant.maxAmountOfVehicles)
            {
                playerCount = Constant.maxAmountOfVehicles; //just in case
            }
            return go;
        }
    }
}
