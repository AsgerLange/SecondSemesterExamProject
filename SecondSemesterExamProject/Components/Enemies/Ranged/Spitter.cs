using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Spitter : Ranged
    {
        public Spitter(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate, EnemyType enemyType, BulletType bulletType, float attackRange, int spread) : base(gameObject, health, damage, movementSpeed, attackRate, enemyType, bulletType, attackRange, spread)
        {
        }

        public override void AI()
        {
            base.AI();
        }

        public override void CreateAnimation()
        {
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
