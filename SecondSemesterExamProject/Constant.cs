﻿using System;
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

        #region Spawning
        public readonly static float singleSpawnDelay = 3f;
        public readonly static float waveSpawnDelay = 30;
        public readonly static int waveSizeVariable = 3;
        #endregion

        #region Vehicles
        public static readonly float buildTowerCoolDown = 2;

        #region Tank
        public static readonly string tankSpriteSheet = "PlayerTank";
        public static readonly string tankSpriteSheet2 = "PlayerTank2";

        public static readonly float tankMoveSpeed = 100;
        public static readonly float tankFireRate = 0.7f;
        public static readonly int tankHealth = 500;
        public static readonly float tankRotateSpeed = 2.0F;
        public static readonly int tankStartGold = 100;
       
        #endregion
        #region Plane
        public static readonly string planeSpriteSheet = "PlayerTank";
        public static readonly string planeSpriteSheet2 = "PlayerTank2";

        public static readonly float planeMoveSpeed = 200;
        public static readonly float planeFireRate = 0.3f;
        public static readonly int planeHealth = 500;
        public static readonly float planeRotateSpeed = 1.5F;
        public static readonly int planeStartGold = 100;
        
        #endregion;
        #endregion

        #region Tower
        #region HQ
        public static readonly string HQSpriteSheet = "HQ";
        public static readonly float HQFireRate = 2;
        public static readonly int HQHealth = 1000;
        public static readonly int HQAttackRange = 450;
        public static readonly BulletType HQbulletType = BulletType.BasicBullet;
        #endregion

        #region BasicTower
        public static readonly int basicTowerPrice = 100;
        public static readonly string basicTowerSpriteSheet = "TowerBasic";
        public static readonly float basicTowerFireRate = 1;
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
        public static readonly int basicBulletDmg = 75;
        #endregion;

        #region BiggerBullet
        public static readonly string biggerBulletSheet = "BiggerBullet";
        public static readonly float biggerBulletMovementSpeed = 700;
        public static readonly float biggerBulletLifeSpan = 1.5f;
        public static readonly int biggerBulletDmg = 150;
        #endregion;

        #region shotgunPellet
        public static readonly string shotgunPelletSheet = "ShotgunPellet";
        public static readonly float shotgunPelletMovementSpeed = 750;
        public static readonly float shotgunPelletLifeSpan = 0.3f;
        public static readonly int shotgunPelletDmg = 30;
        #endregion;
        #endregion;

        #region Weapons
        #region BasicWeapon
        public readonly static float basicWeaponFireRate = 0.7f;
        public readonly static int basicWeaponAmmo = int.MaxValue;
        public readonly static BulletType basicWeaponBulletType = BulletType.BiggerBullet;
        public readonly static int basicWeaponSpread = 3;

        #endregion;

        #region Shotgun
        public readonly static float shotGunFireRate = 1f;
        public readonly static int shotGunAmmo = 50;
        public readonly static BulletType shotgunBulletType = BulletType.ShotgunPellet;
        public readonly static int shotGunSpread = 15;


        #endregion;
        #region MachineGun
        public readonly static float MachineGunFireRate = 0.1f;
        public readonly static int MachineGunGunAmmo = 10000;
        public readonly static int MachineGunSpread = 7;

        public readonly static BulletType MachineGunBulletType = BulletType.BasicBullet;

        #endregion;
        #endregion;

        #region Enemies
        public readonly static int baseEnemyGold = 2;
        #region BasicEnemy
        public static readonly string basicEnemySpriteSheet = "BasicEnemy";
        public static readonly int basicEnemyHealth = 200;
        public static readonly float basicEnemyMovementSpeed = 25;
        public static readonly float basicEnemyAttackRate = 0.7f;
        public static readonly int basicEnemyDamage = 10;

        #endregion
        #endregion

        #region Terrain
        public static readonly int spawnZoneSize = 100;
        public static readonly string rockImage = "TerrainRock";
        public static readonly int pushForce = 2;
        #endregion
    }
}
