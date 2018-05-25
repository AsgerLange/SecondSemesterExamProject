using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Plane : Vehicle
    {
        /// <summary>
        /// Creates the Pláne
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public Plane(GameObject gameObject, Controls control, int health, float movementSpeed, float fireRate, float rotateSpeed, int money,
            BulletType cannonAmmo, TowerType tower) : base(gameObject, control, health, movementSpeed, fireRate, rotateSpeed, money, cannonAmmo, tower)
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

        protected override void Shoot()
        {
            KeyboardState keyState = Keyboard.GetState();

            if ((shotTimeStamp + fireRate) <= GameWorld.Instance.TotalGameTime)
            {
                if ((keyState.IsKeyDown(Keys.F) && control == Controls.WASD)
                    || (keyState.IsKeyDown(Keys.Enter) && control == Controls.UDLR))
                {

                    BulletPool.CreateBullet(GameObject.Transform.Position, Alignment.Friendly,
                        cannonAmmo, rotation + (GameWorld.Instance.Rnd.Next(-5, 5)));
                    animator.PlayAnimation("Shoot");
                    spriteRenderer.Offset = RotateVector(spriteRenderer.Offset);
                    isPlayingAnimation = true;
                    shotTimeStamp = (float)GameWorld.Instance.TotalGameTime;
                }
            }
        }
    }
}
