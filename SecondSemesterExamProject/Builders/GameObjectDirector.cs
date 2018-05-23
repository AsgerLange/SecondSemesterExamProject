﻿using System;
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
        private TowerBuilder towerBuilder;
        private TerrainBuilder rockBuilder;

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
            this.towerBuilder = new TowerBuilder();
            this.rockBuilder = new TerrainBuilder();
        }

        /// <summary>
        /// construction of bullets
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject Construct(Vector2 position, BulletType type, float rotation, Alignment alignment)
        {
            bulletBuilder.Build(position, type, rotation, alignment);

            return bulletBuilder.GetResult(); //returns the bullet that has been build
        }

        /// <summary>
        /// construction of enemies
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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
        public GameObject Construct(Vector2 position, TowerType type)
        {
            switch (type)
            {
                case TowerType.BasicTower:
                    towerBuilder.Build(position, type);

                    break;

                default:
                    break;
            }
            return towerBuilder.GetResult();
        }

        /// <summary>
        /// Constructs a rock
        /// </summary>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public GameObject Construct(Vector2 position, int size, int rotation)
        {
            rockBuilder.Build(position, size, rotation);

            return rockBuilder.GetResult(); //returns the bullet that has been build
        }
    }
}
