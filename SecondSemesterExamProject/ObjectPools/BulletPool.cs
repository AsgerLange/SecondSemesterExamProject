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
        public static GameObject CreateBullet(Vector2 position, Alignment alignment)
        {
            if (inActiveBullets.Count > 0)
            {
                GameObject tmp;

                tmp = inActiveBullets[0];

                inActiveBullets.Remove(tmp);

                tmp.LoadContent(GameWorld.Instance.Content);

                ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;

                ((Bullet)tmp.GetComponent("Bullet")).CanRelease = true;

                GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                tmp.Transform.Position = position;

                activeBullets.Add(tmp);

                return tmp;
            }
            else
            {
                GameObject tmp;


                tmp = GameObjectDirector.Instance.Construct(position, BuilderType.BulletBuilder);
                tmp.LoadContent(GameWorld.Instance.Content);
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
           // ((Collider)bullet.GetComponent("Collider")).EmptyLists();
            ((Collider)bullet.GetComponent("Collider")).DoCollsionChecks = false;
            GameWorld.Instance.Colliders.Remove((Collider)bullet.GetComponent("Collider"));
            //((Bullet)bullet.GetComponent("Bullet")).Speed = Constant.baseProjectileSpeed;

            activeBullets.Remove(bullet);
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
