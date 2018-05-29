using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Spitter : Ranged
    {
        public Spitter(GameObject gameObject, int health , float movementSpeed, float attackRate, float attackRange, EnemyType enemyType, BulletType bulletType, int spread)
            : base(gameObject, health , movementSpeed, attackRate,attackRange, enemyType, bulletType, spread)
        {
        }

        public override void AI()
        {
            base.AI();
        }

        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(3, 29, 0, 21, 29, 4, Vector2.Zero));
            animator.CreateAnimation("Walk", new Animation(4, 58, 0, 21, 29, 6, Vector2.Zero));
            animator.CreateAnimation("Attack1", new Animation(4, 87, 0, 21, 29, (6* 2 /Constant.spitterAttackRate), Vector2.Zero));
            animator.CreateAnimation("Attack2", new Animation(4, 116, 0, 21, 29, (6 * 2 / Constant.spitterAttackRate), Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(5, 145, 0, 21, 29, 6, Vector2.Zero));
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

        
    }
}
