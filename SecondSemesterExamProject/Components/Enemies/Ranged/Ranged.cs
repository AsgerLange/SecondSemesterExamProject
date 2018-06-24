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

        public Ranged(GameObject gameObject, int health, float movementSpeed, float attackRate, float attackRange, EnemyType enemyType, BulletType bulletType, int spread, Alignment alignment
            ) : base(gameObject, health, movementSpeed, attackRate, attackRange, enemyType, alignment)
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
        protected override void FollowHQ()
        {
            if (playerSpawned)
            {
                targetGameObject = vehicleWhoSpawnedIt.GameObject;
            }
            else
            {
                base.FollowHQ();
            }

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
                bool isBullet = false;
                bool targetIsAlive = false;

                Collider target;
                target = FindTargetInRange();
                if (target != null)
                {
                    foreach (Component comp in target.GameObject.GetComponentList)
                    {
                        if (comp is Bullet)
                        {
                            isBullet = true;
                            break;
                        }
                        if (comp is Enemy)
                        {
                            targetIsAlive = (comp as Enemy).IsAlive;
                            break;
                        }
                        if (comp is Vehicle)
                        {
                            targetIsAlive = (comp as Vehicle).IsAlive;
                            break;
                        }
                        if (comp is Tower)
                        {
                            if ((comp as Tower).Health>0)
                            {
                                targetIsAlive = true;
                            }
                            break;
                        }


                    }
                    if (isBullet == false && targetIsAlive)
                    {

                        Vector2 direction = new Vector2(target.CollisionBox.Center.X - GameObject.Transform.Position.X, target.CollisionBox.Center.Y - GameObject.Transform.Position.Y);

                        direction.Normalize();

                        float rotation = GetDegreesFromDestination(direction);

                        RotateToMatchDirection(direction);

                        GameObject tmp = BulletPool.Instance.CreateBullet(GameObject, alignment,
                            bulletType, rotation + (GameWorld.Instance.Rnd.Next(-spread, spread)));

                        ChangeColorOnBullet(tmp);
                        if (attackVariation > 2)//Adds animation variation
                        {
                            attackVariation = 1;
                        }
                        if (isAlive)
                        {
                            animator.PlayAnimation("Attack" + attackVariation);
                        }

                        attackVariation++;

                        attackTimeStamp = GameWorld.Instance.TotalGameTime;

                        isAttacking = true;
                    }
                }
                else
                {
                    isAttacking = false;
                }
            }
        }

        private void ChangeColorOnBullet(GameObject tmp)
        {
            if (playerSpawned)
            {
                if (vehicleWhoSpawnedIt.Control == Controls.WASD)
                {
                    ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).color = Color.Cyan;
                }
                else
                {
                    ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).color = Color.Lime;
                }

            }
            else
            {
                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).color = Color.Red;
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
