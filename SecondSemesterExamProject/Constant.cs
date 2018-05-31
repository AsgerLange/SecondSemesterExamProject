using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    enum VehicleType { None, Tank, Bike, Plane }
    enum BulletType { BasicBullet, BiggerBullet, ShotgunPellet, SniperBullet, SpitterBullet };
    enum EnemyType { BasicEnemy, BasicEliteEnemy, Spitter };
    enum TowerType { BasicTower, ShotgunTower, SniperTower, MachineGunTower };
    enum CrateType { WeaponCrate, TowerCrate, MoneyCrate, HealthCrate };
    enum WeaponType { BasicWeapon, MachineGun, Shotgun, Sniper }


    class Constant
    {
        public readonly static int width = 1240;
        public readonly static int higth = 720;

        #region Menu
        public readonly static string menuBackGround = "Background1";
        public readonly static string gameBackGround = "Background1";
        public readonly static string titleFont = "MenuTitel";
        public readonly static string title = "Invasion of Bugs";
        #endregion

        #region Button
        public readonly static string BlueButtonUpTexture = "ArrowBlueUp";
        public readonly static string BlueButtonDownTexture = "ArrowBlueDown";
        public readonly static string GreenButtonUpTexture = "ArrowGreenUp";
        public readonly static string GreenButtonDownTexture = "ArrowGreenDown";
        public readonly static string blueButtonTexture = "Button";
        public readonly static string RedButtonTexture = "Rutton";
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
        public static readonly int HQAttackRange = 300;
        public static readonly BulletType HQbulletType = BulletType.BasicBullet;
        public static readonly int HQSpread = 4;

        #endregion

        #region BasicTower
        public static readonly int basicTowerPrice = 100;
        public static readonly string basicTowerSpriteSheet = "TowerBasic";
        public static readonly float basicTowerFireRate = 2;
        public static readonly int basicTowerHealth = 100;
        public static readonly BulletType basicTowerBulletType = BulletType.BasicBullet;
        public static readonly int basicTowerAttackRange = 200;
        public static readonly int basicTowerSpread = 4;

        #endregion;

        #region ShotgunTower
        public static readonly int shotgunTowerPrice = 100;
        public static readonly string ShotgunTowerSpriteSheet = "TowerShotgun";
        public static readonly float ShotgunTowerFireRate = 3f;
        public static readonly int ShotgunTowerHealth = 250;
        public static readonly BulletType ShotgunTowerBulletType = BulletType.ShotgunPellet;
        public static readonly int shotgunTowerAttackRange = 150;
        public static readonly int ShotgunTowerSpread = 15;
        public static readonly int shotgunTowerPelletAmount = 20;
        public static readonly int shotgunTowerAmount = 2;


        #endregion;

        #region SniperTower
        public static readonly int sniperTowerPrice = 100;
        public static readonly string sniperTowerSpriteSheet = "TowerSniper";
        public static readonly float sniperTowerFireRate = 4f;
        public static readonly int sniperTowerHealth = 100;
        public static readonly BulletType sniperTowerBulletType = BulletType.SniperBullet;
        public static readonly int sniperTowerAttackRange = 400;
        public static readonly int sniperTowerSpread = 1;
        public static readonly int sniperTowerAmount = 2;


        #region MachineGunTower
        public static readonly int machineGunTowerPrice = 100;
        public static readonly string machineGunTowerSpriteSheet = "TowerBasic";
        public static readonly float machineGunTowerFireRate = 0.2f;
        public static readonly int machineGunTowerHealth = 100;
        public static readonly BulletType machineGunTowerBulletType = BulletType.BasicBullet;
        public static readonly int machineGunTowerAttackRange = 200;
        public static readonly int machineGunTowerSpread = 10;
        public static readonly int machineGunTowerAmount = 2;

        #endregion;
        #endregion;
        #endregion

        #region Bullets
        #region BasicBullet
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 800;
        public static readonly float basicBulletLifeSpan = 0.5f;
        public static readonly int basicBulletDmg = 75;
        #endregion;

        #region BiggerBullet
        public static readonly string biggerBulletSheet = "BiggerBullet";
        public static readonly float biggerBulletMovementSpeed = 700;
        public static readonly float biggerBulletLifeSpan = 0.7f;
        public static readonly int biggerBulletDmg = 150;
        #endregion;

        #region sniperBullet
        public static readonly string sniperBulletSheet = "SniperBullet";
        public static readonly float sniperBulletMovementSpeed = 1100;
        public static readonly float sniperBulletLifeSpan = 0.7f;
        public static readonly int sniperBulletBulletDmg = 400;
        #endregion;

        #region shotgunPellet
        public static readonly string shotgunPelletSheet = "ShotgunPellet";
        public static readonly float shotgunPelletMovementSpeed = 750;
        public static readonly float shotgunPelletLifeSpan = 0.3f;
        public static readonly int shotgunPelletDmg = 30;
        #endregion;

        #region EnemyBullets
        #region SpitterBullet
        public static readonly string spitterBulletSheet = "SpitterBullet";
        public static readonly float spitterBulletMovementSpeed = 200;
        public static readonly float spitterBulletLifeSpan = 1f;
        public static readonly int spitterBulletDmg = 10;
        #endregion
        #endregion
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
        #region Melee
        #region BasicEnemy
        public readonly static int basicEnemyGold = 2;
        public static readonly string basicEnemySpriteSheet = "BasicEnemy";
        public static readonly int basicEnemyHealth = 200;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 0.7f;
        public static readonly int basicEnemyDamage = 10;
        public readonly static int basicEnemyAttackRadius = 100;

        #endregion
        #region BasicEliteEnemy
        public readonly static int basicEliteEnemyGold = 4;
        public static readonly string basicEliteEnemySpriteSheet = "BasicEliteEnemy";
        public static readonly int basicEliteEnemyHealth = 500;
        public static readonly float basicEliteEnemyMovementSpeed = 30;
        public static readonly float basicEliteEnemyAttackRate = 0.7f;
        public static readonly int basicEliteEnemyDamage = 15;
        public readonly static int basicEliteEnemyAttackRadius = 150;
        public static readonly float basicEliteSpawnModifier = 100;

        #endregion
        #endregion
        #region Ranged
        #region Spitter
        public readonly static int spitterGold = 2;
        public static readonly string spitterSpriteSheet = "SpitterEnemy";
        public static readonly int spitterHealth = 200;
        public static readonly float spitterMovementSpeed = 20;
        public static readonly float spitterAttackRate = 1.5f;
        public static readonly float spitterAttackRange = 150;
        public static readonly float spitterSpawnModifier = 200;

        public static readonly BulletType spitterBulletType = BulletType.SpitterBullet;
        public static readonly int spitterSpread = 4;


        #endregion
        #endregion
        #endregion

        #region Terrain
        public static readonly int spawnZoneSize = 100;
        public static readonly string rockImage = "Rock1";
        public static readonly int pushForce = 2;
        #endregion

        #region Crates
        public static readonly float crateLifeSpan = 10;
        public static readonly string crateSpriteSheet = "TestCrate";
        public static readonly string HealthCrateSpriteSheet = "CrateHealth";
        public static readonly string TowerCrateSpriteSheet = "CrateTower";
        public static readonly string GunCrateSpriteSheet = "CrateWeapon";
        public static readonly string MoneyCrateSpriteSheet = "CrateMoney";
        public static readonly int moneyCrateMoney = 100;
        public static readonly float crateSpawnDelay = 23;
        public static readonly int moneyCrateHealth = 100;

        #endregion
    }
}