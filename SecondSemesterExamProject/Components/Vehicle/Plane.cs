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
        public Plane(GameObject gameObject, Controls control, Weapon weapon, int health, float movementSpeed, float fireRate, float rotateSpeed, int money,
              TowerType tower) : base(gameObject, weapon, control, health, movementSpeed, fireRate, rotateSpeed, money, tower)
        {

        }

        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(5, 48, 0, 32, 48, 6, Vector2.Zero));
            animator.CreateAnimation("MoveForward", new Animation(5, 96, 0, 32, 48, 8, Vector2.Zero));
            animator.CreateAnimation("MoveBackward", new Animation(5, 144, 0, 32, 48, 4, Vector2.Zero));
            animator.CreateAnimation("Shoot", new Animation(5, 192, 0, 32, 54, 10 / weapon.FireRate, new Vector2(0,-2)));
            //animator.CreateAnimation("MoveShootForward", new Animation(5, 207, 0, 28, 49, 5, Vector2.Zero));
            //animator.CreateAnimation("MoveShootBackward", new Animation(5, 256, 0, 28, 49, 5, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(3, 246, 0, 32, 48, 5, Vector2.Zero));
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
        /// handles which animation should the plane be running
        /// </summary>
        /// <param name="animationName"></param>
        public override void OnAnimationDone(string animationName)
        {
            isPlayingAnimation = false;
            base.OnAnimationDone(animationName);
        }

        /// <summary>
        /// handles what the plane does
        /// </summary>
        public override void Update()
        {
            base.Update();
        }


        /// <summary>
        /// handles what happens when the plane dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }

        protected override Vector2 Move(Vector2 translation)
        {
            KeyboardState keyState = Keyboard.GetState();

            translation += new Vector2(0, -1);

            if (isPlayingAnimation == false)
            {
                animator.PlayAnimation("Idle");
            }

            if ((keyState.IsKeyDown(Keys.S) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Down) && control == Controls.UDLR))
            {
                if (isPlayingAnimation == false)
                {
                    isPlayingAnimation = true;
                    animator.PlayAnimation("MoveBackward");
                }
                translation += new Vector2(0, 0.4f);

            }
            else if ((keyState.IsKeyDown(Keys.W) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Up) && control == Controls.UDLR))
            {
                if (isPlayingAnimation == false)
                {
                    isPlayingAnimation = true;
                    animator.PlayAnimation("MoveForward");
                }
                translation += new Vector2(0, -0.6f);

            }
            return translation;
        }

        protected override void Shoot()
        {
            base.Shoot();
        }
    }
}
