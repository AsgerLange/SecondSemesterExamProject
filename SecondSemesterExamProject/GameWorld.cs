using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TankGame
{
    enum GameState
    {
        Menu,
        Game,
        GameOver,
        Score

    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class GameWorld : Game
    {
        private GameState gameState = new GameState();
        private Menu menu;
        public static readonly object colliderKey = new object();

        public static Barrier barrier;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static GameWorld instance;
        private List<GameObject> gameObjectsToAdd = new List<GameObject>(); //list of all gameobjects
        private List<GameObject> gameObjects = new List<GameObject>(); //list of all gameobjects
        private List<GameObject> gameObjectsToRemove = new List<GameObject>(); //list of all gameobjects to be removed
        private List<GameObject> vehiclesToRemove = new List<GameObject>();
        private List<Vehicle> vehicles = new List<Vehicle>();

        private List<Collider> colliders = new List<Collider>();
        public bool gameRunning = true;
        private float deltaTime;
        private float totalGameTime;
        private Map map;
        private Spawn spawner;
        private Random rnd = new Random();
        private int playerAmount;
        private int towerAmount;

        private GameOver gameOver;
        Score score;

        //Background
        public Texture2D backGround;
        public Rectangle screenSize;
        public GameOver GetGameOver
        {
            get { return gameOver; }
        }
        public List<Collider> Colliders
        {
            get
            {
                lock (colliderKey)
                {
                    return colliders;
                }
            }
            set
            {
                lock (colliderKey)
                {
                    colliders = value;
                }
            }
        }
        public float TotalGameTime
        {
            get { return totalGameTime; }
        }

        public List<GameObject> GameObjectsToRemove
        {
            get { return gameObjectsToRemove; }
            set { gameObjectsToRemove = value; }
        }

        public GameState GetGameState
        {
            get { return gameState; }
            set { gameState = value; }
        }

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        public List<GameObject> GameObjectsToAdd
        {
            get { return gameObjectsToAdd; }
            set { gameObjectsToAdd = value; }
        }
        public List<GameObject> VehiclesToRemove
        {
            get { return vehiclesToRemove; }
            set { vehiclesToRemove = value; }
        }
        public List<Vehicle> Vehicles
        {
            get { return vehicles; }
            set { vehicles = value; }
        }
        public float DeltaTime
        {
            get { return deltaTime; }
        }

        public Random Rnd
        {
            get { return rnd; }
        }
        public int PlayerAmount
        {
            get { return playerAmount; }
            set { playerAmount = value; }
        }
        public int TowerAmount
        {
            get { return towerAmount; }
            set { towerAmount = value; }
        }
        public Spawn GetSpawn
        {
            get { return spawner; }
            set { spawner = value; }
        }

        public Menu GetMenu
        {
            get { return menu; }
        }

        /// <summary>
        /// Creates a Singleton Gameworld instance
        /// </summary>
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = Constant.hight;//Changes Window Size
            graphics.PreferredBackBufferWidth = Constant.width;//Changes Window Size
            this.Window.Position = new Point(0, 0);
            this.Window.Title = Constant.title;
            graphics.ApplyChanges();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            // graphics.ToggleFullScreen();

            IsMouseVisible = true;


            // TODO: Add your initialization logic here
            //sets the game up to start in the menu
            gameState = GameState.Menu;
            menu = new Menu();

            gameOver = new GameOver();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //initializes the barrier
            barrier = new Barrier(2);

            //secure that enemyPool has been started
            EnemyPool ep = EnemyPool.Instance;

            //adds objects to the map
            map = new Map();

            //Creates the new spawner that spawns the waves
            spawner = new Spawn(Constant.width, Constant.hight);


            //creates a score to keep track of scores and stats
            score = new Score();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            score.LoadContent(Content);
            backGround = Content.Load<Texture2D>(Constant.gameBackGround);
            screenSize = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // TODO: use this.Content to load your game content here
            //loads menu content
            menu.LoadContent(Content);

            gameOver.LoadContent(Content);
            //load objects
            foreach (var go in gameObjects)
            {
                go.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (gameState == GameState.Menu)
            {
                menu.Update();
            }
            else if (gameState == GameState.Game)
            {
                barrier.SignalAndWait();
                // Updates the Time
                deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //adds Gameobjects
                AddGameObjects();

                //call the Spawner
                spawner.Update();

                //Updates GameObjects
                foreach (var go in gameObjects)
                {
                    go.Update();
                }

                //Checks if any vehicle needs to respawn
                if (vehiclesToRemove.Count > 0)
                {
                    foreach (GameObject go in vehiclesToRemove)
                    {
                        foreach (Component comp in go.GetComponentList)
                        {
                            if (comp is Vehicle)
                            {
                                if ((comp as Vehicle).DeathTimeStamp + Constant.respawntime <= totalGameTime)
                                {
                                    (comp as Vehicle).Respawn((comp as Vehicle).PlayerNumber);

                                    VehiclesToRemove.Clear();

                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                lock (BulletPool.activeListKey)
                {
                    foreach (var go in BulletPool.ActiveBullets)
                    {
                        go.Update();
                    }
                }
                BulletPool.ReleaseList();
                RemoveObjects();
            }
            else if (gameState == GameState.GameOver)
            {
                gameOver.Update();
            }
            else if (gameState == GameState.Score)
            {
                //handles score funktions
                score.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// adds GameObjects
        /// </summary>
        private void AddGameObjects()
        {
            if (gameObjectsToAdd.Count > 0)
            {
                foreach (GameObject go in gameObjectsToAdd)
                {
                    gameObjects.Add(go);
                    UpdatePlayerAmount();

                }
                gameObjectsToAdd.Clear();
            }
            if (UpdatePlayerAmount() <= 0 && vehicles.Count > 1)
            {
                GameOver();
            }
        }

        /// <summary>
        /// Removes dead objects
        /// </summary>
        private void RemoveObjects()
        {
            if (vehiclesToRemove.Count > 0)
            {
                foreach (GameObject go in vehiclesToRemove)
                {
                    gameObjectsToRemove.Add(go);
                }
                UpdatePlayerAmount();
            }
            if (gameObjectsToRemove.Count > 0)
            {
                foreach (var go in gameObjectsToRemove)
                {
                    if (go.GetComponent("Collider") is Collider collider)
                    {
                        lock (colliderKey)
                        {
                            Colliders.Remove(collider);
                        }
                    }
                    gameObjects.Remove(go);
                }
                gameObjectsToRemove.Clear();
            }


        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront);

            if (gameState == GameState.Menu)
            {
                menu.Draw(spriteBatch);
            }
            else if (gameState == GameState.Game)
            {
                //Draw Gameobjects
                foreach (var go in gameObjects)
                {
                    go.Draw(spriteBatch);
                }

                lock (EnemyPool.activeKey)
                {
                    foreach (var go in EnemyPool.Instance.ActiveEnemies)
                    {
                        go.Draw(spriteBatch);
                    }
                }
                lock (BulletPool.activeListKey)
                {
                    foreach (var go in BulletPool.ActiveBullets)
                    {
                        go.Draw(spriteBatch);
                    }
                }
                DrawVehiclesRespawnTimeRemaining(spriteBatch);

                spriteBatch.Draw(backGround, screenSize, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            }
            else if (gameState == GameState.GameOver)
            {
                gameOver.Draw(spriteBatch);
            }
            else if (gameState == GameState.Score)
            {
                //draw score
                score.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        /// <summary>
        /// Draws the remaining time before respawn for the player who is dead.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawVehiclesRespawnTimeRemaining(SpriteBatch spriteBatch)
        {
            if (VehiclesToRemove.Count > 0)
            {

                foreach (GameObject go in vehiclesToRemove)
                {
                    foreach (Component comp in go.GetComponentList)
                    {
                        if (comp is Vehicle)
                        {
                            (comp as Vehicle).DrawRespawnTime(spriteBatch);
                        }
                    }
                }
            }
        }
        public int UpdatePlayerAmount()
        {
            PlayerAmount = 0;
            foreach (GameObject go in Instance.GameObjects)
            {
                foreach (Component comp in go.GetComponentList)
                {
                    if (comp is Vehicle)
                    {
                        PlayerAmount++;
                        break;
                    }
                }

            }
            return playerAmount;
        }

        /// <summary>
        /// sets the gamestate to gameOver
        /// </summary>
        public void GameOver()
        {
            this.gameState = GameState.GameOver;
        }
    }
}
