using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class Spitter : Ranged
    {
        public Spitter(GameObject gameObject, int health , float movementSpeed, float attackRate, float attackRange, EnemyType enemyType, BulletType bulletType, int spread, Alignment alignment)
            : base(gameObject, health , movementSpeed, attackRate,attackRange, enemyType, bulletType, spread , alignment)
        {
        }

        public override void AI()
        {
            base.AI();
        }

        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(3, 29, 0, 25, 29, 6, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(5, 58, 0, 25, 29, 46, Vector2.Zero));
            animator.CreateAnimation("Attack1", new Animation(5, 87, 0, 25, 29, ((10* 2) /Constant.spitterAttackRate), Vector2.Zero));
            animator.CreateAnimation("Attack2", new Animation(5, 116, 0, 25, 29, ((10 * 2) / Constant.spitterAttackRate), Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(5, 145, 0, 25, 29, 6, Vector2.Zero));
            base.CreateAnimation();
        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }
        
        protected override void Die()
        {
            base.Die();
        }

        protected override void Shoot()
        {

            base.Shoot();
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

        }

        /// <summary>
        /// Plays the death sound effect for this specefic enemy type
        /// </summary>
        protected override void PlayDeathSound()
        {
            deathSound.Play(0.6f, 0.5f, 0);
        }
    }
}
