using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    class BulletBuilder : IBulletBuilder
    {
        private GameObject go;

        public void Build(Vector2 position, BulletType type, float rotation, Alignment alignment)
        {
            go = new GameObject();
            go.Transform.Position = position;

            switch (type)
            {
                case BulletType.BasicBullet:
                    go.AddComponent(new SpriteRenderer(go, Constant.bulletSheet, 0));
                    go.AddComponent(new BasicBullet(go, type, rotation));
                    break;
                case BulletType.BiggerBullet:
                    go.AddComponent(new SpriteRenderer(go, Constant.biggerBulletSheet, 0));
                    go.AddComponent(new BiggerBullet(go, type, rotation));
                    break;
                default:
                    break;
            }

            go.AddComponent(new Collider(go, alignment));
            go.AddComponent(new Animator(go));

        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }
}

