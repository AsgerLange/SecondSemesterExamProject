using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Constant
    {
        public readonly static int width = 1240;
        public readonly static int higth = 720;

        #region Button
        public readonly static string buttonTexture = "Button";
        public readonly static string buttonFont = "stat";
        public readonly static string startGameButton = "Start Game";

        #endregion

        #region Spawning
        public readonly static float singleSpawnDelay = 3f;
        public readonly static float waveSpawnDelay = 30;
        public readonly static int waveSizeVariable = 3;
        #endregion

        #region Vehicles
        public static readonly float buildTowerCoolDown = 1;

        public static readonly int maxAmountOfVehicles = 2;
        public static readonly int minAmountOfVehicles = 1;

        #region Tank
        public static readonly string tankSpriteSheet = "PlayerTank";

        public static readonly float tankMoveSpeed = 100;
        public static readonly int tankHealth = 500;
        public static readonly float tankRotateSpeed = 2.0F;
        public static readonly int tankStartGold = 100;

        #endregion
        #region Plane
        public static readonly string planeSpriteSheet = "PlayerPlane";

        public static readonly float planeMoveSpeed = 200;
        public static readonly int planeHealth = 100;
        public static readonly float planeRotateSpeed = 1.5F;
        public static readonly int planeStartGold = 100;

        #endregion;
        #region Bike
        public static readonly string bikeSpriteSheet = "PlayerTank";

        public static readonly float bikeMoveSpeed = 200;
        public static readonly int bikeHealth = 250;
        public static readonly float bikeRotateSpeed = 3f;
        public static readonly int bikeStartGold = 100;

        #endregion;
        #endregion

        #region Tower
        #region HQ
        public static readonly string HQSpriteSheet = "HQ";
        public static readonly float HQFireRate = 2;
        public static readonly int HQHealth = 1000;
        public static readonly int HQAttackRange = 450;
        public static readonly BulletType HQbulletType = BulletType.BasicBullet;
        public static readonly int HQSpread = 4;

        #endregion

        #region BasicTower
        public static readonly int basicTowerPrice = 100;
        public static readonly string basicTowerSpriteSheet = "TowerBasic";
        public static readonly float basicTowerFireRate = 1;
        public static readonly int basicTowerHealth = 100;
        public static readonly BulletType basicTowerBulletType = BulletType.BasicBullet;
        public static readonly int basicTowerAttackRange = 200;
        public static readonly int basicTowerSpread = 4;

        #endregion;

        #region ShotgunTower
        public static readonly int shotgunTowerPrice = 100;
        public static readonly string ShotgunTowerSpriteSheet = "TowerBasic";
        public static readonly float ShotgunTowerFireRate = 2.5f;
        public static readonly int ShotgunTowerHealth = 100;
        public static readonly BulletType ShotgunTowerBulletType = BulletType.ShotgunPellet;
        public static readonly int shotgunTowerAttackRange = 150;
        public static readonly int ShotgunTowerSpread = 15;
        public static readonly int shotgunTowerPelletAmount = 12;


        #endregion;
        #endregion

        #region Bullets
        #region BasicBullet
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 700;
        public static readonly float basicBulletLifeSpan = 1f;
        public static readonly int basicBulletDmg = 75;
        #endregion;

        #region BiggerBullet
        public static readonly string biggerBulletSheet = "BiggerBullet";
        public static readonly float biggerBulletMovementSpeed = 700;
        public static readonly float biggerBulletLifeSpan = 1.5f;
        public static readonly int biggerBulletDmg = 150;
        #endregion;

        #region sniperBullet
        public static readonly string sniperBulletSheet = "SniperBullet";
        public static readonly float sniperBulletMovementSpeed = 1100;
        public static readonly float sniperBulletLifeSpan = 1f;
        public static readonly int sniperBulletBulletDmg = 400;
        #endregion;
        #endregion;

        #region shotgunPellet
        public static readonly string shotgunPelletSheet = "ShotgunPellet";
        public static readonly float shotgunPelletMovementSpeed = 750;
        public static readonly float shotgunPelletLifeSpan = 0.3f;
        public static readonly int shotgunPelletDmg = 30;
        #endregion;

        #region Weapons
        #region BasicWeapon
        public readonly static float basicWeaponFireRate = 0.7f;
        public readonly static int basicWeaponAmmo = int.MaxValue;
        public readonly static BulletType basicWeaponBulletType = BulletType.BiggerBullet;
        public readonly static int basicWeaponSpread = 3;

        #endregion;

        #region Sniper
        public readonly static float sniperFireRate = 1.8f;
        public readonly static int sniperAmmo = 20;
        public readonly static BulletType sniperBulletType = BulletType.SniperBullet;
        public readonly static int sniperSpread = 0;

        #endregion;

        #region Shotgun
        public readonly static float shotGunFireRate = 1f;
        public readonly static int shotGunAmmo = 20;
        public readonly static BulletType shotgunBulletType = BulletType.ShotgunPellet;
        public readonly static int shotGunSpread = 15;
        public static readonly int shotgunPelletAmount = 20;


        #endregion;
        #region MachineGun
        public readonly static float MachineGunFireRate = 0.1f;
        public readonly static int MachineGunGunAmmo = 200;
        public readonly static int MachineGunSpread = 7;

        public readonly static BulletType MachineGunBulletType = BulletType.BasicBullet;

        #endregion;
        #endregion;

        #region Enemies
        #region BasicEnemy
        public readonly static int basicEnemyGold = 2;
        public static readonly string basicEnemySpriteSheet = "BasicEnemy";
        public static readonly int basicEnemyHealth = 200;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 0.7f;
        public static readonly int basicEnemyDamage = 10;

        #endregion

        #region BasicEliteEnemy
        public readonly static int basicEliteEnemyGold = 4;
        public static readonly string basicEliteEnemySpriteSheet = "BasicEliteEnemy";
        public static readonly int basicEliteEnemyHealth = 500;
        public static readonly float basicEliteEnemyMovementSpeed = 30;
        public static readonly float basicEliteEnemyAttackRate = 0.7f;
        public static readonly int basicEliteEnemyDamage = 15;
        #endregion
        #endregion

        #region Terrain
        public static readonly int spawnZoneSize = 100;
        public static readonly string rockImage = "Rock1";
        public static readonly int pushForce = 2;
        #endregion
    }
}