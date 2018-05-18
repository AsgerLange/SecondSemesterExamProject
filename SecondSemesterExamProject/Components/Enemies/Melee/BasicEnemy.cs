﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class BasicEnemy : Melee
    {

        /// <summary>
        /// Basic Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="attackRate"></param>
        public BasicEnemy(GameObject gameObject, int health, float movementSpeed, float attackRate) : base(gameObject, health, movementSpeed, attackRate)
        {
            
        }


        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(4, 23, 0, 23, 23, 3, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 46, 0, 23, 25, 3, Vector2.Zero));
            animator.CreateAnimation("Attack", new Animation(4, 71, 0, 23, 29, 3, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(6, 100, 0, 23, 23, 8, Vector2.Zero));
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
            base.AI();
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
            animator.PlayAnimation("Death");
            base.Die();
        }
    }
}
