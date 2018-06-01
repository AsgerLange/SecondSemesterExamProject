using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{ 
    /// <summary>
    /// A short range, low damage round.
    /// </summary>
    class ShotgunPellet : Bullet
    {
        public ShotgunPellet(GameObject gameObject, BulletType type, float dirRotation) : base(gameObject, type, dirRotation)
        {
        }

        public override void CreateAnimation()
        {
            
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 3, 12, 3, Vector2.Zero));

            base.CreateAnimation();
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
    }
}

 