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
    class Vehicle : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter, IDrawable
    {
        private Random rnd = new Random();
        private SpriteFont font;
        public Animator animator;
        private Stats stats;

        protected Weapon weapon;
        protected TowerPlacer towerPlacer;
        protected int health;
        protected int maxHealth;

        protected int money;
        protected Controls control;
        protected VehicleType vehicleType;

        protected float movementSpeed;

        protected float rotation = 0;
        protected float rotateSpeed;
        protected SpriteRenderer spriteRenderer;

        protected float shotTimeStamp; //when a vehicle last fired its weapon
        protected float builtTimeStamp; //when a vehicle last built a tower

        protected float lootTimeStamp; // when a vehicle received loot
        private Crate latestLootCrate; //For displaying reward
        private float deathTimeStamp;
        public int PlayerNumber { get; set; }
        public bool IsAlive { get; set; }

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
        public Controls Control
        {
            get { return control; }

        }
        public float DeathTimeStamp
        {
            get { return deathTimeStamp; }
            set { deathTimeStamp = value; }
        }
        public Stats Stats
        {
            get { return stats; }
            set { stats = value; }
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
                    animator.PlayAnimation("Death");
                    isPlayingAnimation = true;
                    IsAlive = false;
                }
                else if (health > maxHealth)
                {
                    health = maxHealth;
                }
            }
        }

        public float LootTimeStamp
        {
            get { return LootTimeStamp; }
            set { lootTimeStamp = value; }
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
        public float BuiltTimeStamp
        {
            get { return builtTimeStamp; }
            set { builtTimeStamp = value; }
        }

        public Crate LatestLootCrate
        {
            get { return latestLootCrate; }
            set { latestLootCrate = value; }
        }
        /// <summary>
        /// creates a vehicle
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public Vehicle(GameObject gameObject, Controls control, int health, float movementSpeed, float rotateSpeed, int money,
            TowerType towerType, int playerNumber) : base(gameObject)
        {
            this.control = control;
            this.health = health;
            this.maxHealth = health;
            this.movementSpeed = movementSpeed;
            this.rotateSpeed = rotateSpeed;
            this.money = money;
            this.stats = new Stats(this);
            this.PlayerNumber = playerNumber;
            this.towerPlacer = new TowerPlacer(this, towerType, 1);
            this.weapon = new BasicWeapon(this.GameObject);
            IsAlive = true;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;

        }

        /// <summary>
        /// handles what happens when a vehicle dies
        /// </summary>
        protected virtual void Die()
        {
            deathTimeStamp = GameWorld.Instance.TotalGameTime;
            GameWorld.Instance.VehiclesToRemove.Add(this.GameObject);
            GameWorld.Instance.UpdatePlayerAmount();
            this.stats.TotalAmountOfPlayerDeaths++;
        }

        /// <summary>
        /// Handles the vehicles movement etc...
        /// </summary>
        public virtual void Update()
        {
            if (IsAlive)
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
                || (keyState.IsKeyDown(Keys.OemComma) && control == Controls.UDLR))
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
                || (keyState.IsKeyDown(Keys.OemPeriod) && control == Controls.UDLR))
            {
                if (builtTimeStamp + Constant.buildTowerCoolDown <= GameWorld.Instance.TotalGameTime)
                {
                    TowerPlacer.PlaceTower();

                    builtTimeStamp = GameWorld.Instance.TotalGameTime;
                }
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
                Die();
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
            if (GameWorld.Instance.GetGameState == GameState.Game)
            {
                if (control == Controls.WASD)
                {
                    spriteBatch.DrawString(font, TowerPlacer.ToString(), new Vector2(2, 2), Color.CornflowerBlue);
                    spriteBatch.DrawString(font, "$" + money, new Vector2(2, 20), Color.CornflowerBlue);
                    spriteBatch.DrawString(font, weapon.ToString(), new Vector2(2, Constant.higth - 20), Color.CornflowerBlue);
                    spriteBatch.DrawString(font, "HP: " + Health.ToString(), new Vector2(2, Constant.higth - 40), Color.CornflowerBlue);
                    spriteBatch.DrawString(font, this.ToString(), new Vector2(2, Constant.higth - 60), Color.CornflowerBlue);
                }
                else if (control == Controls.UDLR)
                {
                    spriteBatch.DrawString(font, TowerPlacer.ToString(), new Vector2(Constant.width - font.MeasureString(TowerPlacer.ToString()).X - 2, 2), Color.YellowGreen);
                    spriteBatch.DrawString(font, "$" + money, new Vector2(Constant.width - font.MeasureString(money + " $").X - 2, 20), Color.YellowGreen);
                    spriteBatch.DrawString(font, weapon.ToString(), new Vector2(Constant.width - font.MeasureString(weapon.ToString()).X - 2, Constant.higth - 20), Color.YellowGreen);
                    spriteBatch.DrawString(font, "HP: " + Health.ToString(), new Vector2(Constant.width - font.MeasureString("HP: " + Health.ToString()).X - 2, Constant.higth - 40), Color.YellowGreen);
                    spriteBatch.DrawString(font, this.ToString(), new Vector2(Constant.width - font.MeasureString(this.ToString()).X - 2, Constant.higth - 60), Color.YellowGreen);
                }
                DrawLootToString(spriteBatch);


            }
        }

        /// <summary>
        /// Draws the amount of seconds untill respawn
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawRespawnTime(SpriteBatch spriteBatch)
        {
            if (IsAlive == false)
            {
                float timeLeft = deathTimeStamp + Constant.respawntime - GameWorld.Instance.TotalGameTime;

                string timeLeftString = Convert.ToInt32(timeLeft).ToString();
                if (control == Controls.WASD)
                {
                    spriteBatch.DrawString(font, Convert.ToInt32(timeLeft).ToString(), new Vector2(2, 2), Color.CornflowerBlue);
                }
                else if (control == Controls.UDLR)
                {

                    spriteBatch.DrawString(font, Convert.ToInt32(timeLeft).ToString(), new Vector2(Constant.width - font.MeasureString(timeLeftString).X - 2, 2), Color.YellowGreen);
                }

            }
        }
        /// <summary>
        /// gives the vehicle the basic weapon (for when weapon runs out of ammo)
        /// </summary>
        public void GetBasicGun()
        {
            this.weapon = new BasicWeapon(this.GameObject);

        }

        /// <summary>
        /// Displays the loot from crates on screen with the color of the player and the position of the looting
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void DrawLootToString(SpriteBatch spriteBatch)
        {

            if (latestLootCrate != null)
            {
                if (lootTimeStamp + 3 >= GameWorld.Instance.TotalGameTime) //amount of time text is showed on screen
                {
                    if (control == Controls.WASD)//p1
                    {
                        spriteBatch.DrawString(font, latestLootCrate.ToString(), latestLootCrate.GameObject.Transform.Position, Color.CornflowerBlue);
                    }
                    else if (control == Controls.UDLR)//p2
                    {
                        spriteBatch.DrawString(font, latestLootCrate.ToString(), latestLootCrate.GameObject.Transform.Position, Color.YellowGreen);
                    }
                }
                else
                {
                    //expires
                    latestLootCrate = null;
                }
            }
        }
        /// <summary>
        /// Respawns the vehicle
        /// </summary>
        public void Respawn(int playerNumber)
        {
            var tmp = GameObjectDirector.Instance.Construct(vehicleType, control, playerNumber);

            foreach (Component comp in tmp.GetComponentList)
            {
                if (comp is Vehicle)
                {
                    (comp as Vehicle).spriteRenderer.Sprite = this.spriteRenderer.Sprite;
                    (comp as Vehicle).Stats = this.stats;
                    (comp as Vehicle).weapon = this.weapon;
                    (comp as Vehicle).towerPlacer = this.towerPlacer;
                    (comp as Vehicle).Money = this.money;

                }
            }
            GameWorld.Instance.Vehicles.Remove(this);
            GameWorld.Instance.VehiclesToRemove.Clear();

        }
        public override string ToString()
        {
            return vehicleType.ToString();
        }
    }
}
