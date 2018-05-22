using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class HQ : Tower
    {
        public HQ(GameObject gameObject, float attackRate, int health, float attackRange) : base(gameObject, attackRate, health, attackRange)
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
            //HQ Animation
            animator.CreateAnimation("Idle", new Animation(6, 96, 0, 96, 96, 8, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(7, 192, 0, 96, 96, 6, Vector2.Zero));
            base.CreateAnimation();
        }

        /// <summary>
        /// handles what happens when a HQ dies
        /// </summary>
        protected override void Die()
        {

            animator.PlayAnimation("Death");
        }
    }
}
