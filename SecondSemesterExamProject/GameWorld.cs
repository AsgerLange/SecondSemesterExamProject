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
        private List<Collider> colliders = new List<Collider>();
        public bool gameRunning = true;
        private float deltaTime;
        private float totalGameTime;
        private Map map;
        private Spawn spawner;
        private bool gameOver = false;
        private Random rnd = new Random();
        Score score;


        //Background
        Texture2D backGround;
        Rectangle screenSize;

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
        public float DeltaTime
        {
            get { return deltaTime; }
        }

        public bool GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }
        public Random Rnd
        {
            get { return rnd; }
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
            graphics.PreferredBackBufferHeight = Constant.higth;//Changes Window Size
            graphics.PreferredBackBufferWidth = Constant.width;//Changes Window Size
            //this.Window.Position = new Point(0, 0);
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
            //graphics.ToggleFullScreen(); 

            // TODO: Add your initialization logic here
            //sets the game up to start in the menu
            gameState = GameState.Menu;
            menu = new Menu();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //secure that enemyPool has been started
            EnemyPool ep = EnemyPool.Instance;

            //initializes the barrier
            barrier = new Barrier(2);

            //adds objects to the map
            map = new Map();

            //Adds test player
            GameObject go;
            go = new GameObject();
            go.Transform.Position = new Vector2(650, 350);
            go.AddComponent(new SpriteRenderer(go, Constant.tankSpriteSheet, 0.2f));
            go.AddComponent(new Animator(go));
            go.AddComponent(new Plane(go, Controls.WASD, new MachineGun(go), Constant.planeHealth, Constant.planeMoveSpeed,
                Constant.planeFireRate, Constant.planeRotateSpeed, Constant.planeStartGold, TowerType.BasicTower));
            go.AddComponent(new Collider(go, Alignment.Friendly));
            gameObjects.Add(go);

            //adds player2
            //go = new GameObject();
            //go.Transform.Position = new Vector2(350, 350);
            //go.AddComponent(new SpriteRenderer(go, Constant.tankSpriteSheet2, 0.2f));
            //go.AddComponent(new Animator(go));
            //go.AddComponent(new Tank(go, Controls.UDLR, new Sniper(go), Constant.tankHealth, Constant.tankMoveSpeed,
            //    Constant.tankFireRate, Constant.tankRotateSpeed, Constant.tankStartGold, TowerType.BasicTower));
            //go.AddComponent(new Collider(go, Alignment.Friendly));
            //gameObjects.Add(go);

            //adds player2 Bike
            go = new GameObject();
            go.Transform.Position = new Vector2(350, 350);
            go.AddComponent(new SpriteRenderer(go, Constant.bikeSpriteSheet2, 0.2f));
            go.AddComponent(new Animator(go));
            go.AddComponent(new Bike(go, Controls.UDLR, new Shotgun(go), Constant.bikeHealth, Constant.bikeMoveSpeed,
                Constant.bikeFireRate, Constant.bikeRotateSpeed, Constant.bikeStartGold, TowerType.ShotgunTower));
            go.AddComponent(new Collider(go, Alignment.Friendly));
            gameObjects.Add(go);

            //Creates the new spawner that spawns the waves
            spawner = new Spawn(Constant.width, Constant.higth);


            //creates a score to keep track of scores and stats
            //score = new Score();

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
            //score.LoadContent(Content);
            backGround = Content.Load<Texture2D>("Background1");
            screenSize = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            // TODO: use this.Content to load your game content here
            //loads menu content
            menu.LoadContent(Content);
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
            if (gameState == GameState.Game)
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

                foreach (var go in BulletPool.ActiveBullets)
                {
                    go.Update();
                }
                BulletPool.ReleaseList();
                RemoveObjects();
            }

            //handles score funktions
            //score.Update(gameTime);

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
                }
                gameObjectsToAdd.Clear();
            }
        }

        /// <summary>
        /// Removes dead objects
        /// </summary>
        private void RemoveObjects()
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
            if (gameState == GameState.Game)
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
                foreach (var go in BulletPool.ActiveBullets)
                {
                    go.Draw(spriteBatch);
                }
                spriteBatch.Draw(backGround, screenSize, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            }
            //draw score
            //score.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
