using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class BasicTower : Tower
    {

        public BasicTower(GameObject gameObject, float attackRate, int health, float attackRange, BulletType bulletType)
            : base(gameObject, attackRate,health,attackRange, bulletType)
        {

        }
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        public override void OnCollisionEnter(Collider other)
        {
            base.OnCollisionEnter(other);
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(5, 32, 0, 32, 32, 4, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(6, 64, 0, 32, 32, 6, Vector2.Zero));


            base.CreateAnimation();

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
