﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace TankGame
{

    class Tower : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter, ICollisionStay
    {

        protected int health;
        protected float attackRate;
        protected float attackRange;
        protected float shootTimeStamp;
        protected int spread;
        protected bool isAlive;
        protected SpriteRenderer spriteRenderer;
        public Animator animator;
        protected BulletType bulletType;
        protected SoundEffect dieSoundEffect;
        protected SoundEffect shootSound;



        /// <summary>
        /// Property for health, kills tower, if HP < zero
        /// </summary>
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

                        health = 0;
                        isAlive = false;
                        if (dieSoundEffect != null)
                        {
                            dieSoundEffect.Play();
                        }
                        animator.PlayAnimation("Death");
                    }
                }
            }
        }
        /// <summary>
        /// tower construcor
        /// </summary>
        /// <param name="gameObject"></param>
        public Tower(GameObject gameObject) : base(gameObject)
        {
            GameObject.Transform.canMove = false;
            isAlive = true;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        /// <summary>
        /// handles animation
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {

            if (animationName == "Death")
            {
                Die();
            }
        }

        /// <summary>
        /// handles what the tower does
        /// </summary>
        public virtual void Update()
        {

            Shoot();
        }

        /// <summary>
        /// Standard shooting behaviour for all towers
        /// </summary>
        protected virtual void Shoot()
        {
            if (shootTimeStamp + attackRate <= GameWorld.Instance.TotalGameTime)
            {
                Collider target;
                target = FindEnemiesInRange();
                if (target != null)
                {
                    Vector2 direction = new Vector2(target.CollisionBox.Center.X - GameObject.Transform.Position.X, target.CollisionBox.Center.Y - GameObject.Transform.Position.Y);
                    direction.Normalize();

                    float rotation = GetDegreesFromDestination(direction);
                    BulletPool.CreateBullet(GameObject, Alignment.Friendly, bulletType, rotation + (GameWorld.Instance.Rnd.Next(-spread, spread)));
                    shootTimeStamp = GameWorld.Instance.TotalGameTime;
                    PlayShootSoundEffect();
                }
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
            bool otherIsBullet = false;
            lock (GameWorld.colliderKey)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other.GetAlignment == Alignment.Enemy)
                    {
                        foreach (Component comp in other.GameObject.GetComponentList)
                        {
                            if (comp is Bullet)
                            {
                                otherIsBullet = true;
                                break;
                            }
                        }
                        if (AttackRadius.Contains(other.CollisionBox.Center) && otherIsBullet == false)
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
        /// handles what happens when an enemy enters
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionEnter(Collider other)
        {
            bool push = true;
            //push them a bit away
            float force = Constant.pushForce;
            if (other.GetAlignment != Alignment.Neutral)
            {
                if (!(other.GameObject.GetComponent("Plane") is Plane))
                {
                    foreach (Component go in other.GameObject.GetComponentList)
                    {
                        if (go is Bullet)
                        {
                            push = false;
                        }
                    }
                    if (push)
                    {
                        Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                        dir.Normalize();

                        other.GameObject.Transform.Translate(dir * force);
                    }
                }
            }
        }

        /// <summary>
        /// if something enters the middle of the
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionStay(Collider other)
        {
            bool push = true;
            //push them a bit away
            float force = Constant.pushForce * 2;
            if (other.GetAlignment != Alignment.Neutral)
            {
                if (!(other.GameObject.GetComponent("Plane") is Plane))
                {
                    foreach (Component go in other.GameObject.GetComponentList)
                    {
                        if (go is Bullet)
                        {
                            push = false;
                        }
                    }
                    if (push)
                    {
                        Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                        dir.Normalize();

                        other.GameObject.Transform.Translate(dir * force);
                    }
                }
            }
        }

        /// <summary>
        /// loads Tower Content
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            if (dieSoundEffect == null)
            {
                dieSoundEffect = content.Load<SoundEffect>("TowerDeath");
            }

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }

        /// <summary>
        /// creates the different animations for the tower
        /// </summary>
        protected virtual void CreateAnimation()
        {

        }

        /// <summary>
        /// handles what happens when a tower dies
        /// </summary>
        protected virtual void Die()
        {

            GameWorld.Instance.GameObjectsToRemove.Add(this.GameObject);
            GameWorld.Instance.TowerAmount--;
        }
        /// <summary>
        /// plays shoot sound effect
        /// </summary>
        protected virtual void PlayShootSoundEffect()
        {
            shootSound.Play(0.5f, 0, 0);

        }
    }
}
