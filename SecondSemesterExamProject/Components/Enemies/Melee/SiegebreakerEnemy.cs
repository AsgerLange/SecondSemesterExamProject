﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class SiegebreakerEnemy : Melee
    {

        /// <summary>
        /// Basic Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="attackRate"></param>
        public SiegebreakerEnemy(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate,float attackRange, EnemyType enemyType) 
            : base(gameObject, health, damage, movementSpeed, attackRate,attackRange, enemyType)
        {

        }


        /// <summary>
        /// Creates the animations
        /// </summary>
        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(4, 51, 0, 31, 51, 6, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 102, 0, 31, 51, 6, Vector2.Zero));
            animator.CreateAnimation("Attack1", new Animation(5, 153, 0, 31, 51, 12, Vector2.Zero));
            animator.CreateAnimation("Attack2", new Animation(5, 153, 0, 31, 51, 12, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(7, 204, 0, 31, 51, 6, Vector2.Zero));
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
            if (animationName.Contains("Attack"))
            {
                movementSpeed = Constant.siegeBreakerEnemyMovementSpeed;
            }
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

        protected override void CheckIfCanAttack(Collider other)
        {

            base.CheckIfCanAttack(other);


        }

        /// <summary>
        /// Basic Enemy's custom Attack method for attacking vehicles
        /// </summary>
        /// <param name="vehicle"></param>
        protected override void AttackVehicle(Vehicle vehicle)
        {
            this.movementSpeed = -50; //Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackVehicle(vehicle);
        }
        protected override int EnemyGold()
        {
            return Constant.siegeBreakerEnemyGold;
        }
        /// <summary>
        /// Basic Enemy's custon attack method for attacking vehicles
        /// </summary>
        /// <param name="tower"></param>
        protected override void AttackTower(Tower tower)
        {
            this.movementSpeed = -20; //Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackTower(tower);
        }
        /// <summary>
        /// Plays the death sound effect for this specefic enemy type
        /// </summary>
        protected override void PlayDeathSound()
        {
            deathSound.Play(1f, -1f, 0);
        }
    }
}
