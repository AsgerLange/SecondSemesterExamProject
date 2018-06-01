﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class Crate : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter, ICollisionStay
    {
        private float spawnTimeStamp;

        protected Animator animator;

        protected SpriteRenderer spriteRenderer;


        /// <summary>
        /// Constructor for LootCrate
        /// </summary>
        public Crate(GameObject gameObject) : base(gameObject)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");

            this.spawnTimeStamp = GameWorld.Instance.TotalGameTime;

            spriteRenderer.UseRect = true;

        }
        /// <summary>
        /// loads the content of the lootcrate
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Spawn");
        }
        /// <summary>
        /// creates animations for the loot crate
        /// </summary>
        protected virtual void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(4, 25, 0, 25, 25, 8, Vector2.Zero));
            animator.CreateAnimation("Spawn", new Animation(4, 25, 0, 25, 25, 8, Vector2.Zero));
            animator.CreateAnimation("PickUp", new Animation(4, 25, 0, 25, 25, 8, Vector2.Zero));
        }

        /// <summary>
        /// What to do when an animation stops playing
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            if (animationName == "SPawn")
            {
                animator.PlayAnimation("Idle");
            }
            else
            {
                animator.PlayAnimation("Idle");
            }
        }

        /// <summary>
        /// updates the lootcrate
        /// </summary>
        public void Update()
        {
            if (spawnTimeStamp + Constant.crateLifeSpan <= GameWorld.Instance.TotalGameTime)
            {
                Die();
            }
        }
        /// <summary>
        /// removes the crate
        /// </summary>
        protected virtual void Die()
        {
            GameWorld.Instance.GameObjectsToRemove.Add(this.GameObject);
        }

        /// <summary>
        /// handles what happens when other ojects collide with the crate
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(Collider other)
        {
            bool isBullet = false;
            //push them a bit away

            float force = Constant.pushForce * 2;
            if (other.GetAlignment != Alignment.Neutral)
            {

                foreach (Component go in other.GameObject.GetComponentList)
                {
                    if (go is Bullet)
                    {
                        isBullet = true;
                    }
                }
                if (other.GetAlignment == Alignment.Friendly && isBullet == false)
                {
                    foreach (Component comp in other.GameObject.GetComponentList)
                    {
                        if (comp is Vehicle)
                        {
                            GiveLoot(comp as Vehicle);

                            (comp as Vehicle).LootTimeStamp = GameWorld.Instance.TotalGameTime;


                            Die();
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gives the player a reward (Overwrited by children)
        /// </summary>
        /// <param name="vehicle"></param>
        protected virtual void GiveLoot(Vehicle vehicle)
        {

        }

        /// <summary>
        /// crates pushes itself to the side
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionStay(Collider other)
        {
            if (other.GetAlignment != Alignment.Enemy)
            {
                float force = Constant.pushForce;
                Vector2 dir = GameObject.Transform.Position - other.GameObject.Transform.Position;
                dir.Normalize();

                GameObject.Transform.Translate(dir * force);
            }
        }
    }
}
