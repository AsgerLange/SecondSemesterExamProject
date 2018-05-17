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
        public static readonly string tankSpriteSheet = "PlayerTank";
        public static readonly float tankMoveSpeed = 100;
        public static readonly float tankFireRate = 1;
        public static readonly int tankHealth = 1000;
        public static readonly float tankRotateSpeed = 2.0F;
        #endregion
        #endregion

        #region Bullets
        #region BasicBullet
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 700;
        public static readonly float basicBulletLifeSpan = 1f;
        #endregion;
        #endregion;

        #region Enemies
        #region BasicEnemy
        public static readonly string basicEnemySpriteSheet = "EnemyBasic";
        public static readonly int basicEnemyHealth = 250;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 1;

        #endregion
        #endregion

        #region Rocks
        public static readonly string rockImage = "TerrainRock";
        public static readonly int rockPushForce = 2;
        #endregion
    }
}
