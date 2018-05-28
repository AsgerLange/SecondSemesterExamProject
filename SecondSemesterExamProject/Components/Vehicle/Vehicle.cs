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
    enum VehicleType { Tank, Bike, Plane}
    class Vehicle : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter, IDrawable
    {
        private Random rnd = new Random();
        private SpriteFont font;
        public Animator animator;

        protected Weapon weapon;
        protected TowerPlacer towerPlacer;
        protected int health;
        protected int money;
        protected Controls control;
        protected VehicleType vehicleType;

        protected float movementSpeed;
        protected float fireRate;
        protected float rotation = 0;
        protected float rotateSpeed;
        protected SpriteRenderer spriteRenderer;
        protected float shotTimeStamp;

        protected bool isAlive;

        protected bool isPlayingAnimation = false;

        public Weapon Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }
        public TowerPlacer TowerPlacer
        {
            get { return towerPlacer; }
            set { towerPlacer = value; }
        }
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

        private float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                if (rotation >= 360)
                {
                    rotation = 0;
                }
                if (rotation < 0)
                {
                    rotation = 360;
                }
            }
        }

        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                if (money < 0)
                {
                    money = 0;
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
        public Vehicle(GameObject gameObject, Weapon weapon, Controls control, int health, float movementSpeed, float rotateSpeed, int money,
            TowerType towerType) : base(gameObject)
        {
            this.control = control;
            this.health = health;
            this.movementSpeed = movementSpeed;             
            this.rotateSpeed = rotateSpeed;
            this.money = money;

            this.towerPlacer = new TowerPlacer(this, towerType, 1);
            this.weapon = weapon;
            isAlive = true;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;

        }

        /// <summary>
        /// handles what happens when a vehicle dies
        /// </summary>
        protected virtual void Die()
        {
            animator.PlayAnimation("Death");
            isAlive = false;
            isPlayingAnimation = true;

        }

        /// <summary>
        /// Handles the vehicles movement etc...
        /// </summary>
        public virtual void Update()
        {
            if (isAlive)
            {
                Movement(); //Checks if vehicle is moving, and moves if so

                Shoot(); //same for shooting

                BuildTower(); //and building tower
            }
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
            spriteRenderer.Rotation = Rotation;
        }

        /// <summary>
        /// handles the shooting
        /// </summary>
        protected virtual void Shoot()
        {
            KeyboardState keyState = Keyboard.GetState();

            //if the player is pressing the "Shoot" button
            if ((keyState.IsKeyDown(Keys.F) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Enter) && control == Controls.UDLR))
            {

                //if enough time has passed since last shot
                if ((shotTimeStamp + weapon.FireRate) <= GameWorld.Instance.TotalGameTime)
                {

                    weapon.Shoot(GameObject.Transform.Position, Alignment.Friendly, Rotation); //Fires the weapon

                    animator.PlayAnimation("Shoot"); //play shooting animation

                    isPlayingAnimation = true; //allows the animation to not be overwritten by movement animations

                    spriteRenderer.Offset = RotateVector(spriteRenderer.Offset);//Changes offset to fit with animation

                    shotTimeStamp = (float)GameWorld.Instance.TotalGameTime; //Timestamp for when shot is fired (used to determine when the next shot can be fired)
                }
            }
        }

        /// <summary>
        /// Spawns a tower on the vehicle's postition, if the spawn button is pressed and the vehicle has sufficient amount of money.
        /// </summary>
        private void BuildTower()
        {
            KeyboardState keyState = Keyboard.GetState();


            if ((keyState.IsKeyDown(Keys.G) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Back) && control == Controls.UDLR))
            {
                TowerPlacer.PlaceTower();

            }

        }



        /// <summary>
        /// moves the vehicle
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        protected virtual Vector2 Move(Vector2 translation)
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
                Rotation += rotateSpeed;
            }
            if ((keyState.IsKeyDown(Keys.A) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Left) && control == Controls.UDLR))
            {
                Rotation -= rotateSpeed;
            }
        }

        /// <summary>
        /// Returns a rotated version of the given translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        protected Vector2 RotateVector(Vector2 translation)
        {
            return Vector2.Transform(translation, Matrix.CreateRotationZ(MathHelper.ToRadians(Rotation)));
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
                spriteRenderer.Offset = Vector2.Zero;
            }
            if (animationName == "Death")
            {
                isPlayingAnimation = false;
                GameWorld.Instance.GameObjectsToRemove.Add(this.GameObject);
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
            font = content.Load<SpriteFont>("Stat");

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }

        /// <summary>
        /// creates the animations
        /// </summary>
        public virtual void CreateAnimation()
        {
            //EKSEMPEL

        }

        /// <summary>
        /// what happens when something drives into the vehicle or the vehicle driwes into something?
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(Collider other)
        {

        }

        /// <summary>
        /// Draws the vehicles stats
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawInfo(spriteBatch);
        }

        /// <summary>
        /// Draws the vehicle info out to screen depending on the controls used
        /// </summary>
        /// <param name="spriteBatch"></param>
        protected void DrawInfo(SpriteBatch spriteBatch)
        {
            if (control == Controls.WASD)
            {
                spriteBatch.DrawString(font, money + " $", new Vector2(2, 2), Color.CornflowerBlue);
                spriteBatch.DrawString(font, TowerPlacer.ToString(), new Vector2(2, Constant.higth - 20), Color.CornflowerBlue);
                spriteBatch.DrawString(font, weapon.ToString(), new Vector2(2, Constant.higth - 40), Color.CornflowerBlue);
                spriteBatch.DrawString(font, "HP: " + Health.ToString(), new Vector2(2, Constant.higth - 60), Color.CornflowerBlue);
                spriteBatch.DrawString(font, this.ToString(), new Vector2(2, Constant.higth - 80), Color.CornflowerBlue);
            }
            else if (control == Controls.UDLR)
            {
                spriteBatch.DrawString(font, money + " $", new Vector2(Constant.width - font.MeasureString(money + " $").X - 2, 2), Color.YellowGreen);
                spriteBatch.DrawString(font, TowerPlacer.ToString(), new Vector2(Constant.width - font.MeasureString(TowerPlacer.ToString()).X - 2, Constant.higth - 20), Color.YellowGreen);
                spriteBatch.DrawString(font, weapon.ToString(), new Vector2(Constant.width - font.MeasureString(weapon.ToString()).X - 2, Constant.higth - 40), Color.YellowGreen);
                spriteBatch.DrawString(font, "HP: " + Health.ToString(), new Vector2(Constant.width - font.MeasureString("HP: " + Health.ToString()).X - 2, Constant.higth - 60), Color.YellowGreen);
                spriteBatch.DrawString(font, this.ToString(), new Vector2(Constant.width - 200, Constant.higth - 80), Color.YellowGreen);
            }
        }
        public override string ToString()
        {
            return vehicleType.ToString();
        }
    }
}
