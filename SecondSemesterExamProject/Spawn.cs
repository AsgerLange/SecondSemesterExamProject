using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Spawn
    {
        private int wave = 0;
        private int waveSize = 0;
        private int spawned = 0;
        private Random rnd = new Random();
        private Rectangle topZone;
        private Rectangle leftZone;
        private Rectangle rightZone;
        private Rectangle bottomZone;
        private float spawnStamp;
        private float waveStamp;
        private float crateStamp;
        private float eliteBasicEnemyChance = Constant.basicEliteSpawnModifier;//spawn chance out of 1000
        private float spitterChance = Constant.spitterSpawnModifier;


        /// <summary>
        /// returns the current wave number
        /// </summary>
        public int Wave
        {
            get { return wave; }
        }

        /// <summary>
        /// returns the size of the current wave
        /// </summary>
        public int WaveSize
        {
            get { return waveSize; }
        }

        /// <summary>
        /// Prepers the waveSpavner
        /// </summary>
        public Spawn(int width, int hight)
        {
            SetSpawnZones(width, hight);
            CreateWave();
        }

        /// <summary>
        /// sets the spawnZones
        /// </summary>
        /// <param name="width"></param>
        /// <param name="hight"></param>
        private void SetSpawnZones(int width, int hight)
        {
            topZone = new Rectangle(0, -Constant.spawnZoneSize, width, Constant.spawnZoneSize);
            leftZone = new Rectangle(-Constant.spawnZoneSize, 0, Constant.spawnZoneSize, hight);
            rightZone = new Rectangle(width, 0, Constant.spawnZoneSize, hight);
            bottomZone = new Rectangle(0, hight, width, Constant.spawnZoneSize);
        }

        /// <summary>
        /// tjecks and spawns new waves
        /// </summary>
        public void Update()
        {
            SpawnSingle();
            CreateWave();
            SpawnCrate();
        }

        /// <summary>
        /// Spawns a crate with an interval
        /// </summary>
        private void SpawnCrate()
        {
            if (Constant.crateSpawnDelay + crateStamp <= GameWorld.Instance.TotalGameTime)
            {
                GameWorld.Instance.GameObjectsToAdd.Add(GameObjectDirector.Instance.ConstructCrate());

                crateStamp = GameWorld.Instance.TotalGameTime;
            }

        }
        /// <summary>
        /// creates a new wave
        /// </summary>
        private void CreateWave()
        {
            if (Constant.waveSpawnDelay + waveStamp <= GameWorld.Instance.TotalGameTime)
            {
                wave++;
                eliteBasicEnemyChance += (eliteBasicEnemyChance * 0.03f);//add to the chance of harder enemies
                spitterChance += (spitterChance * 0.03f);

                int side = rnd.Next(0, 5);
                Rectangle spawnRectangle;
                switch (side)
                {
                    case 1:
                        spawnRectangle = leftZone;
                        break;
                    case 2:
                        spawnRectangle = rightZone;
                        break;
                    case 3:
                        spawnRectangle = topZone;
                        break;
                    case 4:
                        spawnRectangle = bottomZone;
                        break;
                    default:
                        spawnRectangle = leftZone;
                        break;
                }
                int waveMin = wave - Constant.waveSizeVariable;
                if (waveMin < 1)
                {
                    waveMin = 1;
                }
                int waveMax = wave + Constant.waveSizeVariable + 1;

                waveSize = rnd.Next(waveMin, waveMax);

                SpawnEnemy(WaveSize, spawnRectangle);
                waveStamp = GameWorld.Instance.TotalGameTime;
                Console.WriteLine("WaveNumber: " + wave);
                Console.WriteLine("Total Enemies: " + spawned + " Spawned");
            }
        }

        /// <summary>
        /// spawns a lone Critter
        /// </summary>
        private void SpawnSingle()
        {
            if (Constant.singleSpawnDelay + spawnStamp <= GameWorld.Instance.TotalGameTime)
            {
                int side = rnd.Next(0, 5);
                Rectangle spawnRectangle;
                switch (side)
                {
                    case 1:
                        spawnRectangle = leftZone;
                        break;
                    case 2:
                        spawnRectangle = rightZone;
                        break;
                    case 3:
                        spawnRectangle = topZone;
                        break;
                    case 4:
                        spawnRectangle = bottomZone;
                        break;
                    default:
                        spawnRectangle = leftZone;
                        break;
                }

                SpawnEnemy(1, spawnRectangle);
                Console.WriteLine("Total Enemies: " + spawned + " Spawned");
                spawnStamp = GameWorld.Instance.TotalGameTime;
            }
        }

        /// <summary>
        /// actually spawns the enemies
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="spawnRectangle"></param>
        private void SpawnEnemy(int amount, Rectangle spawnRectangle)
        {
            Vector2 spawnPos;
            EnemyType enemyType;
            for (int i = 0; i < amount; i++)
            {
                spawnPos = new Vector2(rnd.Next(spawnRectangle.X, spawnRectangle.X + spawnRectangle.Width),
                       rnd.Next(spawnRectangle.Y, spawnRectangle.Y + spawnRectangle.Height));

                int roll = rnd.Next(1001);
                if (roll <= eliteBasicEnemyChance)
                {
                    enemyType = EnemyType.BasicEliteEnemy;
                }
                else
                {
                    roll = rnd.Next(1001);
                    if (roll <= spitterChance)
                    {
                        enemyType = EnemyType.Spitter;
                    }
                    else
                    {
                        enemyType = EnemyType.BasicEnemy;
                    }
                }
                EnemyPool.Instance.CreateEnemy(spawnPos, enemyType);

                spawned++;
            }
        }
    }
}
