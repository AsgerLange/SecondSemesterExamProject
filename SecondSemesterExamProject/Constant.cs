using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    enum VehicleType { None, Tank, Bike, Plane,
        MonsterVehicle
    }
    enum BulletType { BasicBullet, BiggerBullet, ShotgunPellet, SniperBullet, SpitterBullet, MonsterBullet };
    enum EnemyType { BasicEnemy, BasicEliteEnemy, Swarmer, Spitter, SiegebreakerEnemy };
    enum TowerType { BasicTower, ShotgunTower, SniperTower, MachineGunTower };
    enum CrateType { WeaponCrate, TowerCrate, MoneyCrate, HealthCrate };
    enum WeaponType { BasicWeapon, MachineGun, Shotgun, Sniper }


    class Constant
    {
        public readonly static int width = 1240;
        public readonly static int hight = 720;
        #region PVP
        public readonly static int maxDeaths = 5;
        public readonly static int pvpHealthModifier = 3;

        #endregion
        #region Menu
        public readonly static string menuBackGround = "Background1";
        public readonly static string gameBackGround = "Background1";
        public readonly static string titleFont = "MenuTitel";
        public readonly static string title = "The Last Stand";
        public readonly static string p1ControlImagePath = "p1Controls";
        public readonly static string p2ControlImagePath = "p2Controls";


        #endregion

        #region Button
        public readonly static string BlueButtonUpTexture = "ArrowBlueUp";
        public readonly static string BlueButtonDownTexture = "ArrowBlueDown";
        public readonly static string GreenButtonUpTexture = "ArrowGreenUp";
        public readonly static string GreenButtonDownTexture = "ArrowGreenDown";
        public readonly static string blueButtonTexture = "Button";
        public readonly static string RedButtonTexture = "Rutton";
        public readonly static string TexBoxButton = "TextBoxButton";
        public readonly static string buttonFont = "stat";
        public readonly static string startGameButton = "Start Game";
        #endregion

        #region Spawning
        public readonly static float singleSpawnDelay = 3f;
        public readonly static float waveSpawnDelay = 30;
        public readonly static int waveSizeVariable = 3;
        #endregion

        #region Vehicles

        public static readonly float buildTowerCoolDown = 0.25f;
        public static readonly int respawntime = 15;
        public static readonly int maxAmountOfVehicles = 2;
        public readonly static int aimLineLenght = 650;

        #region Tank
        public static readonly string tankSpriteSheet = "PlayerTank";
        public static readonly float tankMoveSpeed = 120;
        public static readonly int tankHealth = 500;
        public static readonly float tankRotateSpeed = 1.7F;
        public static readonly int tankStartGold = 150;
        #endregion
        #region Plane
        public static readonly string planeSpriteSheet = "PlayerPlane";
        public static readonly float planeMoveSpeed = 200;
        public static readonly int planeHealth = 150;
        public static readonly float planeRotateSpeed = 1.8F;
        public static readonly int planeStartGold = 150;
        #endregion;
        #region Bike
        public static readonly string bikeSpriteSheet = "PlayerBike";
        public static readonly float bikeMoveSpeed = 200;
        public static readonly int bikeHealth = 250;
        public static readonly float bikeRotateSpeed = 3f;
        public static readonly int bikeStartGold = 150;
        #endregion;
        #region MonsterVehicle
        public static readonly string monsterSpriteSheet = "MonsterVehicle";
        public static readonly float monsterMoveSpeed = 140;
        public static readonly int monsterHealth = 350;
        public static readonly float monsterRotateSpeed = 2.5f;
        public static readonly int monsterStartGold = 150;
        public static readonly int swarmerCost = 10;
        public static readonly int swarmerMaxAmount = 15; 

        public static readonly float monsterRegenRate = 0.5f; // seconds per life


        #endregion
        #endregion

        #region Tower
        public static readonly int maxTowerAmount = 20;

        #region HQ
        public static readonly string HQSpriteSheet = "HQ";
        public static readonly float HQFireRate = 2;
        public static readonly int HQHealth = 1000;
        public static readonly int HQAttackRange = 400;
        public static readonly BulletType HQbulletType = BulletType.BiggerBullet;
        public static readonly int HQSpread = 4;

        #endregion

        #region BasicTower
        public static readonly int basicTowerPrice = 150;
        public static readonly string basicTowerSpriteSheet = "TowerBasic";
        public static readonly float basicTowerFireRate = 3;
        public static readonly int basicTowerHealth = 160;
        public static readonly BulletType basicTowerBulletType = BulletType.BasicBullet;
        public static readonly int basicTowerAttackRange = 200;
        public static readonly int basicTowerSpread = 4;
        #endregion;

        #region ShotgunTower
        public static readonly int shotgunTowerPrice = 300;
        public static readonly string ShotgunTowerSpriteSheet = "TowerShotgun";
        public static readonly float ShotgunTowerFireRate = 1.9f;
        public static readonly int ShotgunTowerHealth = 350;
        public static readonly BulletType ShotgunTowerBulletType = BulletType.ShotgunPellet;
        public static readonly int shotgunTowerAttackRange = 140;
        public static readonly int ShotgunTowerSpread = 10;
        public static readonly int shotgunTowerPelletAmount = 10;
        public static readonly int shotgunTowerAmount = 2;
        #endregion;

        #region SniperTower
        public static readonly int sniperTowerPrice = 400;
        public static readonly string sniperTowerSpriteSheet = "TowerSniper";
        public static readonly float sniperTowerFireRate = 3f;
        public static readonly int sniperTowerHealth = 120;
        public static readonly BulletType sniperTowerBulletType = BulletType.SniperBullet;
        public static readonly int sniperTowerAttackRange = 400;
        public static readonly int sniperTowerSpread = 0;
        public static readonly int sniperTowerAmount = 2;
        #endregion

        #region MachineGunTower
        public static readonly int machineGunTowerPrice = 250;
        public static readonly string machineGunTowerSpriteSheet = "TowerMachineGun";
        public static readonly float machineGunTowerFireRate = 0.2f;
        public static readonly int machineGunTowerHealth = 120;
        public static readonly BulletType machineGunTowerBulletType = BulletType.BasicBullet;
        public static readonly int machineGunTowerAttackRange = 190;
        public static readonly int machineGunTowerSpread = 12;
        public static readonly int machineGunTowerAmount = 2;
        #endregion;
        #endregion;

        #region Bullets
        #region BasicBullet
        public static readonly string bulletSheet = "BasicBullet";
        public static readonly float basicBulletMovementSpeed = 800;
        public static readonly float basicBulletLifeSpan = 0.5f;
        public static readonly int basicBulletDmg = 65;
        #endregion;

        #region BiggerBullet
        public static readonly string biggerBulletSheet = "BiggerBullet";
        public static readonly float biggerBulletMovementSpeed = 700;
        public static readonly float biggerBulletLifeSpan = 0.8f;
        public static readonly int biggerBulletDmg = 190;
        #endregion;

        #region sniperBullet
        public static readonly string sniperBulletSheet = "SniperBullet";
        public static readonly float sniperBulletMovementSpeed = 1500;
        public static readonly float sniperBulletLifeSpan = 0.8f;
        public static readonly int sniperBulletBulletDmg = 700;
        #endregion;

        #region shotgunPellet
        public static readonly string shotgunPelletSheet = "ShotgunPellet";
        public static readonly float shotgunPelletMovementSpeed = 750;
        public static readonly float shotgunPelletLifeSpan = 0.25f;
        public static readonly int shotgunPelletDmg = 50;
        #endregion;

        #region MonsterBullet
        public static readonly string monsterBulletSheet = "MonsterShockwaveBullet";
        public static readonly float monsterBulletMovementSpeed = 360;
        public static readonly float monsterBulletLifeSpan = 0.2f;
        public static readonly int monsterBulletDmg = 250;
        #endregion
        #region EnemyBullets
        #region SpitterBullet
        public static readonly string spitterBulletSheet = "SpitterBullet";
        public static readonly float spitterBulletMovementSpeed = 210;
        public static readonly float spitterBulletLifeSpan = 0.95f;
        public static readonly int spitterBulletDmg = 10;
        #endregion
        #endregion
        #endregion;

        #region Weapons
        #region BasicWeapon
        public readonly static float basicWeaponFireRate = 0.7f;
        public readonly static int basicWeaponAmmo = int.MaxValue;
        public readonly static BulletType basicWeaponBulletType = BulletType.BiggerBullet;
        public readonly static int basicWeaponSpread = 1;
        #endregion;

        #region Sniper
        public readonly static float sniperFireRate = 1f;
        public readonly static int sniperAmmo = 30;
        public readonly static BulletType sniperBulletType = BulletType.SniperBullet;
        public readonly static int sniperSpread = 0;


        #endregion;

        #region Shotgun
        public readonly static float shotGunFireRate = 0.95f;
        public readonly static int shotGunAmmo = 25;
        public readonly static BulletType shotgunBulletType = BulletType.ShotgunPellet;
        public readonly static int shotGunSpread = 15;
        public static readonly int shotgunPelletAmount = 12;
        #endregion;

        #region MachineGun
        public readonly static float MachineGunFireRate = 0.07f;
        public readonly static int MachineGunGunAmmo = 500;
        public readonly static int MachineGunSpread = 7;
        public readonly static BulletType MachineGunBulletType = BulletType.BasicBullet;
        #endregion;

        #region MonsterWeapon
        public readonly static float monsterWeaponFireRate = 1f;
        public readonly static int monsterWeaponAmmo = int.MaxValue;
                public readonly static BulletType monsterWeaponBulletType = BulletType.MonsterBullet;
        public readonly static int monsterWeaponSpread = 0;
        #endregion
        #endregion;

        #region Enemies
        public static readonly int maxEnemyOnScreen = 120;
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
        public readonly static int basicEliteEnemyGold = 6;
        public static readonly string basicEliteEnemySpriteSheet = "BasicEliteEnemy";
        public static readonly int basicEliteEnemyHealth = 600;
        public static readonly float basicEliteEnemyMovementSpeed = 35;
        public static readonly float basicEliteEnemyAttackRate = 0.7f;
        public static readonly int basicEliteEnemyDamage = 15;
        public readonly static int basicEliteEnemyAttackRadius = 150;
        public static readonly float basicEliteSpawnWave = 15;
        public static readonly float basicEliteSpawnModifier = 100;
        #endregion
        #region SwarmerEnemy
        public readonly static int swarmerEnemyGold = 1;
        public static readonly string swarmerEnemySpriteSheet = "SwarmerEnemy";
        public static readonly int swarmerEnemyHealth = 30;
        public static readonly float swarmerEnemyMovementSpeed = 60;
        public static readonly float swarmerEnemyAttackRate = 0.6f;
        public static readonly int swarmerEnemyDamage = 5;
        public readonly static int swarmerEnemyAttackRadius = 150;
        public static readonly int swarmerSpawnMax = 3;
        public static readonly float swarmerSpawnWave = 3;
        public static readonly float swarmerSpawnModifier = 200;
        #endregion
        #region SiegebreakerEnemy
        public readonly static int siegeBreakerEnemyGold = 14;
        public static readonly string siegeBreakerSpriteSheet = "SiegebreakerEnemy";
        public static readonly int siegeBreakerEnemyHealth = 3000;
        public static readonly float siegeBreakerEnemyMovementSpeed = 40;
        public static readonly float siegeBreakerEnemyAttackRate = 0.9f;
        public static readonly int siegeBreakerEnemyDamage = 40;
        public readonly static int siegeBreakerEnemyAttackRadius = 200;
        public static readonly float siegeBreakerSpawnWave = 20;
        public static readonly float siegeBreakerSpawnModifier = 50;
        #endregion
        #endregion
        #region Ranged
        #region Spitter
        public readonly static int spitterGold = 4;
        public static readonly string spitterSpriteSheet = "SpitterEnemy";
        public static readonly int spitterHealth = 190;
        public static readonly float spitterMovementSpeed = 20;
        public static readonly float spitterAttackRate = 1.5f;
        public static readonly float spitterAttackRange = 150;
        public static readonly float spitterSpawnWave = 5;
        public static readonly float spitterSpawnModifier = 150;
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
        public static readonly float crateSpawnDelay = 17;
        public static readonly int healthCrate = 100;
        #endregion
    }
}