using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TankGame
{

    class EnemyPool
    {
        public static readonly object activeKey = new object();
        public static readonly object inActiveKey = new object();
        public static readonly object releaseKey = new object();
        //Private instance of the EnemyPool
        private static EnemyPool instance;
        //The enemyPool is in its own thread
        private static Thread enemyPoolThread;
        //List containing active Enemies
        private static List<GameObject> inActiveEnemies = new List<GameObject>();

        //List containing inactive enemies
        private static List<GameObject> activeEnemies = new List<GameObject>();

        //List containing enemies to be released
        private static List<GameObject> releaseList = new List<GameObject>();


        
        public static EnemyPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnemyPool();
                }
                return instance;
            }
        }

        /// <summary>
        /// creates the enemyPool and makes it its own thread
        /// </summary>
        private EnemyPool()
        {
            if (enemyPoolThread == null)
            {
                enemyPoolThread = new Thread(Update)
                {
                    IsBackground = true
                };
            }
            enemyPoolThread.Start();
        }

        /// <summary>
        /// keeps the enemies updated
        /// </summary>
        private void Update()
        {
            while (GameWorld.Instance.gameRunning)
            {
                GameWorld.barrier.SignalAndWait();
                lock (activeKey)
                {
                    foreach (var go in ActiveEnemies)
                    {
                        go.Update();
                    }
                }
                Release();
            }
        }

        /// <summary>
        /// Get/set property for the activeEnemies list
        /// </summary>
        public List<GameObject> ActiveEnemies
        {
            get
            {
                return activeEnemies;

            }
            set
            {
                lock (activeKey)
                {
                    activeEnemies = value;
                }
            }
        }

        /// <summary>
        /// Get/set property for the activeEnemies list
        /// </summary>
        private List<GameObject> InActiveEnemies
        {
            get
            {
                lock (inActiveKey)
                {
                    return inActiveEnemies;
                }
            }
            set
            {
                lock (inActiveKey)
                {
                    inActiveEnemies = value;
                }
            }
        }

        /// <summary>
        /// Get/set property for the releaseList list
        /// </summary>
        public List<GameObject> ReleaseList
        {
            get
            {
                lock (releaseKey)
                {
                    return releaseList;
                }
            }
            set
            {
                lock (releaseKey)
                {
                    releaseList = value;
                }
            }
        }

        /// <summary>
        /// Recycles old Enemy objects, or Creates new ones if the inactiveEnemy list is empty
        /// </summary>
        /// <param name="position"></param>
        /// <param name="enemyType"></param>
        /// <returns></returns>
        public GameObject CreateEnemy(Vector2 position, EnemyType enemyType)
        {
            if (inActiveEnemies.Count > 0)
            {
                GameObject tmp = null;
                lock (inActiveKey)
                {
                    foreach (GameObject en in inActiveEnemies)
                    {
                        foreach (Component comp in en.GetComponentList)
                        {
                            if (comp is Enemy)
                            {
                                if (((Enemy)comp).GetEnemyType == enemyType)
                                {
                                    tmp = en;
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
                    ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;

                    inActiveEnemies.Remove(tmp);

                    tmp.LoadContent(GameWorld.Instance.Content);

                    lock (GameWorld.colliderKey)
                    {
                        GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                    }
                    tmp.Transform.Position = position;

                    lock (activeKey)
                    {
                        ActiveEnemies.Add(tmp);
                    }

                    return tmp;
                }
                else
                {
                    tmp = GameObjectDirector.Instance.Construct(position, enemyType);
                    lock (activeKey)
                    {
                        ActiveEnemies.Add(tmp);
                    }

                    return tmp;
                }
            }
            else
            {
                GameObject tmp;

                tmp = GameObjectDirector.Instance.Construct(position, enemyType);
                lock (activeKey)
                {
                    ActiveEnemies.Add(tmp);
                }

                return tmp;
            }
        }
        /// <summary>
        /// releases the enemy
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        public GameObject ReleaseEnemy(GameObject enemy)
        {
            CleanUp(enemy);

            return enemy;
        }

        /// <summary>
        /// Cleans up the enemy (resets attributes)
        /// </summary>
        /// <param name="enemy"></param>
        public void CleanUp(GameObject enemy)
        {
            enemy.Transform.Position = new Vector2(100, 100);

            ((Collider)enemy.GetComponent("Collider")).DoCollsionChecks = false;

            lock (GameWorld.colliderKey)
            {
                GameWorld.Instance.Colliders.Remove((Collider)enemy.GetComponent("Collider"));
            }

            ((Animator)enemy.GetComponent("Animator")).PlayAnimation("Idle");

            foreach (var component in enemy.GetComponentList)
            {
                if (component is Enemy)
                {
                    var tmp = component as Enemy;

                    tmp.IsAlive = true;
                    tmp.CanRelease = true;

                    if (component is BasicEnemy)
                    {
                        tmp.Health = Constant.basicEnemyHealth;
                        tmp.MovementSpeed = Constant.basicEnemyMovementSpeed;
                    }

                    if (component is BasicEliteEnemy)
                    {
                        tmp.Health = Constant.basicEliteEnemyHealth;
                        tmp.MovementSpeed = Constant.basicEliteEnemyMovementSpeed;

                    }


                }
            }
            lock (activeKey)
            {
                ActiveEnemies.Remove(enemy);
            }
            InActiveEnemies.Add(enemy);
        }

        /// <summary>
        /// Calls ReleaseEnemy() on all enemies in ReleaseList
        /// </summary>
        public void Release()
        {
            lock (releaseKey)
            {
                foreach (GameObject go in releaseList)
                {
                    ReleaseEnemy(go);
                }
            }
            releaseList.Clear();
        }
    }
}
