using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class BasicEnemy : Enemy
    {
        public BasicEnemy(GameObject gameObject, Alignment alignment, int health, float movementSpeed,float attackRate) : base(gameObject,alignment, health, movementSpeed, attackRate)
        {
            
        }


        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
        {
            base.CreateAnimation();
        }

        /// <summary>
        /// loads the tank
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
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
        /// handles what the tank does
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// handles what happens when the tank dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }
    }
}
