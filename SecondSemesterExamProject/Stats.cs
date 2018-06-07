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
    class Stats
    {
        private Vehicle vehicle;

        public Vehicle Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }
        #region VehicleStats
        private int totalAmountOfGold;
        private int totalAmountOfPlayerDeaths;


        public int TotalAmountOfGold
        {
            get { return totalAmountOfGold; }
            set { totalAmountOfGold = value; }
        }
        public int PlayerDeathAmmount
        {
            get { return totalAmountOfPlayerDeaths; }
            set { totalAmountOfPlayerDeaths = value; }
        }
        #endregion;

        #region BulletCounters
        private int basicBulletCounter;
        private int biggerBulletCounter;
        private int sniperBulletCounter;
        private int shotgunPelletsCounter;
        private static int spitterBulletCounter;

        private int bulletsMissed;

        public int BulletsMissed
        {
            get { return bulletsMissed; }
            set { bulletsMissed = value; }
        }
        public int BasicBulletCounter
        {
            get { return basicBulletCounter; }
            set { basicBulletCounter = value; }
        }
        public int BiggerBulletCounter
        {
            get { return biggerBulletCounter; }
            set { biggerBulletCounter = value; }
        }
        public int SniperBulletCounter
        {
            get { return sniperBulletCounter; }
            set { sniperBulletCounter = value; }
        }
        public int ShotgunPelletsCounter
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
        private static int spitterKilled = 0;
        private static int swarmerKilled = 0;
        private static int siegeBreakerKilled = 0;

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
        public static int SwarmerKilled
        {
            get { return swarmerKilled; }
            set { swarmerKilled = value; }
        }
        public static int SiegeBreakerKilled
        {
            get { return siegeBreakerKilled; }
            set { siegeBreakerKilled = value; }
        }
        #endregion;

        #region WeaponsFired
        private int basicWeaponFired;
        private int sniperFired;
        private int machinegunFired;
        private int shotgunFired;

        public int BasicWeaponFired
        {
            get { return basicWeaponFired; }
            set { basicWeaponFired = value; }
        }
        public int SniperFired
        {
            get { return sniperFired; }
            set { sniperFired = value; }
        }
        public int MachinegunFired
        {
            get { return machinegunFired; }
            set { machinegunFired = value; }
        }
        public int ShotgunFired
        {
            get { return shotgunFired; }
            set { shotgunFired = value; }
        }

        #endregion;

        #region TowersCreated
        private int basicTowerBuilt;
        private int shotgunTowerbuilt;
        private int machinegunTowerbuilt;
        private int sniperTowerBuilt;

        public int BasicTowerBuilt
        {
            get { return basicTowerBuilt; }
            set { basicTowerBuilt = value; }
        }
        public int ShotgunTowerbuilt
        {
            get { return shotgunTowerbuilt; }
            set { shotgunTowerbuilt = value; }
        }
        public int MachinegunTowerbuilt
        {
            get { return machinegunTowerbuilt; }
            set { machinegunTowerbuilt = value; }
        }
        public int SniperTowerBuilt
        {
            get { return sniperTowerBuilt; }
            set { sniperTowerBuilt = value; }
        }
        #endregion

        /// <summary>
        /// Stat constructor
        /// </summary>
        /// <param name="vehicle">the vehicle who owns the stat object</param>
        public Stats(Vehicle vehicle)
        {
            this.vehicle = vehicle;
        }
        /// <summary>
        /// Calculates Accuracy, based on total amounts of bullets fired and missed
        /// </summary>
        /// <returns></returns>
        public int CalculateAccuracy()
        {
            float result;

            float sum;

            sum = vehicle.Stats.BasicBulletCounter + vehicle.Stats.biggerBulletCounter +
            vehicle.Stats.sniperBulletCounter + vehicle.Stats.shotgunPelletsCounter;
            if (sum == 0)
            {
                sum = 1;
            }
            result = vehicle.Stats.bulletsMissed / sum * 100;

            result = 100 - result;
            return (int)result;
        }
    }
}
