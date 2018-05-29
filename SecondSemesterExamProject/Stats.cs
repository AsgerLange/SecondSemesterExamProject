using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// Class for keeping track of statistics in the game
    /// </summary>
    static class Stats
    {
        #region VehicleStats
        private static int totalAmountOfGold;
        private static int totalAmountOfPlayerDeaths;


        public static int TotalAmountOfGold
        {
            get { return totalAmountOfGold; }
            set { totalAmountOfGold = value; }
        }
        public static int TotalAmountOfPlayerDeaths
        {
            get { return totalAmountOfPlayerDeaths; }
            set { totalAmountOfPlayerDeaths = value; }
        }
        #endregion;

        #region BulletCounters
        private static int basicBulletCounter;
        private static int biggerBulletCounter;
        private static int sniperBulletCounter;
        private static int shotgunPelletsCounter;
        private static int spitterBulletCounter;

        public static int BasicBulletCounter
        {
            get { return basicBulletCounter; }
            set { basicBulletCounter = value; }
        }
        public static int BiggerBulletCounter
        {
            get { return biggerBulletCounter; }
            set { biggerBulletCounter = value; }
        }
        public static int SniperBulletCounter
        {
            get { return sniperBulletCounter; }
            set { sniperBulletCounter = value; }
        }
        public static int ShotgunPelletsCounter
        {
            get { return shotgunPelletsCounter; }
            set { shotgunPelletsCounter = value; }
        }
        public static int SpitterBulletCounter
        {
            get { return spitterBulletCounter; }
            set { spitterBulletCounter = value; }
        }
        #endregion;

        #region EnemyKillCounts

        private static int basicEnemyKilled = 0;
        private static int basicEliteEnemyKilled = 0;
        private static int spitterKilled= 0;

        public static int BasicEnemyKilled
        {
            get { return basicEnemyKilled; }
            set { basicEnemyKilled = value; }
        }

        public static int BasicEliteEnemyKilled
        {
            get { return basicEliteEnemyKilled; }
            set { basicEliteEnemyKilled = value; }
        }

        public static int SpitterKilled
        {
            get { return spitterKilled; }
            set { spitterKilled = value; }
        }
        #endregion;

        #region WeaponsFired
        private static int basicWeaponFired;
        private static int sniperFired;
        private static int machinegunFired;
        private static int shotgunFired;

        public static int BasicWeaponFired
        {
            get { return basicWeaponFired; }
            set { basicWeaponFired = value; }
        }
        public static int SniperFired
        {
            get { return sniperFired; }
            set { sniperFired = value; }
        }
        public static int MachinegunFired
        {
            get { return machinegunFired; }
            set { machinegunFired = value; }
        }
        public static int ShotgunFired
        {
            get { return shotgunFired; }
            set { shotgunFired = value; }
        }

        #endregion;
    }
}
