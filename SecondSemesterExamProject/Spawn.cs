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

        }

        /// <summary>
        /// creates a new wave
        /// </summary>
        private void CreateWave()
        {

        }

        /// <summary>
        /// spawns a lone Critter
        /// </summary>
        private void SpawnSingle()
        {

        }
    }
}
