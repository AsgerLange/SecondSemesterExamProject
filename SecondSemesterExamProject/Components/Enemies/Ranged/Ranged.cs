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
        protected int spread;
        protected bool isAttacking = false;

        public Ranged(GameObject gameObject, int health, float movementSpeed, float attackRate,float attackRange, EnemyType enemyType, BulletType bulletType,  int spread
            ) : base(gameObject, health, movementSpeed, attackRate,attackRange, enemyType)
        {
            this.CanAttackPlane = true;
            this.bulletType = bulletType;
            this.attackRange = attackRange;
            this.spread = spread;
        }

        /// <summary>
        /// Creates the animations
        /// </summary>
        protected override void CreateAnimation()
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
                                    
                    BulletPool.CreateBullet(GameObject, Alignment.Enemy,
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
