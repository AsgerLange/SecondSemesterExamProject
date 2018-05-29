using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Ranged : Enemy
    {
        protected BulletType bulletType;
        protected float attackRange;
        protected int spread;
        protected bool isAttacking = false;

        public Ranged(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate, EnemyType enemyType, BulletType bulletType, float attackRange, int spread
            ) : base(gameObject, health, damage, movementSpeed, attackRate, enemyType)
        {
            this.bulletType = bulletType;
            this.attackRange = attackRange;
            this.spread = spread;
        }

        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
        {
            base.CreateAnimation();
        }

        /// <summary>
        /// loads the enemy
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        /// <summary>
        /// Override for Enemy.AI()
        /// </summary>
        public override void AI()
        {
            if (isAttacking == false)
            {
                base.AI();
            }
            Shoot();
        }

        protected virtual void Shoot()
        {

            if (attackTimeStamp + attackRate <= GameWorld.Instance.TotalGameTime)
            {
                Collider target;
                target = FindTargetInRange();
                if (target != null)
                {
                    Vector2 direction = new Vector2(target.CollisionBox.Center.X - GameObject.Transform.Position.X, target.CollisionBox.Center.Y - GameObject.Transform.Position.Y);

                    direction.Normalize();

                    float rotation = GetDegreesFromDestination(direction);

                    RotateToMatchDirection(direction);

                    BulletPool.CreateBullet(GameObject.Transform.Position, Alignment.Enemy,
                        bulletType, rotation + (GameWorld.Instance.Rnd.Next(-spread, spread)));

                    if (attackVariation > 2)//Adds animation variation
                    {
                        attackVariation = 1;
                    }
                    animator.PlayAnimation("Attack" + attackVariation);
                    attackVariation++;

                    attackTimeStamp = GameWorld.Instance.TotalGameTime;

                    isAttacking = true;
                }
                else
                {
                    isAttacking = false;
                }
            }
        }

        /// <summary>
        /// checks and returns the nearest target
        /// </summary>
        protected Collider FindTargetInRange()
        {
            Collider closestTarget = null;
            float distance = 0;
            bool otherIsBullet = false;

            lock (GameWorld.colliderKey)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other.GetAlignment == Alignment.Friendly)
                    {
                        if (AttackRadius.Contains(other.CollisionBox.Center))
                        {
                            foreach (Component comp in other.GameObject.GetComponentList)
                            {
                                if (comp is Bullet)
                                {
                                    otherIsBullet = true;
                                    break;
                                }

                            }
                            if (otherIsBullet == false)
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
        /// handles which animation should the tank be running
        /// </summary>
        /// <param name="animationName"></param>
        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        /// <summary>
        /// handles what the enemy does
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// handles what happens when the basicEnemy dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }
    }
}
