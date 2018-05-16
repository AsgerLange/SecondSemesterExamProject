using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    enum BuilderType { BulletBuilder, EnemyBuilder }
    class GameObjectDirector
    {
        private static GameObjectDirector instance;

        private BulletBuilder bulletBuilder;
        private EnemyBuilder enemyBuilder;

        /// <summary>
        /// Get Property to the GameObjectDirector's Singleton instance
        /// </summary>
        public static GameObjectDirector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObjectDirector();

                }

                return instance;
            }
        }



        private GameObjectDirector()
        {
            this.bulletBuilder = new BulletBuilder();
            this.enemyBuilder = new EnemyBuilder();

        }


        public GameObject Construct(Vector2 position, BulletType type)
        {
            switch (type)
            {
                case BulletType.BaiscBullet:
                    bulletBuilder.Build(position, type);
                    break;

                default:
                    break;
            }


            return bulletBuilder.GetResult(); //returns the bullet that has been build
        }

        public GameObject Construct(Vector2 position, EnemyType type)
        {
            switch (type)
            {
                case EnemyType.BasicEnemy:
                    enemyBuilder.Build(position, type);
                    break;

                default:
                    break;
            }


            return enemyBuilder.GetResult(); //returns the bullet that has been build
        }
    }
}
