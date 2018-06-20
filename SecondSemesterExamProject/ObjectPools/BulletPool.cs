using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TankGame
{

    class BulletPool
    {
        public static readonly object activeKey = new object();
        public static readonly object inActiveKey = new object();
        public static readonly object releaseKey = new object();

        private static Thread bulletPoolThread;

        //List containing active bullets
        private static List<GameObject> inActiveBullets = new List<GameObject>();

        //List containing inactive bullets
        private static List<GameObject> activeBullets = new List<GameObject>();

        //List containing bullets to be released
        public static List<GameObject> releaseList = new List<GameObject>();

        public static readonly object activeListKey = new object();
        public static readonly object inActiveListKey = new object();
        private static BulletPool instance;

        public static BulletPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BulletPool();
                }
                return instance;
            }
        }

        /// <summary>
        /// Get/set property for the activeBullets list
        /// </summary>
        public static List<GameObject> ActiveBullets
        {
            get
            {
                lock (activeListKey)
                {
                    return activeBullets;
                }
            }
            set
            {
                lock (activeListKey)
                {
                    activeBullets = value;
                }
            }
        }

        private BulletPool()
        {
            if (bulletPoolThread == null)
            {
                bulletPoolThread = new Thread(Update)
                {
                    IsBackground = true
                };
            }
            bulletPoolThread.Start();
        }

        private void Update()
        {
            while (GameWorld.Instance.gameRunning)
            {
                GameWorld.barrier.SignalAndWait();
                lock (activeListKey)
                {

                    lock (activeKey)
                    {
                        foreach (var go in ActiveBullets)
                        {
                            go.Update();
                        }
                    }
                }

                ReleaseList();
            }
        }

        /// <summary>
        /// Recycles old Bullet objects, or Creates new ones if the inactiveBullet list is empty
        /// </summary>
        /// <param name="position">The position where the bullet should spawn</param>
        /// <param name="alignment">The allignment of the bullet (Enemy/Friendly/neutral)</param>
        /// <returns></returns>
        public GameObject CreateBullet(GameObject gameObject, Alignment alignment, BulletType bulletType, float directionRotation)
        {
            Vehicle shooter = FindShooter(gameObject);


            IncrementBulletCounts(bulletType, shooter);

            if (inActiveBullets.Count > 0)
            {
                GameObject tmp = null;
                lock (inActiveListKey)
                {
                    foreach (GameObject bul in inActiveBullets)
                    {
                        foreach (Component comp in bul.GetComponentList)
                        {
                            if (comp is Bullet)
                            {
                                if (((Bullet)comp).GetBulletType == bulletType)
                                {

                                    tmp = bul;
                                    break;
                                }
                            }
                        }
                        if (tmp != null)
                        {
                            break;
                        }
                    }
                }
                if (tmp != null)
                {
                    lock (inActiveListKey)
                    {

                        inActiveBullets.Remove(tmp);
                    }

                    tmp.LoadContent(GameWorld.Instance.Content);


                    Component bullet = null;

                    foreach (Component comp in tmp.GetComponentList)
                    {
                        if (comp is Bullet)
                        {
                            bullet = comp;
                            break;
                        }
                    }

                    ((Bullet)bullet).DirRotation = directionRotation;
                    ((Bullet)bullet).SpriteRenderer.Rotation = directionRotation;

                    ((Bullet)bullet).CanRelease = true;
                    ((Bullet)bullet).ShouldDie = false;
                    ((Bullet)bullet).Shooter = shooter;


                    ((Bullet)bullet).TimeStamp = GameWorld.Instance.TotalGameTime;

                    lock (GameWorld.colliderKey)
                    {
                        ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;
                        ((Collider)tmp.GetComponent("Collider")).GetAlignment = alignment;
                        GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                    }
                    tmp.Transform.Position = gameObject.Transform.Position;

                    lock (activeListKey)
                    {

                        activeBullets.Add(tmp);
                    }


                    return tmp;
                }
                else
                {
                    tmp = GameObjectDirector.Instance.Construct(gameObject.Transform.Position, bulletType, directionRotation, alignment);

                    FindBullet(tmp).Shooter = shooter;

                    lock (activeListKey)
                    {

                        activeBullets.Add(tmp);
                    }


                    return tmp;
                }
            }
            else
            {
                GameObject tmp;

                tmp = GameObjectDirector.Instance.Construct(gameObject.Transform.Position, bulletType, directionRotation, alignment);
                FindBullet(tmp).Shooter = shooter;

                lock (activeListKey)
                {

                    activeBullets.Add(tmp);
                }


                return tmp;
            }
        }
        /// <summary>
        /// releases the bullet
        /// </summary>
        /// <param name="projectile"></param>
        /// <returns></returns>
        public static GameObject ReleaseBullet(GameObject projectile)
        {
            CleanUp(projectile);

            return projectile;
        }

        /// <summary>
        /// Cleans up the bullet (resets attributes)
        /// </summary>
        /// <param name="projectile"></param>
        public static void CleanUp(GameObject bullet)
        {
            //Reset all bullet attributes
            bullet.Transform.Position = new Vector2(100, 100);
            //  ((Collider)bullet.GetComponent("Collider")).EmptyLists();

            lock (GameWorld.colliderKey)
            {
                ((Collider)bullet.GetComponent("Collider")).DoCollsionChecks = false;
                GameWorld.Instance.Colliders.Remove((Collider)bullet.GetComponent("Collider"));
            }

            foreach (var component in bullet.GetComponentList)
            {
                if (component is Bullet)
                {
                    var tmp = component as Bullet;

                    tmp.DirRotation = 0;
                    tmp.IsRotated = false;

                    if (component is BasicBullet)
                    {
                        tmp = component as BasicBullet;
                        tmp.LifeSpan = Constant.basicBulletLifeSpan;
                        tmp.BulletDamage = Constant.basicBulletDmg;
                        tmp.MovementSpeed = Constant.basicBulletMovementSpeed;
                    }
                    else if (component is BiggerBullet)
                    {
                        tmp = component as BiggerBullet;
                        tmp.LifeSpan = Constant.biggerBulletLifeSpan;
                        tmp.BulletDamage = Constant.biggerBulletDmg;
                        tmp.MovementSpeed = Constant.biggerBulletMovementSpeed;
                    }
                    else if (component is ShotgunPellet)
                    {
                        tmp = component as ShotgunPellet;
                        tmp.LifeSpan = Constant.shotgunPelletLifeSpan;
                        tmp.BulletDamage = Constant.shotgunPelletDmg;
                        tmp.MovementSpeed = Constant.shotgunPelletMovementSpeed;
                    }
                    else if (component is SniperBullet)
                    {
                        tmp = component as SniperBullet;
                        tmp.LifeSpan = Constant.sniperBulletLifeSpan;
                        tmp.BulletDamage = Constant.sniperBulletBulletDmg;
                        tmp.MovementSpeed = Constant.sniperBulletMovementSpeed;
                    }
                    else if (component is SpitterBullet)
                    {
                        tmp = component as SpitterBullet;
                        tmp.LifeSpan = Constant.spitterBulletLifeSpan;
                        tmp.BulletDamage = Constant.spitterBulletDmg;
                        tmp.MovementSpeed = Constant.spitterBulletMovementSpeed;
                    }
                    break;
                }
            }
            lock (activeListKey)
            {
                ActiveBullets.Remove(bullet);
            }

            lock (inActiveListKey)
            {
                inActiveBullets.Add(bullet);
            }
        }

        /// <summary>
        /// Calls ReleaseBullet() on all bullets in ReleaseList
        /// </summary>
        public static void ReleaseList()
        {

            foreach (GameObject go in releaseList)
            {
                ReleaseBullet(go);
            }
            releaseList.Clear();
        }

        /// <summary>
        /// Returns the shooter (vehicle)of the bullet
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        private static Vehicle FindShooter(GameObject gameObject)
        {
            Vehicle shooter = null;

            foreach (Component comp in gameObject.GetComponentList)
            {
                if (comp is Vehicle)
                {
                    shooter = (comp as Vehicle);
                    break;
                }
            }

            return shooter;
        }

        private static Bullet FindBullet(GameObject gameObject)
        {
            Bullet bullet = null;
            foreach (Component comp in gameObject.GetComponentList)
            {
                if (comp is Bullet)
                {
                    bullet = (comp as Bullet);
                    break;
                }

            }
            return bullet;
        }
        /// <summary>
        /// Increments the appopriate bullet counter when shot is fired.
        /// </summary>
        /// <param name="type">Type of bullet that was fired</param>
        private static void IncrementBulletCounts(BulletType type, Vehicle vehicle)
        {
            if (vehicle != null)
            {
                switch (type)
                {
                    case BulletType.BasicBullet:
                        vehicle.Stats.BasicBulletCounter++;
                        break;
                    case BulletType.BiggerBullet:
                        vehicle.Stats.BiggerBulletCounter++;
                        break;
                    case BulletType.ShotgunPellet:
                        vehicle.Stats.ShotgunPelletsCounter++;
                        break;
                    case BulletType.SniperBullet:
                        vehicle.Stats.SniperBulletCounter++;
                        break;

                    default:
                        System.Diagnostics.Debug.WriteLine("Error in bullet pool IncrementBulletCounts()");
                        break;
                }
            }
            else if (type == BulletType.SpitterBullet)
            {
                Stats.SpitterBulletCounter++;
            }
        }
    }
}


