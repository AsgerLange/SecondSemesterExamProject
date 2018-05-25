using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TankGame;


namespace UnitTestSceondSemester
{
    [TestClass]
    public class TankGameTest
    {


        [TestInitialize]
        public void TestStart()
        {

        }

        [TestMethod]
        public void CreateGameWorldSingleton()
        {
            GameWorld gameWorld = GameWorld.Instance;

            Assert.IsFalse(gameWorld != null);
        }

        [TestMethod]

        public void GameObjectHasTransform()
        {
            GameObject go;

            go = new GameObject(); //Gameobject constructor adds a "Transform" component

            Assert.IsTrue(go.GetComponent("Transform") is Transform);
        }
        [TestMethod]
        public void ObjectDirectorBuildBullet()
        {
            Component test = GameObjectDirector.Instance.Construct(Vector2.Zero, BulletType.BasicBullet, 0, Alignment.Friendly);

            Assert.IsTrue(test.GameObject.GetComponent("Bullet") is Bullet);
        }
        public void PlayerTakesDamage()
        {


        }
        public void TowerTakesDamage()
        {


        }
       
        public void PlayerShoot()
        {


        }
        [TestMethod]
        public void CanCreateBullet() //Test if the damage of the bullet does that amount
        {

            BulletPool.CreateBullet(Vector2.Zero, Alignment.Friendly, BulletType.BasicBullet, 0);

            Assert.IsTrue(BulletPool.ActiveBullets.Count > 0);

        }
        public void BulletCollision() //Does the bullet collide with anything.
        {


        }
        public void TowerView() //Test if the towers can see the things around them.
        {


        }

        public void TowerShoot()
        {


        }

        public void EnemyDoDamage() //Test if enemy lowers the health of player to the amount that we know
        {


        }
        public void Collision() //To test if collision happends
        {


        }
        public void EnemyView() //For testing if enemy see's something that they will move towards it
        {


        }
        public void BuyTower()
        {


        }
        public void SupplyCrateSpawn() //Test if a SupplyCrate Spawns within the maps size.
        {


        }
        public void WeaponCrateSpawn()
        {


        }
        public void RandomWeapon() //Check what weapon the player got from a random between some numbers
        {


        }
        public void RandomGoldAmount() //Check the amount of gold from SupplyCrate between some numbers.
        {


        }

    }
}
