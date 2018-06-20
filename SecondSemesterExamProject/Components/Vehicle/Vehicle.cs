using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace TankGame
{
    enum Controls { WASD, UDLR }
    class Vehicle : Component, IAnimatable, IUpdatable, ILoadable, ICollisionEnter, IDrawable
    {
        private Random rnd = new Random();
        private SpriteFont font;
        public Animator animator;
        private Stats stats;
        protected SoundEffect vehicleDeathSound;

        protected Weapon weapon;
        protected TowerPlacer towerPlacer;
        protected int health;
        protected int maxHealth;

        protected int money;
        protected Controls control;
        protected VehicleType vehicleType;
        public Alignment alignment;

        protected float movementSpeed;

        protected float rotation = 0;
        protected float rotateSpeed;
        protected SpriteRenderer spriteRenderer;
        private Texture2D aimLine;
        private Vector2 movementDirection;

        protected float shotTimeStamp; //when a vehicle last fired its weapon
        protected float builtTimeStamp; //when a vehicle last built a tower

        protected float lootTimeStamp; // when a vehicle received loot
        private Crate latestLootCrate; //For displaying reward
        private float deathTimeStamp;
        private static float muteTimeStamp;

        public int PlayerNumber { get; set; }
        public bool IsAlive { get; set; }

        protected bool isPlayingAnimation = false;
        private bool hasSetup = false;

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
                    if (IsAlive)
                    {
                        animator.PlayAnimation("Death");
                        IsAlive = false;
                        health = 0;
                        PlayDeathSound();
                        isPlayingAnimation = true;
                    }
                    else
                    {
                        animator.PlayAnimation("Death"); //just in case

                    }
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
            if (GameWorld.Instance.pvp == true)
            {
                this.health = health * Constant.pvpHealthModifier;
                this.maxHealth = this.health;
            }
            else
            {
                this.health = health;
                this.maxHealth = this.health;

            }
            this.movementSpeed = movementSpeed;
            this.rotateSpeed = rotateSpeed;
            this.money = money;
            this.stats = new Stats(this);
            this.PlayerNumber = playerNumber;
            this.towerPlacer = new TowerPlacer(this, TowerType.BasicTower, 100000);

            IsAlive = true;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
            Setup();
        }

        /// <summary>
        /// handles what happens when a vehicle dies
        /// </summary>
        protected virtual void Die()
        {
            this.health = maxHealth;
            deathTimeStamp = GameWorld.Instance.TotalGameTime;
            GameWorld.Instance.VehiclesToRemove.Add(this.GameObject);
            GameWorld.Instance.UpdatePlayerAmount();
            this.stats.PlayerDeathAmmount++;

            if (this.stats.PlayerDeathAmmount >= Constant.maxDeaths && GameWorld.Instance.pvp)
            {
                GameWorld.Instance.GameOver();
            }
        }

        public void Setup()
        {
            if (hasSetup == false)
            {

                foreach (Component comp in GameObject.GetComponentList)
                {
                    if (comp is Collider)
                    {
                        this.alignment = (comp as Collider).GetAlignment;
                        break;
                    }
                }
                hasSetup = true;
            }
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

                ToggleMusic();
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
            if (((keyState.IsKeyDown(Keys.F) || (keyState.IsKeyDown(Keys.Space))) && control == Controls.WASD)
                || ((keyState.IsKeyDown(Keys.OemComma) || (keyState.IsKeyDown(Keys.Enter))) && control == Controls.UDLR))
            {

                //if enough time has passed since last shot
                if ((shotTimeStamp + weapon.FireRate) <= GameWorld.Instance.TotalGameTime)
                {

                    weapon.Shoot(alignment, Rotation); //Fires the weapon


                    if (weapon is MachineGun)
                    {
                        animator.PlayAnimation("ShootMachinegun"); //play shooting animation
                    }
                    else
                    {
                        animator.PlayAnimation("Shoot"); //play shooting animation
                    }

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

            if (((keyState.IsKeyDown(Keys.LeftAlt) || (keyState.IsKeyDown(Keys.G))) && control == Controls.WASD)
                 || ((keyState.IsKeyDown(Keys.OemPeriod) || (keyState.IsKeyDown(Keys.Back))) && control == Controls.UDLR))
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
            movementDirection = translation;

            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);

        }

        /// <summary>
        /// handles animation for the vehicle
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            if ((animationName == "Shoot" || animationName == "ShootMachinegun" )&& IsAlive)
            {
                isPlayingAnimation = false;
                spriteRenderer.Offset = Vector2.Zero;
            }
            if (animationName == "Death")
            {
                Die();
                isPlayingAnimation = false;
            }
            if (isPlayingAnimation == false && IsAlive)
            {
                animator.PlayAnimation("Idle");

            }
            else if (IsAlive == false)
            {
               animator.PlayAnimation("Death");
                isPlayingAnimation = true;

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
            aimLine = content.Load<Texture2D>("AimLine");
            CreateAnimation();
            vehicleDeathSound = content.Load<SoundEffect>("VehicleDeath");

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

            if (weapon is Sniper && GameWorld.Instance.GetGameState == GameState.Game)
            {
                DrawShotDirection(this.GameObject.Transform.Position, GetDirectionVectorFromRotation(),
                    spriteBatch);
            }
        }

        /// <summary>
        /// Draws a line between two points
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="spriteBatch"></param>
        private void DrawShotDirection(Vector2 position, Vector2 direction, SpriteBatch spriteBatch)
        {
            direction = direction * Constant.aimLineLenght;

            direction = position + direction;

            float angle = (float)Math.Atan2(position.Y - direction.Y, position.X - direction.X);
            float distance = Vector2.Distance(position, direction);

            spriteBatch.Draw(aimLine, new Rectangle((int)direction.X, (int)direction.Y, (int)distance, 1),
                null, Color.Red, angle, Vector2.Zero, SpriteEffects.None, 1);
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
                    spriteBatch.DrawString(font, weapon.ToString(), new Vector2(2, Constant.hight - 20), Color.CornflowerBlue);
                    spriteBatch.DrawString(font, "HP: " + Health.ToString(), new Vector2(2, Constant.hight - 40), Color.CornflowerBlue);
                    spriteBatch.DrawString(font, this.ToString(), new Vector2(2, Constant.hight - 60), Color.CornflowerBlue);
                }
                else if (control == Controls.UDLR)
                {
                    spriteBatch.DrawString(font, TowerPlacer.ToString(), new Vector2(Constant.width - font.MeasureString(TowerPlacer.ToString()).X - 2, 2), Color.YellowGreen);
                    spriteBatch.DrawString(font, "$" + money, new Vector2(Constant.width - font.MeasureString(money + " $").X - 2, 20), Color.YellowGreen);
                    spriteBatch.DrawString(font, weapon.ToString(), new Vector2(Constant.width - font.MeasureString(weapon.ToString()).X - 2, Constant.hight - 20), Color.YellowGreen);
                    spriteBatch.DrawString(font, "HP: " + Health.ToString(), new Vector2(Constant.width - font.MeasureString("HP: " + Health.ToString()).X - 2, Constant.hight - 40), Color.YellowGreen);
                    spriteBatch.DrawString(font, this.ToString(), new Vector2(Constant.width - font.MeasureString(this.ToString()).X - 2, Constant.hight - 60), Color.YellowGreen);
                }
                DrawLootToString(spriteBatch);
                spriteBatch.DrawString(font, "Towers: " + GameWorld.Instance.TowerAmount + "/" + Constant.maxTowerAmount,
                    new Vector2(Constant.width / 2 - 50, Constant.hight - 50), Color.Gold);

                if (GameWorld.Instance.pvp == false)
                {
                    spriteBatch.DrawString(font, "Wave: " + GameWorld.Instance.GetSpawn.Wave,
                        new Vector2(Constant.width / 2 - 50, Constant.hight - 70), Color.Gold);

                }
            }
        }

        public void DrawDeaths(SpriteBatch spriteBatch)
        {
            if (GameWorld.Instance.pvp)
            {
                if (control == Controls.WASD)
                {
                    spriteBatch.DrawString(font, stats.PlayerDeathAmmount.ToString(), new Vector2(Constant.width / 2 + 20, 20), Color.YellowGreen);

                }
                else
                {
                    spriteBatch.DrawString(font, stats.PlayerDeathAmmount.ToString(), new Vector2(Constant.width / 2 - 20, 20), Color.CornflowerBlue);

                }

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
                float timeLeft;
                if (GameWorld.Instance.pvp)
                {

                    timeLeft = deathTimeStamp + Constant.respawntime / 2 - GameWorld.Instance.TotalGameTime;
                }
                else
                {
                    timeLeft = deathTimeStamp + Constant.respawntime - GameWorld.Instance.TotalGameTime;

                }

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
        public virtual void GetBasicGun()
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
                if (lootTimeStamp + 3 /* Seconds*/>= GameWorld.Instance.TotalGameTime) //amount of time text is showed on screen
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
            animator.PlayAnimation("Idle");
            isPlayingAnimation = false;
            this.IsAlive = true;
            GetBasicGun();
            GameWorld.Instance.Colliders.Add((Collider)GameObject.GetComponent("Collider"));
            GameWorld.Instance.VehiclesToRemove.Remove(this.GameObject);
            GameWorld.Instance.UpdatePlayerAmount();

            if (GameWorld.Instance.pvp==true)
            {
                GameObject.Transform.Position = new Vector2(GameWorld.Instance.Rnd.Next(75,Constant.width-75), GameWorld.Instance.Rnd.Next(75,Constant.hight-75));
            }
            else
            {
                GameObject.Transform.Position = new Vector2(Constant.width / 2, Constant.hight / 2);

            }

            GameWorld.Instance.GameObjectsToAdd.Add(this.GameObject);




            //var tmp = GameObjectDirector.Instance.Construct(vehicleType, control, playerNumber, alignment);

            //foreach (Component comp in tmp.GetComponentList)
            //{
            //    if (comp is Vehicle)
            //    {
            //        (comp as Vehicle).spriteRenderer.Sprite = this.spriteRenderer.Sprite;
            //        (comp as Vehicle).Stats = this.stats;
            //        (comp as Vehicle).GetBasicGun();


            //        (comp as Vehicle).Money = this.money;
           // GameWorld.Instance.GameObjectsToAdd.Add(tmp);
            // GameWorld.Instance.VehiclesToRemove.Remove(this.GameObject);
            //    }
            //}

        }
        public override string ToString()
        {
            return vehicleType.ToString();
        }

        /// <summary>
        /// Gets a direction vector from degrees(rotation)
        /// </summary>
        /// <returns></returns>
        private Vector2 GetDirectionVectorFromRotation()
        {
            return Vector2.Transform(new Vector2(0, -1),
                Matrix.CreateRotationZ(MathHelper.ToRadians(this.rotation)));

        }

        private void ToggleMusic()
        {
            KeyboardState keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.M) && GameWorld.Instance.TotalGameTime > muteTimeStamp + 1 && GameWorld.Instance.MusicIsPlaying)
            {
                GameWorld.Instance.StopMusic();
                muteTimeStamp = GameWorld.Instance.TotalGameTime;
            }
            else if (keyState.IsKeyDown(Keys.M) && GameWorld.Instance.TotalGameTime > muteTimeStamp + 1 && GameWorld.Instance.MusicIsPlaying == false)
            {
                GameWorld.Instance.PlayBackgroundSong();
                muteTimeStamp = GameWorld.Instance.TotalGameTime;

            }
        }
        protected virtual void PlayDeathSound()
        {
            vehicleDeathSound.Play(1f, 0f, 0);
        }

    }
}
