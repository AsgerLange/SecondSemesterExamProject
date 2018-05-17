using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            base.CreateAnimation();
        }
    }
}
