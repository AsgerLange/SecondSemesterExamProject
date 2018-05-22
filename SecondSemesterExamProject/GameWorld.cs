﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TankGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private static GameWorld instance;
        private List<GameObject> gameObjects = new List<GameObject>(); //list of all gameobjects
        private List<GameObject> gameObjectsToRemove = new List<GameObject>(); //list of all gameobjects to be removed
        private List<Collider> colliders = new List<Collider>();
        private float deltaTime;
        private float totalGameTime;
        private Map map;
        private Spawn spawner;

        private bool gameOver = false;

        //Background
        Texture2D backGround;
        Rectangle screenSize;

        public List<Collider> Colliders
        {
            get { return colliders; }
            set { colliders = value; }
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

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
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
            this.Window.Position = new Point(0, 0);
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

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //adds objects to the map
            map = new Map();

            //Adds test player
            GameObject go;
            go = new GameObject();
            go.Transform.Position = new Vector2(650, 350);
            go.AddComponent(new SpriteRenderer(go, Constant.tankSpriteSheet, 0.2f));
            go.AddComponent(new Animator(go));
            go.AddComponent(new Tank(go, Controls.WASD, Constant.tankHealth, Constant.tankMoveSpeed,
                Constant.tankFireRate, Constant.tankRotateSpeed, Constant.tankStartGold));
            go.AddComponent(new Collider(go, Alignment.Friendly));
            gameObjects.Add(go);

            //Creates the new spawner that spawns the waves
            spawner = new Spawn(Constant.width, Constant.higth);


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

            backGround = Content.Load<Texture2D>("testBackground");
            screenSize = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            // TODO: use this.Content to load your game content here

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
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Updates the Time
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalGameTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //call the Spawner
            spawner.Update();

            //Updates GameObjects
            foreach (var go in gameObjects)
            {
                go.Update();
            }
            foreach (var go in EnemyPool.ActiveEnemies)
            {
                go.Update();
            }
            EnemyPool.ReleaseList();

            foreach (var go in BulletPool.ActiveBullets)
            {
                go.Update();
            }
           BulletPool.ReleaseList();

            RemoveObjects();
            base.Update(gameTime);
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
                    Colliders.Remove(collider);
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
            //Draw Gameobjects
            foreach (var go in gameObjects)
            {
                go.Draw(spriteBatch);
            }
            foreach (var go in EnemyPool.ActiveEnemies)
            {
                go.Draw(spriteBatch);
            }
            foreach (var go in BulletPool.ActiveBullets)
            {
                go.Draw(spriteBatch);
            }
            spriteBatch.Draw(backGround, screenSize, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 1);
            spriteBatch.End();
            base.Draw(gameTime);
        }

       
    }
}
