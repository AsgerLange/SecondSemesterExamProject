using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame
{
    enum Controls { WASD, UDLR }
    class Vehicle : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter
    {
        public Animator animator;
        protected int health;
        protected int money;
        protected Controls control;
        protected float movementSpeed;
        protected float fireRate;
        protected float rotation = 0;
        protected float rotateSpeed;
        protected SpriteRenderer spriteRenderer;
        protected float shotTimeStamp;

        /// <summary>
        /// creates a vehicle
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public Vehicle(GameObject gameObject, Controls control, int health, float movementSpeed, float fireRate, float rotateSpeed, int money) : base(gameObject)
        {
            this.control = control;
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.fireRate = fireRate;
            this.rotateSpeed = rotateSpeed;
            this.money = money;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        /// <summary>
        /// handles what happens when a vehicle dies
        /// </summary>
        protected virtual void Die()
        {
            Console.WriteLine(new NotImplementedException("die Vehicle"));
        }

        /// <summary>
        /// Handles the vehicles movement etc...
        /// </summary>
        public virtual void Update()
        {
            Movement();
            Shoot();
        }
        /// <summary>
        /// Handles Movement for Vehicles
        /// </summary>
        public void Movement()
        {
            Vector2 translation = Vector2.Zero;
            //is the player Rotating?
            Rotate(translation);
            //Is the player moving
            translation = Move(translation);
            //calculate direction of movement
            translation = RotateMove(translation);
            //move the vehicle
            TranslateMovement(translation);
            //rotate sprite
            spriteRenderer.Rotation = rotation;
        }

        public virtual void Shoot()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space) && (shotTimeStamp + fireRate) <= GameWorld.Instance.TotalGameTime)
            {
                BulletPool.CreateBullet(this.GameObject.Transform.Position, Alignment.Friendly,
                    BulletType.BaiscBullet, rotation);
                shotTimeStamp = (float)GameWorld.Instance.TotalGameTime;
            }

            if (keyState.IsKeyDown(Keys.F) && (shotTimeStamp + fireRate) <= GameWorld.Instance.TotalGameTime)
            {


                EnemyPool.CreateEnemy(new Vector2(GameObject.Transform.Position.X + 100,
                    GameObject.Transform.Position.Y + 100), EnemyType.BasicEnemy);

                shotTimeStamp = (float)GameWorld.Instance.TotalGameTime;
            }


        }

        /// <summary>
        /// moves the vehicle
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 Move(Vector2 translation)
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.W) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Up) && control == Controls.UDLR))
            {
                translation += new Vector2(0, -1);
                animator.PlayAnimation("MoveForward");
            }
            else if ((keyState.IsKeyDown(Keys.S) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Down) && control == Controls.UDLR))
            {
                translation += new Vector2(0, 1);
                animator.PlayAnimation("MoveBackward");
            }
            return translation;
        }

        /// <summary>
        /// Rotates the vehicle depending on the reotateSpeed
        /// </summary>
        /// <param name="translation"></param>
        public void Rotate(Vector2 translation)
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.D) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Right) && control == Controls.UDLR))
            {
                rotation += rotateSpeed;
            }
            if ((keyState.IsKeyDown(Keys.A) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Left) && control == Controls.UDLR))
            {
                rotation -= rotateSpeed;
            }
        }

        /// <summary>
        /// Returns a rotated version of the given translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 RotateMove(Vector2 translation)
        {
            return Vector2.Transform(translation, Matrix.CreateRotationZ(MathHelper.ToRadians(rotation)));
        }

        /// <summary>
        /// Makes the vehicle actually move
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="movementSpeed"></param>
        public void TranslateMovement(Vector2 translation)
        {
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);
        }

        /// <summary>
        /// handles animation for the vehicle
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            Console.WriteLine(new NotImplementedException("OnAnimationDone Vehicle"));

        }

        /// <summary>
        /// loads the vehicle sprite
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }

        public virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("Idle", new Animation(5, 40, 0, 28, 40, 2, Vector2.Zero));
            animator.CreateAnimation("MoveForward", new Animation(5, 80, 0, 28, 40, 5, Vector2.Zero));
            animator.CreateAnimation("MoveBackward", new Animation(5, 120, 0, 28, 40, 5, Vector2.Zero));
            animator.CreateAnimation("Shoot", new Animation(5, 160, 0, 28, 47, 5, Vector2.Zero));
        }

        /// <summary>
        /// what happens when something drives into the vehicle or the vehicle driwes into something?
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(Collider other)
        {
#if DEBUG
            foreach (Component com in other.GameObject.GetComponentList)
            {
                Console.WriteLine("Collided with an object with this Component: " + com.ToString());
            }
            Console.WriteLine("At these Coordinates: " + GameObject.Transform.Position);
#endif
        }
    }
}
