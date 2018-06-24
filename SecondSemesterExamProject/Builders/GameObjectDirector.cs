using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    class GameObjectDirector
    {
        private static GameObjectDirector instance;

        private BulletBuilder bulletBuilder;
        private EnemyBuilder enemyBuilder;
        private TowerBuilder towerBuilder;
        private TerrainBuilder terrainBuilder;
        private VehicleBuilder vehicleBuilder;
        private CrateBuilder lootCrateBuilder;
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
            this.terrainBuilder = new TerrainBuilder();
            this.vehicleBuilder = new VehicleBuilder();
            this.lootCrateBuilder = new CrateBuilder();
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
            enemyBuilder.Build(position, type);

            return enemyBuilder.GetResult(); //returns the bullet that has been build
        }

        /// <summary>
        /// Constructs the tower
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject Construct(Vector2 position, TowerType type, Vehicle vehicle)
        {
            towerBuilder.Build(position, type, vehicle);

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
            terrainBuilder.Build(position, size, rotation);

            return terrainBuilder.GetResult(); //returns the bullet that has been build
        }

        /// <summary>
        /// constructs a vehicle
        /// </summary>
        /// <param name="type"></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        public GameObject Construct(VehicleType type, Controls controls, int playerNumber)
        {
            vehicleBuilder.Build(type, controls, playerNumber);

            return vehicleBuilder.GetResult(); //returns the vehicle that has been build
        }
        /// <summary>
        /// Creates a random functional crate
        /// </summary>
        /// <returns></returns>
        public GameObject ConstructCrate()
        {
            lootCrateBuilder.Build();

            return lootCrateBuilder.GetResult(); //returns the vehicle that has been build
        }
    }
}
