using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TankGame;


namespace UnitTestSceondSemester
{
    [TestClass]
    public class TankGameTest
    {
        GameObject go;

        [TestInitialize]
        public void TestStart()
        {

        }

        [TestMethod]

        public void GameObjectHasTransform()
        {
            
            go = new GameObject(); //Gameobject constructor adds a "Transform" component

            Assert.IsTrue(go.GetComponent("Transform") is Transform);
        }
        [TestMethod]
        public void GameObjectIsGameObject()
        {
            if (go == null)
            {
                go = new GameObject();
            }
            Assert.IsTrue(go is GameObject);
        }
        [TestMethod]
        public void GameobjectsTransformHasPosition()
        {
            if (go == null)
            {
                go = new GameObject();
            }

            Vector2 result = go.Transform.Position;

            Assert.IsNull(result);
        }
        public void TowerTakesDamage()
        {


        }

        //[TestMethod]
        //public void CanCreateBullet() //Test if the damage of the bullet does that amount
        //{

        //    BulletPool.CreateBullet(Alignment.Friendly, BulletType.BasicBullet, 0);

        //    Assert.IsTrue(BulletPool.ActiveBullets.Count > 0);

        //}
        public void BulletCollision() //Does the bullet collide with anything.
        {


        }
        public void TowerView() //Test if the towers can see the things around them.
        {


        }

        public void TowerShoot()
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
