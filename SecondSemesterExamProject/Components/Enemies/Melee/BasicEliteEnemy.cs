using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class BasicEliteEnemy : Melee
    {

        /// <summary>
        /// Basic Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="attackRate"></param>
        public BasicEliteEnemy(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate,float attackRange, EnemyType enemyType, Alignment alignment) 
            : base(gameObject, health, damage, movementSpeed, attackRate,attackRange, enemyType, alignment)
        {

        }


        /// <summary>
        /// Creates the animations
        /// </summary>
        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(5, 23, 0, 23, 23, 4, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 46, 0, 23, 25, 8, Vector2.Zero));
            animator.CreateAnimation("Attack1", new Animation(5, 71, 0, 23, 29, 10, Vector2.Zero));
            animator.CreateAnimation("Attack2", new Animation(5, 100, 0, 23, 29, 10, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(6, 129, 0, 23, 23, 8, Vector2.Zero));
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
                movementSpeed = Constant.basicEliteEnemyMovementSpeed;
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
            this.movementSpeed = -10; //Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackVehicle(vehicle);
        }
        protected override int EnemyGold()
        {
            return Constant.basicEliteEnemyGold;
        }
        /// <summary>
        /// Basic Enemy's custon attack method for attacking vehicles
        /// </summary>
        /// <param name="tower"></param>
        protected override void AttackTower(Tower tower)
        {
            this.movementSpeed = -10; //Slows enemy down when attacking ( Resets after attackanimation is done)
            base.AttackTower(tower);
        }
        /// <summary>
        /// Plays the death sound effect for this specefic enemy type
        /// </summary>
        protected override void PlayDeathSound()
        {
            deathSound.Play(1f, -0.5f, 0);
        }
    }
}
