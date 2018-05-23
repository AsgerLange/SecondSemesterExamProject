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
        public static readonly float buildTowerCoolDown = 2;

        #region Tank
        public static readonly string tankSpriteSheet = "PlayerTank";
        public static readonly float tankMoveSpeed = 100;
        public static readonly float tankFireRate = 1f;
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
        public static readonly BulletType HQbulletType = BulletType.BiggerBullet;
        #endregion

        #region BasicTower
        public static readonly string basicTowerSpriteSheet = "TowerBasic";
        public static readonly float basicTowerFireRate =1;
        public static readonly int basicTowerHealth = 100;
        public static readonly BulletType basicTowerBulletType = BulletType.BasicBullet;
        public static readonly int basicTowerAttackRange = 150;
        #endregion;
        #endregion

        #region Bullets
        #region BasicBullet
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 700;
        public static readonly float basicBulletLifeSpan = 1f;
        public static readonly float basicBulletDmg = 25;
        #endregion;

        #region BiggerBullet
        public static readonly string biggerBulletSheet = "BiggerBullet";
        public static readonly float biggerBulletMovementSpeed = 700;
        public static readonly float biggerBulletLifeSpan = 1.5f;
        public static readonly float biggerBulletDmg = 50;
        #endregion;
        #endregion;

        #region Enemies
        #region BasicEnemy
        public static readonly string basicEnemySpriteSheet = "BasicEnemy";
        public static readonly int basicEnemyHealth = 200;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 0.7f;
        public static readonly int basicEnemyDamage = 10;

        #endregion
        #endregion

        #region Terrain
        public static readonly string rockImage = "TerrainRock";
        public static readonly int pushForce = 2;
        #endregion
    }
}
