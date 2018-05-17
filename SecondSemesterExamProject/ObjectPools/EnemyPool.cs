using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{

    static class EnemyPool
    {
        //List containing active Enemies
        private static List<GameObject> inActiveEnemies = new List<GameObject>();

        //List containing inactive enemies
        private static List<GameObject> activeEnemies = new List<GameObject>();

        //List containing enemies to be released
        public static List<GameObject> releaseList = new List<GameObject>();

        

        /// <summary>
        /// Get/set property for the activeEnemies list
        /// </summary>
        public static List<GameObject> ActiveEnemies
        {
            get { return activeEnemies; }
            set { activeEnemies = value; }
        }

        /// <summary>
        /// Recycles old Enemy objects, or Creates new ones if the inactiveEnemy list is empty
        /// </summary>
        /// <param name="position"></param>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public static GameObject CreateEnemy(Vector2 position, EnemyType enemyType)
        {
            if (inActiveEnemies.Count > 0)
            {
                GameObject tmp;

                tmp = inActiveEnemies[0];

                inActiveEnemies.Remove(tmp);

                tmp.LoadContent(GameWorld.Instance.Content);

                ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;

                ((Enemy)tmp.GetComponent("Enemy")).CanRelease = true;

                GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                tmp.Transform.Position = position;

                activeEnemies.Add(tmp);

                return tmp;
            }
            else
            {
                GameObject tmp;

              
                tmp = GameObjectDirector.Instance.Construct(position, enemyType);
                tmp.LoadContent(GameWorld.Instance.Content);
                activeEnemies.Add(tmp);

                return tmp;
            }
        }
        /// <summary>
        /// releases the enemy
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public static GameObject ReleaseEnemy(GameObject enemy)
        {
            CleanUp(enemy);

            return enemy;
        }

        /// <summary>
        /// Cleans up the enemy (resets attributes)
        /// </summary>
        /// <param name="enemy"></param>
        public static void CleanUp(GameObject enemy)
        {
            //Reset all enemy attrubutes
        }

        /// <summary>
        /// Calls ReleaseEnemy() on all enemies in ReleaseList
        /// </summary>
        public static void ReleaseList()
        {

            foreach (GameObject go in releaseList)
            {

                ReleaseEnemy(go);
            }
            releaseList.Clear();
        }

    }

}
