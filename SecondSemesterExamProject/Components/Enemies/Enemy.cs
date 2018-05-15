using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace TankGame
{
    enum EnemyType { BasicEnemy, };

    class Enemy : Component, IAnimatable, IUpdatable, ILoadable
    {
        private bool shouldMoveRight = true; //delete

        public Animator animator;
        private SpriteRenderer spriteRenderer;

        protected float rotation = 0;
        protected float movementSpeed;
        protected float attackRate;

        protected int health;

        protected IEnemyAI action;
        protected Alignment alignment;

        #region Attributes for object pool
        private bool canRelease;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;

        /// <summary>
        /// Enemy Constructor
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="alignment">Alignment of the game object (enemy/friendly / neutral)</param>
        /// <param name="health">The amount of health the enemy should have</param>
        /// <param name="movementSpeed">Movement speed of the enemy</param>
        /// <param name="attackRate">the attackrate of the enemy</param>
        public Enemy(GameObject gameObject, Alignment alignment, int health, float movementSpeed, float attackRate) : base(gameObject)
        {
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.attackRate = attackRate;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;

        }

        /// <summary>
        /// Loads sprites and animations
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Idle");

        }

        /// <summary>
        /// The Standard behaviour of an Enemy
        /// </summary>
        public virtual void AI()
        {

            Vector2 translation = Vector2.Zero;

            translation = Move(translation); //Moves left and right depending on x position
            TranslateMovement(translation); // Translates the movement
            spriteRenderer.Rotation = rotation;//Rotates the sprite so it fits with the gameobject

        }

        public virtual void Update()
        {
            AI();
        }

        /// <summary>
        /// moves the Enemy
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 Move(Vector2 translation)
        {
            //DUMMY LOGIC REPLACE WITH ACTUAL AI
            if (this.GameObject.Transform.Position.X <= 501 && shouldMoveRight)
            {
                translation += new Vector2(1, 0);
                this.rotation = 90;

            }
            if (this.GameObject.Transform.Position.X <= 501 && shouldMoveRight == false)
            {
                translation += new Vector2(-1, 0);
                this.rotation = 270;

            }
            if (this.GameObject.Transform.Position.X >= 500)
            {
                shouldMoveRight = false;
                
            }
            if (this.GameObject.Transform.Position.X <= 400)
            {
                shouldMoveRight = true;
                
            }
            return translation;
        }

        /// <summary>
        /// Makes the Enemy actually move
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="movementSpeed"></param>
        public void TranslateMovement(Vector2 translation)
        {
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);
        }

        public virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 24, 24, 3, Vector2.Zero));
        }

        /// <summary>
        /// handles animation for the Enemy
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            Console.WriteLine(new NotImplementedException("OnAnimationDone Enemy"));
        }

        /// <summary>
        /// handles what happens when a enemy dies
        /// </summary>
        protected virtual void Die()
        {
            Console.WriteLine(new NotImplementedException("die enemy"));
        }
    }
}
