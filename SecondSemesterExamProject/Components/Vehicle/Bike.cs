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
    class Bike : Vehicle
    {
        /// <summary>
        /// Creates the tank
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public Bike(GameObject gameObject, Controls control, Weapon weapon, int health, float movementSpeed, float fireRate, float rotateSpeed, int money,
             TowerType tower) : base(gameObject, weapon, control, health, movementSpeed, fireRate, rotateSpeed, money, tower)
        {

        }

        /// <summary>
        /// moves the bike
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        protected override Vector2 Move(Vector2 translation)
        {

            KeyboardState keyState = Keyboard.GetState();

            if ((keyState.IsKeyDown(Keys.W) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Up) && control == Controls.UDLR))
            {
                translation += new Vector2(0, -1);
                if (isPlayingAnimation == false)
                {
                    animator.PlayAnimation("MoveForward");
                }
            }
            else if ((keyState.IsKeyDown(Keys.S) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Down) && control == Controls.UDLR))
            {
                translation += new Vector2(0, 0.2f);
                if (isPlayingAnimation == false)
                {
                    animator.PlayAnimation("MoveBackward");
                }
            }
            return translation;
        }
        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(5, 40, 0, 28, 40, 2, Vector2.Zero));
            animator.CreateAnimation("MoveForward", new Animation(5, 80, 0, 28, 40, 5, Vector2.Zero));
            animator.CreateAnimation("MoveBackward", new Animation(5, 120, 0, 28, 40, 5, Vector2.Zero));
            animator.CreateAnimation("Shoot", new Animation(5, 160, 0, 28, 47, 10 / weapon.FireRate, new Vector2(0, -3)));
            animator.CreateAnimation("MoveShootForward", new Animation(5, 207, 0, 28, 49, 5, Vector2.Zero));
            animator.CreateAnimation("MoveShootBackward", new Animation(5, 256, 0, 28, 49, 5, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(7, 305, 0, 28, 40, 5, Vector2.Zero));
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