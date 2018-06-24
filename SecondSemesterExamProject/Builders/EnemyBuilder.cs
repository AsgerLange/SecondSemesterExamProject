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
        public void Build(Vector2 position, EnemyType type, Alignment alignment)
        {
            go = new GameObject();
            go.Transform.Position = position;
            switch (type)
            {
                case EnemyType.BasicEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.basicEnemySpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth, Constant.basicEnemyDamage,
                            Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate,Constant.basicEnemyAttackRadius, type, alignment));

                    break;

                case EnemyType.BasicEliteEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.basicEliteEnemySpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new BasicEliteEnemy(go, Constant.basicEliteEnemyHealth, Constant.basicEliteEnemyDamage,
                            Constant.basicEliteEnemyMovementSpeed, Constant.basicEliteEnemyAttackRate,Constant.basicEliteEnemyAttackRadius ,type, alignment));

                    break;

                case EnemyType.Swarmer:
                    go.AddComponent(new SpriteRenderer(go, Constant.swarmerEnemySpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new SwarmerEnemy(go, Constant.swarmerEnemyHealth, Constant.swarmerEnemyDamage,
                            Constant.swarmerEnemyMovementSpeed, Constant.swarmerEnemyAttackRate, Constant.swarmerEnemyAttackRadius, type, alignment));

                    break;

                case EnemyType.SiegebreakerEnemy:
                    go.AddComponent(new SpriteRenderer(go, Constant.siegeBreakerSpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new SiegebreakerEnemy(go, Constant.siegeBreakerEnemyHealth, Constant.siegeBreakerEnemyDamage,
                            Constant.siegeBreakerEnemyMovementSpeed, Constant.siegeBreakerEnemyAttackRate, Constant.siegeBreakerEnemyAttackRadius, type, alignment));

                    break;

                case EnemyType.Spitter:
                    go.AddComponent(new SpriteRenderer(go, Constant.spitterSpriteSheet, 0.2f));

                    go.AddComponent(new Animator(go));

                    go.AddComponent(new Spitter(go, Constant.spitterHealth,
                            Constant.spitterMovementSpeed, Constant.spitterAttackRate, Constant.spitterAttackRange, type, Constant.spitterBulletType,Constant.spitterSpread, alignment));
                    break;

                default:
                    go.AddComponent(new BasicEnemy(go, Constant.basicEnemyHealth, Constant.basicEnemyDamage,
                            Constant.basicEnemyMovementSpeed, Constant.basicEnemyAttackRate,Constant.basicEnemyAttackRadius, type, alignment));
                    break;
            }
            go.AddComponent(new Collider(go, alignment));
        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }


}

