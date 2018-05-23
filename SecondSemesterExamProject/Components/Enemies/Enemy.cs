using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    enum EnemyType { BasicEnemy, };

    class Enemy : Component, IAnimatable, IUpdatable, ILoadable, ICollisionStay
    {

        public Animator animator;
        private SpriteRenderer spriteRenderer;
        protected GameObject targetGameObject = GameWorld.Instance.GameObjects[0]; //HQ by default
        protected float rotation = 0;
        protected float movementSpeed;
        protected float attackRate;
        protected bool isAlive;
        protected int health;
        protected int damage;
        protected float attackTimeStamp;
        private int attackVariation = 1;

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health <= 0)
                {
                    isAlive = false;
                    Die();
                }
            }
        }
        #region Attributes for object pool
        private bool canRelease;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;

        /// <summary>
        /// Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment">Alignment of the game object (enemy/friendly / neutral)</param>
        /// <param name="health">The amount of health the enemy should have</param>
        /// <param name="movementSpeed">Movement speed of the enemy</param>
        /// <param name="attackRate">the attackrate of the enemy</param>
        public Enemy(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate) : base(gameObject)
        {
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.attackRate = attackRate;
            this.damage = damage;
            this.isAlive = true;
            this.canRelease = true;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;


            FollowHQ();
        }

        /// <summary>
        /// finds the 
        /// </summary>
        private void FollowHQ()
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
        /// Loads sprites and animations
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("TPose");

        }

        /// <summary>
        /// The Standard behaviour of an Enemy
        /// </summary>
        public virtual void AI()
        {
            MoveTo(targetGameObject); //Enemy moves towards player1

            spriteRenderer.Rotation = rotation;//Rotates the sprite so it fits with the gameobject
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

                    GameWorld.Instance.Colliders.Remove((Collider)this.GameObject.GetComponent("Collider"));
                }

            }
        }

        /// <summary>
        /// Moves towards a targeted gameobject
        /// </summary>
        /// <param name="go"></param>
        private void MoveTo(GameObject go)
        {
            float x = go.Transform.Position.X;
            float y = go.Transform.Position.Y;

            Vector2 direction = new Vector2(x - this.GameObject.Transform.Position.X, y - this.GameObject.Transform.Position.Y);
            direction.Normalize();


            rotation = GetDegreesFromDestination(direction);

            TranslateMovement(direction);
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
        }

        /// <summary>
        /// creates an animation
        /// </summary>
        public virtual void CreateAnimation()
        {
            //Enemy Animation Set
            animator.CreateAnimation("TPose", new Animation(1, 0, 0, 23, 23, 3, Vector2.Zero));
        }

        /// <summary>
        /// handles animation for the Enemy
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            if (animationName == "Death")
            {
                if (canRelease)
                {
                    EnemyPool.releaseList.Add(this.GameObject);
                    canRelease = false;
                }
            }
            else
            {
                animator.PlayAnimation("Idle");
            }

        }

        /// <summary>
        /// handles what happens when an enemy dies
        /// </summary>
        protected virtual void Die()
        {
            foreach (GameObject go in GameWorld.Instance.GameObjects)
            {
                foreach (Component com in go.GetComponentList)
                {
                    if (com is Vehicle)
                    {
                        (com as Vehicle).Money += EnemyGold();
                        break;
                    }
                }
            }
            animator.PlayAnimation("Death");
        }

        /// <summary>
        /// returns the amount of gold the enemy gives
        /// </summary>
        /// <returns></returns>
        protected virtual int EnemyGold()
        {
            return Constant.baseEnemyGold;
        }

        /// <summary>
        /// when something is inside the enemy
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionStay(Collider other)
        {
            if (IsAlive)
            {

                if (other.GetAlignment != Alignment.Neutral)
                {
                    if (other.GetAlignment == Alignment.Friendly)
                    {
                        Attack(other);
                    }
                    float force = Constant.pushForce;

                    Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                    dir.Normalize();

                    other.GameObject.Transform.Translate(dir * force);
                }
            }
        }

        /// <summary>
        /// The standard overwritable attack method for all enemies
        /// </summary>
        /// <param name="other"></param>
        protected virtual void Attack(Collider other)
        {
            {//can enemy attack yet?
                if ((attackTimeStamp + attackRate) <= GameWorld.Instance.TotalGameTime)
                {
                    foreach (Component component in other.GameObject.GetComponentList)

                    {//does other object contain a vehicle?
                        if ((component is Vehicle && (component as Vehicle).Health > 0))
                        {
                            (component as Vehicle).Health -= damage; // damage vehicle

                            if (attackVariation > 2)//Adds animation variation
                            {
                                attackVariation = 1;
                            }
                            animator.PlayAnimation("Attack" + attackVariation);
                            attackVariation++;

                            attackTimeStamp = GameWorld.Instance.TotalGameTime; //determines the next time an enemy can attack
                            break;
                        }

                        if ((component is Tower && (component as Tower).Health > 0))
                        {

                            (component as Tower).Health -= damage;  //damage Tower

                            if (attackVariation > 2)//Adds animation variation
                            {
                                attackVariation = 1;
                            }
                            animator.PlayAnimation("Attack" + attackVariation);
                            attackVariation++;

                            attackTimeStamp = GameWorld.Instance.TotalGameTime;
                            break;
                        }

                    }
                }
            }
        }

    }
}


