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
            topZone = new Rectangle(0, -50, width, 50);
            leftZone = new Rectangle(-50, 0, 50, hight);
            rightZone = new Rectangle(width, 0, 50, hight);
            bottomZone = new Rectangle(0, hight, width, 50);
        }

        /// <summary>
        /// tjecks and spawns new waves
        /// </summary>
        public void Update()
        {
            SpawnSingle();
            CreateWave();
        }

        /// <summary>
        /// creates a new wave
        /// </summary>
        private void CreateWave()
        {
            if (Constant.waveSpawnDelay + waveStamp <= GameWorld.Instance.TotalGameTime)
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
                int waveMin = wave - Constant.waveSizeVariable;
                if (waveMin < 1)
                {
                    waveMin = 1;
                }
                int waveMax = wave + Constant.waveSizeVariable + 1;

                waveSize = rnd.Next(waveMin, waveMax);

                SpawnEnemy(WaveSize, spawnRectangle);
                waveStamp = GameWorld.Instance.TotalGameTime;
                wave++;
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
            int enemyTypeInt;
            EnemyType enemyType;
            for (int i = 0; i < amount; i++)
            {
                spawnPos = new Vector2(rnd.Next(spawnRectangle.X, spawnRectangle.X + spawnRectangle.Width),
                       rnd.Next(spawnRectangle.Y, spawnRectangle.Y + spawnRectangle.Height));

                enemyTypeInt = rnd.Next((Enum.GetNames(typeof(EnemyType)).Length));
                enemyType = (EnemyType)enemyTypeInt;

                EnemyPool.CreateEnemy(spawnPos, enemyType);

                spawned++;
            }
        }
    }
}
