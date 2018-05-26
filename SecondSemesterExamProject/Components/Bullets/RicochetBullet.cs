using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    class RicochetBullet : Bullet
    {

        protected int bounceCount;

        public int BounceCount
        {
            get { return bounceCount; }
            set { bounceCount = value; }
        }

        public RicochetBullet(GameObject gameObject, BulletType type, float dirRotation) : base(gameObject, type, dirRotation)
        {
            bounceCount = 0;
        }

        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 3, 22, 3, Vector2.Zero));
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
            Collider thisCollider = (Collider)GameObject.GetComponent("Collider");
            Console.WriteLine(dirRotation);

            if (thisCollider != null)
            {
                if (thisCollider.GetAlignment != other.GetAlignment)
                {
                    if (canRelease)
                    {
                        if (!(other.GameObject.GetComponent("HQ") is HQ) && thisCollider.GetAlignment == Alignment.Friendly || thisCollider.GetAlignment == Alignment.Enemy)
                        {

                            foreach (Component go in other.GameObject.GetComponentList)
                            {
                                if (go is Enemy && thisCollider.GetAlignment == Alignment.Friendly)
                                {
                                    (go as Enemy).Health -= bulletDmg;
                                }
                                if (go is Vehicle && thisCollider.GetAlignment == Alignment.Enemy)
                                {
                                    (go as Vehicle).Health -= bulletDmg;
                                }
                                if (go is Tower && thisCollider.GetAlignment == Alignment.Enemy)
                                {
                                    (go as Tower).Health -= bulletDmg;
                                }
                            }
                            if (BounceCount <= 4)
                            {
                                //TODO: Mirror angle properly
                                if (dirRotation <= 180)
                                {
                                    dirRotation = 180 - dirRotation;
                                }
                                else
                                {
                                    dirRotation= 360 - dirRotation;
                                }
                                
                                bounceCount++;
                            }
                            else
                            {
                                DestroyBullet();
                                bounceCount = 0;
                            }

                        }
                    }
                }
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}

