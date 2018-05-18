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
        public static readonly float tankFireRate = 0.1f;
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
        #region BasicBullet
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 700;
        public static readonly float basicBulletLifeSpan = 1f;
        public static readonly float basicBulletDmg = 50;
        #endregion;
        #endregion;

        #region Enemies
        #region BasicEnemy
        public static readonly string basicEnemySpriteSheet = "BasicEnemy";
        public static readonly int basicEnemyHealth = 50;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 1;

        #endregion
        #endregion

        #region Terrain
        public static readonly string rockImage = "TerrainRock";
        public static readonly int pushForce = 2;
        #endregion
    }
}
