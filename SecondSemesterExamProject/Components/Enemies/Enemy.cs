using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace TankGame
{

    class Enemy : Component, IAnimatable, IUpdatable, ILoadable, ICollisionStay
    {
        public bool playerSpawned = false;

        public Animator animator;
        protected EnemyType enemyType;
        private SpriteRenderer spriteRenderer;
        protected GameObject targetGameObject;
        protected float rotation = 0;
        protected float movementSpeed;
        protected float attackRate;
        protected bool isAlive;
        protected int health;
        protected int damage;
        protected float attackTimeStamp;
        protected int attackVariation = 1;
        protected float attackRange;
        protected SoundEffect deathSound;
        protected bool canAttackPlane = false;
        protected Alignment alignment;

        protected MonsterVehicle vehicleWhoSpawnedIt;

        public MonsterVehicle VehicleWhoSpawnedIt
        {
            get { return vehicleWhoSpawnedIt; }
            set { vehicleWhoSpawnedIt = value; }
        }
        public Alignment Alignment
        {
            get { return alignment; }
            set { alignment = value; }
        }

        public EnemyType GetEnemyType
        {
            get { return enemyType; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        public bool CanAttackPlane
        {
            get { return canAttackPlane; }
            set { canAttackPlane = value; }
        }
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health <= 0)
                {
                    if (isAlive)
                    {
                        PlayDeathSound();
                        isPlayingAnimation = true;
                        isAlive = false;
                        animator.PlayAnimation("Death");
                        health = 0;
                    }
                }
            }
        }
        public float AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
        }
        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        #region Attributes for object pool
        private bool canRelease;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }

        protected bool isPlayingAnimation = false;
        #endregion;

        /// <summary>
        /// Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment">Alignment of the game object (enemy/friendly / neutral)</param>
        /// <param name="health">The amount of health the enemy should have</param>
        /// <param name="movementSpeed">Movement speed of the enemy</param>
        /// <param name="attackRate">the attackrate of the enemy</param>
        public Enemy(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate, float attackRange, EnemyType enemyType, Alignment alignment) : base(gameObject)
        {
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.attackRate = attackRate;
            this.damage = damage;
            this.isAlive = true;
            this.attackRange = attackRange;
            this.canRelease = true;
            this.enemyType = enemyType;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
            this.alignment = alignment;
            FollowHQ();
        }
        /// <summary>
        /// Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment">Alignment of the game object (enemy/friendly / neutral)</param>
        /// <param name="health">The amount of health the enemy should have</param>
        /// <param name="movementSpeed">Movement speed of the enemy</param>
        /// <param name="attackRate">the attackrate of the enemy</param>
        public Enemy(GameObject gameObject, int health, float movementSpeed, float attackRate, float attackRange, EnemyType enemyType, Alignment alignment) : base(gameObject)
        {
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.attackRate = attackRate;
            this.isAlive = true;
            this.canRelease = true;
            this.enemyType = enemyType;
            this.attackRange = attackRange;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
            this.alignment = alignment;


        }

        /// <summary>
        /// finds the 
        /// </summary>
        protected virtual void FollowHQ()
        {

            foreach (var go in GameWorld.Instance.GameObjects)
            {
                if (go.GetComponent("HQ") is HQ)
                {
                    targetGameObject = go;
                    break;
                }
            }
        }
        /// <summary>
        /// Rotates the Enemy to match direction
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void RotateToMatchDirection(Vector2 direction)
        {
            this.rotation = GetDegreesFromDestination(direction);

            spriteRenderer.Rotation = rotation;
        }

        /// <summary>
        /// Loads sprites and animations
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {



            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("TPose");

            deathSound = content.Load<SoundEffect>("EnemyDeath");

        }

        /// <summary>
        /// The Standard behaviour of an Enemy
        /// </summary>
        public virtual void AI()
        {
            MoveTo(targetGameObject);

            ChangeTargetToNearestTarget();
        }

        /// <summary>
        /// updates the Enemy
        /// </summary>
        public virtual void Update()
        {
            if (isAlive)
            {
                AI();
            }
            else
            {
                if (this.GameObject.GetComponent("Collider") is Collider)
                {
                    lock (GameWorld.colliderKey)
                    {
                        GameWorld.Instance.Colliders.Remove((Collider)this.GameObject.GetComponent("Collider"));
                    }
                }

            }
        }

        /// <summary>
        /// Moves towards a targeted gameobject
        /// </summary>
        /// <param name="go"></param>
        private void MoveTo(GameObject go)
        {
            if (go != null)
            {

                float x = go.Transform.Position.X - 1;
                float y = go.Transform.Position.Y - 1; //-1 because gameobjects disappear if they're directly on top of another

                Vector2 direction = new Vector2(x - this.GameObject.Transform.Position.X, y - this.GameObject.Transform.Position.Y);

                direction.Normalize();

                RotateToMatchDirection(direction);

                TranslateMovement(direction);
            }
            else
            {
                FollowHQ();
            }
        }

        /// <summary>
        /// Calculates the degrees from standart vector to destination vector.
        /// </summary>
        /// <param name="vector">The direction the enemy is moving</param>
        protected float GetDegreesFromDestination(Vector2 destinationVec)
        {
            Vector2 positionVec = new Vector2(0, -1); //Standard position (UP)

            float toppart = 0;

            toppart += positionVec.X * destinationVec.X;
            toppart += positionVec.Y * destinationVec.Y;

            float destinationVector2 = 0; //destinationVec squared
            float positionVector2 = 0; //positionVec squared

            destinationVector2 += positionVec.X * positionVec.X;
            destinationVector2 += positionVec.Y * positionVec.Y;

            positionVector2 += destinationVec.X * destinationVec.X;
            positionVector2 += destinationVec.Y * destinationVec.Y;

            float bottompart = 0;
            bottompart = (float)Math.Sqrt(destinationVector2 * positionVector2);


            double returnValue = (float)Math.Acos(toppart / bottompart);

            returnValue *= 360.0 / (2 * Math.PI); //Coverts the radian to degrees

            if (destinationVec.X < 0)
            {
                returnValue -= (returnValue * 2);
            }

            return (float)returnValue;
        }

        /// <summary>
        /// Makes the Enemy actually move
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="movementSpeed"></param>
        public void TranslateMovement(Vector2 translation)
        {
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);

            if (isPlayingAnimation == false && isAlive)
            {
                if (isAlive)
                {
                    animator.PlayAnimation("Walk");
                }
                isPlayingAnimation = true;
            }
        }

        /// <summary>
        /// creates an animation
        /// </summary>
        protected virtual void CreateAnimation()
        {
            //Enemy Animation Set
            animator.CreateAnimation("TPose", new Animation(1, 0, 0, 23, 23, 3, Vector2.Zero)); //Please note that this only displays correctly for Basic Enemy and is only a failure detection measure
        }

        /// <summary>
        /// handles animation for the Enemy
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            if (animationName == "Death")
            {
                Die();
            }

            if (animationName == "Walk" && IsAlive)
            {
                if (isPlayingAnimation == true)
                {
                    isPlayingAnimation = false;
                }
            }
            else
            {
                if (IsAlive)
                {

                    animator.PlayAnimation("Idle");
                    isPlayingAnimation = false;
                }
            }

        }

        /// <summary>
        /// handles what happens when an enemy dies
        /// </summary>
        protected virtual void Die()
        {
            if (playerSpawned)
            {
                vehicleWhoSpawnedIt.EnemyCount--;
            }

            if (canRelease)
            {
                EnemyPool.Instance.ReleaseList.Add(this.GameObject);
                canRelease = false;

                if (playerSpawned == false)
                {
                    IncrementEnemyDeaths();


                    foreach (GameObject go in GameWorld.Instance.GameObjects)
                    {
                        foreach (Component com in go.GetComponentList)
                        {
                            if (com is Vehicle)
                            {
                                int moneyReward;
                                //Balancing gold income, to limit tower building in multiplayer
                                try
                                {
                                    moneyReward = (EnemyGold() / GameWorld.Instance.PlayerAmount);
                                }
                                catch (Exception)
                                {
                                    moneyReward = 1;

                                }

                                (com as Vehicle).Money += moneyReward;

                                (com as Vehicle).Stats.TotalAmountOfGold += moneyReward;

                                break;
                            }
                        }
                    }
                }
            }

        }
        /// <summary>
        /// Changes target to the nearest target 
        /// </summary>
        protected virtual void ChangeTargetToNearestTarget()
        {
            Collider target;

            target = FindTargetInRange();

            if (target != null)
            {
                if (targetGameObject.GetComponent("Collider") != target)
                {
                    this.targetGameObject = target.GameObject;

                }
            }
            else
            {
                FollowHQ();

            }

        }
        /// <summary>
        /// Increments deathCounters in Stats, depending on what kind of enemy dies
        /// </summary>
        private void IncrementEnemyDeaths()
        {
            switch (enemyType)
            {
                case EnemyType.BasicEnemy:
                    Stats.BasicEnemyKilled++;
                    break;

                case EnemyType.BasicEliteEnemy:
                    Stats.BasicEliteEnemyKilled++;
                    break;

                case EnemyType.Spitter:
                    Stats.SpitterKilled++;
                    break;

                case EnemyType.Swarmer:
                    Stats.SwarmerKilled++;
                    break;

                case EnemyType.SiegebreakerEnemy:
                    Stats.SiegeBreakerKilled++;
                    break;

                default:
                    System.Diagnostics.Debug.WriteLine("error incrementenemydeath");
                    break;
            }
        }

        /// <summary>
        /// returns the amount of gold the enemy gives
        /// </summary>
        /// <returns></returns>
        protected virtual int EnemyGold()
        {

            return Constant.basicEnemyGold;
        }
        /// <summary>
        /// checks and returns the nearest target
        /// </summary>
        protected Collider FindTargetInRange()
        {
            Collider closestTarget = null;
            Collider thisCollider = (Collider)GameObject.GetComponent("Collider");
            float distance = 0;

            bool otherIsBullet = false;
            bool otherIsPlane = false;


            lock (GameWorld.colliderKey)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other.GetAlignment != thisCollider.GetAlignment
                        && other.GetAlignment != Alignment.Neutral)
                    {
                        if (AttackRadius.Contains(other.CollisionBox.Center))
                        {
                            foreach (Component comp in other.GameObject.GetComponentList)
                            {
                                if (comp is Bullet)
                                {
                                    otherIsBullet = true;


                                }
                                if (comp is Plane)
                                {

                                    otherIsPlane = true;

                                }

                            }
                            if (otherIsBullet == false)
                            {
                                if (otherIsPlane == false || (otherIsPlane && this.canAttackPlane))
                                {


                                    float otherDistance;
                                    otherDistance = ((GameObject.Transform.Position.X - other.CollisionBox.Center.X)
                                        * (GameObject.Transform.Position.X - other.CollisionBox.Center.X)
                                        + (GameObject.Transform.Position.Y - other.CollisionBox.Center.Y)
                                        * (GameObject.Transform.Position.Y - other.CollisionBox.Center.Y));
                                    if (closestTarget == null)
                                    {
                                        closestTarget = other;
                                        distance = otherDistance;
                                    }
                                    else if (distance > otherDistance)
                                    {
                                        closestTarget = other;
                                        distance = otherDistance;
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return closestTarget;
        }
        /// <summary>
        /// creates a circle with the attackrange
        /// </summary>
        protected Circle AttackRadius
        {
            get
            {
                return new Circle(GameObject.Transform.Position, attackRange);
            }
        }
        /// <summary>
        /// when something is inside the enemy
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionStay(Collider other)
        {
            bool push = true;
            Collider thisCollider = (Collider)GameObject.GetComponent("Collider");

            if (other.GetAlignment != Alignment.Neutral)
            {
                if (IsAlive)
                {
                    if (!(other.GameObject.GetComponent("Plane") is Plane))
                    {
                        InteractionOnCollision(other);

                        foreach (Component go in other.GameObject.GetComponentList)
                        {
                            if (go is Bullet)
                            {
                                push = false; //makes sure enemies don't push allied bullets away
                                break;
                            }
                            if (go is Vehicle)
                            {
                                push = false;
                                break;
                            }
                        }

                        if (other.GetAlignment == thisCollider.GetAlignment && push || push && playerSpawned)
                        {
                            float force = Constant.pushForce;

                            Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                            dir.Normalize();

                            other.GameObject.Transform.Translate(dir * force);
                        }
                    }
                }
            }
        }
        /// <summary>
        ///  an enemy's interaction when colliding with "Friendly" gameobjects
        /// </summary>
        /// <param name="other"></param>
        protected virtual void InteractionOnCollision(Collider other)
        {
            //Overwrited by melee enemies
        }
        /// <summary>
        /// plays the death sound effect
        /// </summary>
        protected virtual void PlayDeathSound()
        {
            deathSound.Play(0.6f, 0, 0);
        }
    }
}



