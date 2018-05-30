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
        public void Build(Vector2 position, TowerType type)
        {
            go = new GameObject();
            switch (type)
            {
                case TowerType.BasicTower:

                    go.Transform.Position = position;
                    go.AddComponent(new SpriteRenderer(go, Constant.basicTowerSpriteSheet, 0.9f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new BasicTower(go));
                    go.AddComponent(new Collider(go, Alignment.Friendly));
                    break;

                case TowerType.ShotgunTower:

                    go.Transform.Position = position;
                    go.AddComponent(new SpriteRenderer(go, Constant.ShotgunTowerSpriteSheet, 0.9f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new ShotgunTower(go));
                    go.AddComponent(new Collider(go, Alignment.Friendly));
                    break;

                case TowerType.SniperTower:

                    go.Transform.Position = position;
                    go.AddComponent(new SpriteRenderer(go, Constant.sniperTowerSpriteSheet, 0.9f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new SniperTower(go));
                    go.AddComponent(new Collider(go, Alignment.Friendly));
                    break;
                case TowerType.MachineGunTower:

                    go.Transform.Position = position;
                    go.AddComponent(new SpriteRenderer(go, Constant.machineGunTowerSpriteSheet, 0.9f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new MachineGunTower(go));
                    go.AddComponent(new Collider(go, Alignment.Friendly));
                    break;
                default:
                    break;
            }
        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }


}
