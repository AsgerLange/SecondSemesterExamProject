using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class Tower : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter
    {
        protected int health;
        float attackRate;
        float shootRotation = 0;
        float attackRange;
        float shootTimeStamp;
        protected SpriteRenderer spriteRenderer;
        public Animator animator;

        public Tower(GameObject gameObject, float attackRate, int health, float attackRange) : base(gameObject)
        {
            this.health = health;
            this.attackRate = attackRate;
            this.attackRange = attackRange;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }


        /// <summary>
        /// handles animation
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            Console.WriteLine(new NotImplementedException("OnAnimationDone Tower"));
        }

        /// <summary>
        /// handles what happens when an enemy enters
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionEnter(Collider other)
        {
            Console.WriteLine(new NotImplementedException("OnCollisionEnter Tower"));
        }

        /// <summary>
        /// handles what the tower does
        /// </summary>
        public virtual void Update()
        {

            Shoot();
        }

        protected virtual void Shoot()
        {
            if (shootTimeStamp + attackRate <= GameWorld.Instance.DeltaTime)
            {
                Collider target;
                target = FindEnemiesInRange();

                Vector2 direction = new Vector2(target.CollisionBox.Center.X - GameObject.Transform.Position.X, target.CollisionBox.Center.Y - GameObject.Transform.Position.Y);
                direction.Normalize();

                float rotation = GetDegreesFromDestination(direction);
                BulletPool.CreateBullet(GameObject.Transform.Position, Alignment.Friendly, BulletType.BaiscBullet);
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
        /// checks and returns the nearest enemy
        /// </summary>
        protected Collider FindEnemiesInRange()
        {
            Collider closestEnemy = null;
            float distance = 0;
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other.GetAlignment == Alignment.Enemy)
                {
                    if (AttackRadius.Contains(other.CollisionBox.Center))
                    {
                        float otherDistance;
                        otherDistance = ((GameObject.Transform.Position.X - other.CollisionBox.Center.X)
                            * (GameObject.Transform.Position.X - other.CollisionBox.Center.X)
                            + (GameObject.Transform.Position.Y - other.CollisionBox.Center.Y)
                            * (GameObject.Transform.Position.Y - other.CollisionBox.Center.Y));
                        if (closestEnemy == null)
                        {
                            closestEnemy = other;
                            distance = otherDistance;
                        }
                        else if (distance > otherDistance)
                        {
                            closestEnemy = other;
                            distance = otherDistance;
                        }
                    }
                }
            }
            return closestEnemy;
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
        /// loads Tower Content
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }

        /// <summary>
        /// creates the different animations for the tower
        /// </summary>
        protected virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 10, 10, 3, Vector2.Zero));
        }
    }
}
