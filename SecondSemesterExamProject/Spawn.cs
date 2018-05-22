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
        public Spawn()
        {

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
    }
}
