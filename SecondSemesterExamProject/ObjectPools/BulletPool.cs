using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{

    static class BulletPool
    {
        //List containing active bullets
        private static List<GameObject> inActiveBullets = new List<GameObject>();

        //List containing inactive bullets
        private static List<GameObject> activeBullets = new List<GameObject>();

        //List containing bullets to be released
        public static List<GameObject> releaseList = new List<GameObject>();



        /// <summary>
        /// Get/set property for the activeBullets list
        /// </summary>
        public static List<GameObject> ActiveBullets
        {
            get { return activeBullets; }
            set { activeBullets = value; }
        }

        /// <summary>
        /// Recycles old Bullet objects, or Creates new ones if the inactiveBullet list is empty
        /// </summary>
        /// <param name="position">The position where the bullet should spawn</param>
        /// <param name="alignment">The allignment of the bullet (Enemy/Friendly/neutral)</param>
        /// <returns></returns>
        public static GameObject CreateBullet(Vector2 position, Alignment alignment, BulletType bulletType, float directionRotation)
        {
            if (inActiveBullets.Count > 0)
            {
                GameObject tmp = null;
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
                if (tmp != null)
                {
                    inActiveBullets.Remove(tmp);

                    tmp.LoadContent(GameWorld.Instance.Content);

                    ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;

                    Component bullet = null;
                    foreach (Component comp in tmp.GetComponentList)
                    {
                        if (comp is Bullet)
                        {
                            bullet = comp;
                            break;
                        }
                    }

                    ((Bullet)bullet).CanRelease = true;
                    ((Bullet)bullet).ShouldDie = false;


                    ((Bullet)bullet).DirRotation = directionRotation;


                    ((Bullet)bullet).TimeStamp = GameWorld.Instance.TotalGameTime;

                    lock (GameWorld.colliderKey)
                    {
                        GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                    }
                    tmp.Transform.Position = position;

                    activeBullets.Add(tmp);

                    return tmp;
                }
                else
                {
                    tmp = GameObjectDirector.Instance.Construct(position, bulletType, directionRotation, alignment);
                    activeBullets.Add(tmp);

                    return tmp;
                }
            }
            else
            {
                GameObject tmp;

                tmp = GameObjectDirector.Instance.Construct(position, bulletType, directionRotation, alignment);
                activeBullets.Add(tmp);

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
            ((Collider)bullet.GetComponent("Collider")).DoCollsionChecks = false;

            lock (GameWorld.colliderKey)
            {
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
                    break;
                }
            }
            ActiveBullets.Remove(bullet);
            inActiveBullets.Add(bullet);
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

    }

}
    

