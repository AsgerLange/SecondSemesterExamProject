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
        protected float builtTimeStamp;

        protected bool isPlayingAnimation = false;

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health <= 0)
                {
                    health = 0;
                    Die();
                }
            }
        }

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
            GameWorld.Instance.GameObjectsToRemove.Add(this.GameObject);
            Console.WriteLine(new NotImplementedException("die Vehicle"));
        }

        /// <summary>
        /// Handles the vehicles movement etc...
        /// </summary>
        public virtual void Update()
        {
            Movement(); //Checks if vehicle is moving, and moves if so

            Shoot(); //same for shooting

            BuildTower(); //and building tower

            // spriteRenderer.Offset = RotateVector(spriteRenderer.Offset);
        }

        /// <summary>
        /// Handles Movement for Vehicles
        /// </summary>
        protected void Movement()
        {
            Vector2 translation = Vector2.Zero;
            //is the player Rotating?
            Rotate(translation);
            //Is the player moving
            translation = Move(translation);
            //calculate direction of movement
            translation = RotateVector(translation);
            //move the vehicle
            TranslateMovement(translation);
            //rotate sprite
            spriteRenderer.Rotation = rotation;
        }

        /// <summary>
        /// handles the shooting
        /// </summary>
        protected virtual void Shoot()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space) && (shotTimeStamp + fireRate) <= GameWorld.Instance.TotalGameTime)
            {
                BulletPool.CreateBullet(GameObject.Transform.Position, Alignment.Friendly,
                    BulletType.BasicBullet, rotation);
                animator.PlayAnimation("Shoot");
                isPlayingAnimation = true;
                shotTimeStamp = (float)GameWorld.Instance.TotalGameTime;
            }

        }

        /// <summary>
        /// Spawns a tower on the vehicle's postition, if the spawn button is pressed and the vehicle has sufficient amount of money.
        /// </summary>
        private void BuildTower()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.G) && (builtTimeStamp + Constant.buildTowerCoolDown) <= GameWorld.Instance.TotalGameTime)
            {

                GameObject tower;

                //Gameobjectdirector builds a new tower
                tower = GameObjectDirector.Instance.Construct(new Vector2(GameObject.Transform.Position.X + 1,
                    GameObject.Transform.Position.Y + 1), TowerType.BasicTower);

                //its content is loaded
                tower.LoadContent(GameWorld.Instance.Content);

                //it's added to gameworld next update cycle
                GameWorld.Instance.GameObjectsToAdd.Add(tower);

                //time stamps for when the tower is build (used for cooldown)
                builtTimeStamp = (float)GameWorld.Instance.TotalGameTime;
            }
        }

        /// <summary>
        /// moves the vehicle
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        protected Vector2 Move(Vector2 translation)
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
                translation += new Vector2(0, 1);
                if (isPlayingAnimation == false)
                {
                    animator.PlayAnimation("MoveBackward");

                }
            }
            return translation;
        }

        /// <summary>
        /// Rotates the vehicle depending on the reotateSpeed
        /// </summary>
        /// <param name="translation"></param>
        protected void Rotate(Vector2 translation)
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
        protected Vector2 RotateVector(Vector2 translation)
        {
            return Vector2.Transform(translation, Matrix.CreateRotationZ(MathHelper.ToRadians(rotation)));
        }

        /// <summary>
        /// Makes the vehicle actually move
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="movementSpeed"></param>
        protected void TranslateMovement(Vector2 translation)
        {
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);
        }

        /// <summary>
        /// handles animation for the vehicle
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            if (animationName == "Shoot")
            {
                isPlayingAnimation = false;
            }
            if (isPlayingAnimation == false)
            {
                animator.PlayAnimation("Idle");

            }

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

        /// <summary>
        /// creates the animations
        /// </summary>
        public virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("Idle", new Animation(5, 40, 0, 28, 40, 2, Vector2.Zero));
            animator.CreateAnimation("MoveForward", new Animation(5, 80, 0, 28, 40, 5, Vector2.Zero));
            animator.CreateAnimation("MoveBackward", new Animation(5, 120, 0, 28, 40, 5, Vector2.Zero));
            animator.CreateAnimation("Shoot", new Animation(5, 160, 0, 28, 47, 10 / Constant.tankFireRate, new Vector2(0, -4)));
            animator.CreateAnimation("MoveShootForward", new Animation(5, 207, 0, 28, 49, 5, Vector2.Zero));
            animator.CreateAnimation("MoveShootBackward", new Animation(5, 256, 0, 28, 49, 5, Vector2.Zero));
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
