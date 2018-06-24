using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class SwarmerEnemy : Melee
    {

        public MonsterVehicle vehicleWhoSpawnedIt;
        /// <summary>
        /// Basic Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="attackRate"></param>
        public SwarmerEnemy(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate, float attackRange, EnemyType enemyType) 
            : base(gameObject, health, damage, movementSpeed, attackRate,attackRange, enemyType)
        {

        }


        /// <summary>
        /// Creates the animations
        /// </summary>
        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(4, 19, 0, 17, 19, 10, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 38, 0, 17, 19, 16, Vector2.Zero));
            animator.CreateAnimation("Attack1", new Animation(4, 57, 0, 17, 19, 8, Vector2.Zero));
            animator.CreateAnimation("Attack2", new Animation(4, 76, 0, 17, 19, 8, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(4, 95, 0, 17, 19, 6, Vector2.Zero));
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
                movementSpeed = Constant.swarmerEnemyMovementSpeed;
            }
        }

        /// <summary>
        /// handles what the enemy does
        /// </summary>
        public override void Update()
        {
            if (playerSpawned)
            {
                if ((targetGameObject == GameWorld.Instance.GameObjects[0] || targetGameObject == null))
                {

                targetGameObject = vehicleWhoSpawnedIt.GameObject;
                }
            }

            base.Update();
        }



        /// <summary>
        /// handles what happens when the basicEnemy dies
        /// </summary>
        protected override void Die()
        {
            if (playerSpawned)
            {
            vehicleWhoSpawnedIt.swarmerCount--;
            }

            base.Die();
        }

        protected override void CheckIfCanAttack(Collider other)
        {

            base.CheckIfCanAttack(other);


        }
        protected override int EnemyGold()
        {
            return Constant.swarmerEnemyGold;
        }

        /// <summary>
        /// Basic Enemy's custom Attack method for attacking vehicles
        /// </summary>
        /// <param name="vehicle"></param>
        protected override void AttackVehicle(Vehicle vehicle)
        {
            this.movementSpeed = -60; //Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackVehicle(vehicle);
        }

        /// <summary>
        /// Basic Enemy's custon attack method for attacking vehicles
        /// </summary>
        /// <param name="tower"></param>
        protected override void AttackTower(Tower tower)
        {
            this.movementSpeed = -40;//Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackTower(tower);
        }

        protected override void AttackEnemy(Enemy enemy)
        {
            this.movementSpeed = -40;//Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackEnemy(enemy);
        }
       
        /// <summary>
        /// Plays the death sound effect for this specefic enemy type
        /// </summary>
        protected override void PlayDeathSound()
        {
            deathSound.Play(0.5f, 1f, 0);
        }
    }
}
