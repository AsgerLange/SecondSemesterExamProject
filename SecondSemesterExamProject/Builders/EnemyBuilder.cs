using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class EnemyBuilder : IEnemyBuilder
    {
        private GameObject go;
        public void Build(Vector2 position, EnemyType type)
        {
            go = new GameObject();
            go.Transform.Position = position;
            go.AddComponent(new SpriteRenderer(go, Constant.basicEnemySpriteSheet, 0));
            go.AddComponent(new Animator(go));
            go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth,
                    Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate));
            go.AddComponent(new Collider(go, Alignment.Enemy));
        }
        

        public GameObject GetResult()
        {
            return go;
        }
    }


}

