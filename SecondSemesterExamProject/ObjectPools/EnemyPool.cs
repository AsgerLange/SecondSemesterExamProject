using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        private static Queue<GameObject> enemiesWaitingToBeSpawned = new Queue<GameObject>();



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
        public GameObject CreateEnemy(Vector2 position, EnemyType enemyType, Alignment alignment)
        {
            if (enemiesWaitingToBeSpawned.Count > 0 && activeEnemies.Count < Constant.maxEnemyOnScreen)
            {
                int deQueueAmount = 0;

                deQueueAmount = enemiesWaitingToBeSpawned.Count;
                if (Constant.maxEnemyOnScreen - ActiveEnemies.Count < deQueueAmount)
                {
                    deQueueAmount = Constant.maxEnemyOnScreen - activeEnemies.Count;
                }

                for (int i = 0; i < deQueueAmount; i++)
                {

                    GameObject tmp;

                    tmp = enemiesWaitingToBeSpawned.Dequeue();

                    lock (GameWorld.colliderKey)
                    {
                        ((Collider)tmp.GetComponent("Collider")).GetAlignment = alignment;

                        GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                        ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;
                    }

                    AddEnemy(tmp, alignment);
                }
            }
            if (inActiveEnemies.Count > 0 &&
                 activeEnemies.Count <= Constant.maxEnemyOnScreen &&
                 enemiesWaitingToBeSpawned.Count == 0)
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

                    inActiveEnemies.Remove(tmp);

                    tmp.LoadContent(GameWorld.Instance.Content);

                    lock (GameWorld.colliderKey)
                    {
                        ((Collider)tmp.GetComponent("Collider")).GetAlignment = alignment;
                        ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = true;

                        GameWorld.Instance.Colliders.Add((Collider)tmp.GetComponent("Collider"));
                    }
                    tmp.Transform.Position = position;

                    AddEnemy(tmp, alignment);

                    return tmp;
                }
                else
                {
                    tmp = GameObjectDirector.Instance.Construct(position, enemyType, alignment);

                    AddEnemy(tmp, alignment);


                    return tmp;
                }
            }

            else
            {
                GameObject tmp;

                tmp = GameObjectDirector.Instance.Construct(position, enemyType, alignment);

                AddEnemy(tmp, alignment);


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


            lock (GameWorld.colliderKey)
            {
                ((Collider)enemy.GetComponent("Collider")).DoCollsionChecks = false;
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
                    tmp.CanAttackPlane = false;
                    tmp.playerSpawned = false;

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

                    if (component is SwarmerEnemy)
                    {
                        tmp.Health = Constant.swarmerEnemyHealth;
                        tmp.MovementSpeed = Constant.swarmerEnemyMovementSpeed;

                    }

                    if (component is SiegebreakerEnemy)
                    {
                        tmp.Health = Constant.siegeBreakerEnemyHealth;
                        tmp.MovementSpeed = Constant.siegeBreakerEnemyMovementSpeed;

                    }

                    if (component is Spitter)
                    {
                        tmp.Health = Constant.spitterHealth;
                        tmp.MovementSpeed = Constant.spitterMovementSpeed;

                        tmp.CanAttackPlane = true;
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

        /// <summary>
        /// Adds the enemy to the queue or to the game depending on how many is in game already
        /// </summary>
        /// <param name="tmp"></param>
        private void AddEnemy(GameObject tmp, Alignment alignment)
        {

            bool spitter = false;
            bool playerSpawned = false;


            foreach (Component comp in tmp.GetComponentList)
            {
                if (comp is Enemy)
                {
                    playerSpawned = (comp as Enemy).playerSpawned;
                    (comp as Enemy).Alignment = alignment;


                }
                if (comp is Spitter)
                {
                    spitter = true;
                    break;
                }
            }

            if (spitter == false && playerSpawned == false)
            {
                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).color = Color.Red;
            }
            else
            {
                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).Sprite = GameWorld.Instance.Content.Load<Texture2D>("SpitterEnemy");

            }

            lock (activeKey)
            {
                if (activeEnemies.Count < Constant.maxEnemyOnScreen)
                {

                    ActiveEnemies.Add(tmp);
                }
                else
                {
                    lock (GameWorld.colliderKey)
                    {
                        ((Collider)tmp.GetComponent("Collider")).DoCollsionChecks = false;
                        GameWorld.Instance.Colliders.Remove((Collider)tmp.GetComponent("Collider"));
                    }
                    enemiesWaitingToBeSpawned.Enqueue(tmp);
                }
            }
        }
    }
}
