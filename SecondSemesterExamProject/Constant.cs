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
        public static readonly float tankFireRate = 2;
        public static readonly int tankHealth = 1000;
        public static readonly float tankRotateSpeed = 2.0F;
        #endregion
        #endregion

        #region Bullets
        public static readonly string bulletSheet = "";
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
        public static readonly string rockImage = "Rock";
        public static readonly int rockPushForce = 2;
        #endregion
    }
}
