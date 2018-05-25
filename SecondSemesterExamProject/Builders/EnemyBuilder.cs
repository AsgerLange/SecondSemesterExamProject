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
            switch (type)
            {
                case EnemyType.BasicEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.basicEnemySpriteSheet, 0));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth, Constant.basicEnemyDamage,
                            Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate));

                    break;

                case EnemyType.BasicEliteEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.basicEliteEnemySpriteSheet, 0));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new BasicEliteEnemy(go, Constant.basicEliteEnemyHealth, Constant.basicEliteEnemyDamage,
                            Constant.basicEliteEnemyMovementSpeed, Constant.basicEliteEnemyAttackRate));

                    break;

                default:
                    go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth, Constant.basicEnemyDamage,
                            Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate));
                    break;
            }
            go.AddComponent(new Collider(go, Alignment.Enemy));
        }


        public GameObject GetResult()
        {
            return go;
        }
    }


}

