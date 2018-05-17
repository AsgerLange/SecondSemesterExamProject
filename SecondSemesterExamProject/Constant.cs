using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Constant
    {
        #region Vehicles
        #region Tank
        public static readonly string tankSpriteSheet = "Tank";
        public static readonly float tankMoveSpeed = 100;
        public static readonly float tankFireRate = 1;
        public static readonly int tankHealth = 1000;
        public static readonly float tankRotateSpeed = 2.0F;
        public static readonly int tankStartGold = 100;
        #endregion
        #endregion

        #region Tower
        #region HQ
        public static readonly string HQSpriteSheet = "HQ";
        public static readonly float HQFireRate = 2;
        public static readonly int HQHealth = 1000;
        public static readonly int HQAttackRange = 1000;
        #endregion
        #endregion

        #region Bullets
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 50;

        #endregion;

        #region Enemies
        #region BasicEnemy
        public static readonly string basicEnemySpriteSheet = "EnemyBasic";
        public static readonly int basicEnemyHealth = 250;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 1;

        #endregion
        #endregion

        #region Terrain
        public static readonly string rockImage = "Rock";
        public static readonly int PushForce = 2;
        #endregion
    }
}
