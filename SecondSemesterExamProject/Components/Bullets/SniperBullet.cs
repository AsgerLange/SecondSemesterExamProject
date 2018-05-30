using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    /// <summary>
    /// A high damaging, fast moving, long range, piercing round.
    /// </summary>
    class SniperBullet : Bullet
    {
        protected int enemiesPierced;

        public int EnemiesPierced
        {
            get { return enemiesPierced; }
            set { enemiesPierced = value; }
        }

        public SniperBullet(GameObject gameObject, BulletType type, float dirRotation) : base(gameObject, type, dirRotation)
        {
            enemiesPierced = 0;

        }
        /// <summary>
        /// Allows the sniper bullet to pierce multiple enemies, but lowers for each piercing 
        /// </summary>
        protected override void BulletSpecialEffect(Collider other)
        {
            enemiesPierced++;

            this.bulletDmg = bulletDmg / 2;

            if (enemiesPierced > 5)
            {
                base.BulletSpecialEffect(other);
            }
            if ((other.GameObject.GetComponent("Terrain") is Terrain))
            {
                base.BulletSpecialEffect(other);
            }
        }

        /// <summary>
        /// sniperbullet animation
        /// </summary>
        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 4, 29, 3, Vector2.Zero));
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
        /// <summary>
        /// Overwrites base collision to allow sniper bullet to penetrate enemies but not terrain
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollisionEnter(Collider other)
        {
            base.OnCollisionEnter(other);
        }

        /// <summary>
        /// Destroys sniper bullet and resets the counter for enemies pierced.
        /// </summary>
        public override void DestroyBullet()
        {
            base.DestroyBullet();
            enemiesPierced = 0;

        }
        public override void Update()
        {
            base.Update();
        }
    }
}

