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
        /// Creates the bike
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public Bike(GameObject gameObject, Controls control , int health, float movementSpeed  , float rotateSpeed, int money,
             TowerType tower, int playerNumber) : base(gameObject , control, health, movementSpeed, rotateSpeed, money, tower, playerNumber)
        {
            this.vehicleType = VehicleType.Bike;
            this.weapon = new Shotgun(this.GameObject);

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
            animator.CreateAnimation("Idle", new Animation(5, 34, 0, 21, 34, 6, Vector2.Zero));
            animator.CreateAnimation("MoveForward", new Animation(5, 68, 0, 21, 34, 8, Vector2.Zero));
            animator.CreateAnimation("MoveBackward", new Animation(5, 102, 0, 21, 34, 8, Vector2.Zero));
            animator.CreateAnimation("Shoot", new Animation(5, 136, 0, 21, 36, 14 / weapon.FireRate, new Vector2(-1, -1)));
            //animator.CreateAnimation("MoveShootForward", new Animation(5, 170, 0, 21, 49, 8, Vector2.Zero));
            //animator.CreateAnimation("MoveShootBackward", new Animation(5, 256, 0, 21, 49, 8, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(5, 170, 0, 21, 34, 6, Vector2.Zero));
        }

        /// <summary>
        /// loads the bike
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            this.weapon = new Shotgun(this.GameObject);
            base.LoadContent(content);
        }

        /// <summary>
        /// handles which animation should the bike be running
        /// </summary>
        /// <param name="animationName"></param>
        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        /// <summary>
        /// handles what the bike does
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// handles what happens when the bike dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }
        
    }
}