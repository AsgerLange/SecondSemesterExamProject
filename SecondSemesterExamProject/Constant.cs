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
        public static string tankSpriteSheet = "Tank";
        public static float tankMoveSpeed = 100;
        public static float tankFireRate = 2;
        public static int tankHealth = 1000;
        public static float tankRotateSpeed = 2.0F;
        #endregion
        #endregion

        #region Enemies
        #region BasicEnemy
        public static string basicEnemySpriteSheet = "EnemyBasic";
        public static int basicEnemyHealth = 250;
        public static float basicEnemyMovementSpeed = 25;
        public static float basicEnemyAttackRate = 1;

        #endregion
        #endregion
    }
}
