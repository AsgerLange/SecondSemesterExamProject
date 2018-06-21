using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MonsterBullet : Bullet
    {
        private bool colorChanged = false;
        public bool hasHit = false;

        public MonsterBullet(GameObject gameObject, BulletType type, float dirRotation) : base(gameObject, type, dirRotation)
        {

        }

        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 31, 10,1, Vector2.Zero));

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
            ChangeColor();
            base.Update();
        }
        public override void DestroyBullet()
        {
            hasHit = false;
            colorChanged = false;
            base.DestroyBullet();
        }
        protected override void BulletSpecialEffect(Collider other)
        {
            if ((other.GameObject.GetComponent("Terrain") is Terrain))
            {
                base.BulletSpecialEffect(other);
            }
            else if (hasHit == false)
            {
                hasHit = false;
            }
        }
        public void ChangeColor()
        {

            if (colorChanged == false)
            {
                if (shooter.Control == Controls.WASD)
                {
                    spriteRenderer.color = Color.Cyan;

                }
                else
                {
                    spriteRenderer.color = Color.Lime;

                }
                colorChanged = true;
            }
        }

    }
    
}