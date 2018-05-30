using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class EnemyBuilder
    {
        private GameObject go;
        public void Build(Vector2 position, EnemyType type)
        {
            go = new GameObject();
            go.Transform.Position = position;
            switch (type)
            {
                case EnemyType.BasicEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.basicEnemySpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth, Constant.basicEnemyDamage,
                            Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate,Constant.basicEnemyAttackRadius, type));

                    break;

                case EnemyType.BasicEliteEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.basicEliteEnemySpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new BasicEliteEnemy(go, Constant.basicEliteEnemyHealth, Constant.basicEliteEnemyDamage,
                            Constant.basicEliteEnemyMovementSpeed, Constant.basicEliteEnemyAttackRate,Constant.basicEliteEnemyAttackRadius ,type));

                    break;

                case EnemyType.Spitter:
                    go.AddComponent(new SpriteRenderer(go, Constant.spitterSpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new Spitter(go, Constant.spitterHealth,
                            Constant.spitterMovementSpeed, Constant.spitterAttackRate, Constant.spitterAttackRange, type, Constant.spitterBulletType,Constant.spitterSpread));
                    break;

                default:
                    go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth, Constant.basicEnemyDamage,
                            Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate,Constant.basicEnemyAttackRadius, type));
                    break;
            }
            go.AddComponent(new Collider(go, Alignment.Enemy));
        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }


}

