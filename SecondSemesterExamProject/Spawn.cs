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
        private float swarmerChance = Constant.swarmerSpawnModifier;
        private float siegeBreakerEnemyChance = Constant.siegeBreakerSpawnModifier;


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
            if (GameWorld.Instance.pvp == false)
            {

                SpawnSingle();
                CreateWave();
            }
            SpawnCrate();
        }

        /// <summary>
        /// Spawns a crate with an interval
        /// </summary>
        private void SpawnCrate()
        {
            if ((GameWorld.Instance.pvp==false && Constant.crateSpawnDelay + crateStamp <= GameWorld.Instance.TotalGameTime) 
                || (GameWorld.Instance.pvp == true && (Constant.crateSpawnDelay/2) + crateStamp <= GameWorld.Instance.TotalGameTime))
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

                if (wave >= Constant.siegeBreakerSpawnWave)
                {
                    siegeBreakerEnemyChance += (siegeBreakerEnemyChance * 0.03f);//add to the chance of harder enemies
                    if (siegeBreakerEnemyChance > 600)
                    {
                        siegeBreakerEnemyChance = 600;
                    }
                }
                if (wave >= Constant.basicEliteSpawnWave)
                {
                    eliteBasicEnemyChance += (eliteBasicEnemyChance * 0.03f);//add to the chance of harder enemies
                    if (eliteBasicEnemyChance > 800)
                    {
                        eliteBasicEnemyChance = 800;
                    }
                }
                if (wave >= Constant.spitterSpawnWave)
                {
                    spitterChance += (spitterChance * 0.03f);
                    if (spitterChance > 800)
                    {
                        spitterChance = 800;
                    }
                }
                if (wave >= Constant.swarmerSpawnWave)
                {
                    swarmerChance += (swarmerChance * 0.03f);
                    if (swarmerChance > 800)
                    {
                        swarmerChance = 800;
                    }
                }

                int side = rnd.Next(1, 5);
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
                if (waveSize > 400)
                {
                    waveSize = 400;
                }

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
                int side = rnd.Next(1, 5);
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

                enemyType = ChooseEnemyType();

                if (enemyType == EnemyType.Swarmer)
                {
                    int roll = rnd.Next(1, Constant.swarmerSpawnMax + 1);
                    for (int s = 0; s < roll; s++)
                    {
                        GameObject tmp = EnemyPool.Instance.CreateEnemy(new Vector2(spawnPos.X + s, spawnPos.Y), enemyType, Alignment.Enemy);
                        
                        foreach (Component comp in tmp.GetComponentList)
                        {
                            if (comp is SpriteRenderer)
                            {
                                (comp as SpriteRenderer).color = Color.Red;
                            }

                        }
                    }
                }
                else
                {
                    EnemyPool.Instance.CreateEnemy(spawnPos, enemyType, Alignment.Enemy);
                }

                spawned++;
            }
        }

        /// <summary>
        /// Returns the enemyType that has been chosen
        /// </summary>
        /// <returns></returns>
        private EnemyType ChooseEnemyType()
        {
            EnemyType enemyType;
            bool chosen = false;
            int roll = rnd.Next(1001);
            if (wave >= Constant.siegeBreakerSpawnWave && roll <= siegeBreakerEnemyChance && !chosen)
            {
                enemyType = EnemyType.SiegebreakerEnemy;
                chosen = true;
            }
            else
            {
                roll = rnd.Next(1001);
                if (wave >= Constant.basicEliteSpawnWave && roll <= eliteBasicEnemyChance && !chosen)
                {
                    enemyType = EnemyType.BasicEliteEnemy;
                    chosen = true;
                }
                else
                {
                    roll = rnd.Next(1001);
                    if (wave >= Constant.spitterSpawnWave && roll <= spitterChance && !chosen)
                    {
                        enemyType = EnemyType.Spitter;
                        chosen = true;
                    }
                    else
                    {
                        roll = rnd.Next(1001);
                        if (wave >= Constant.swarmerSpawnWave && roll <= swarmerChance)
                        {
                            enemyType = EnemyType.Swarmer;
                            chosen = true;
                        }
                        else
                        {
                            enemyType = EnemyType.BasicEnemy;
                        }
                    }
                }
            }
            return enemyType;
        }
    }
}
