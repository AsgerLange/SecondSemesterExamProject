using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class TowerBuilder : ITowerBuilder
    {
        private GameObject go;

        public void Build(Vector2 position, TowerType type)
        {
            go = new GameObject();
            switch (type)
            {
                case TowerType.BasicTower:

                    go.Transform.Position = position;
                    go.AddComponent(new SpriteRenderer(go, Constant.basicTowerSpriteSheet, 0.9f));
                    go.AddComponent(new Animator(go));
                    go.AddComponent(new BasicTower(go, Constant.basicTowerFireRate, Constant.basicTowerHealth,
                        Constant.basicTowerAttackRange, Constant.basicTowerBulletType));
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
