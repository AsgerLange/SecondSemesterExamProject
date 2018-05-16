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

        public void Build(Vector2 position, BulletType type)
        {
            go = new GameObject();
            go.Transform.Position = position;
            go.AddComponent(new SpriteRenderer(go, Constant.bulletSheet,0));
            go.AddComponent(new Bullet(go, type));
            go.AddComponent(new Collider(go,Alignment.Friendly));
            
        }

        
        public GameObject GetResult()
        {
            return go;
        }
    }
    }

