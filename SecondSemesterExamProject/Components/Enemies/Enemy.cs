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

    class Enemy : Component, IAnimatable, IUpdatable, ILoadable, ICollisionStay
    {

        public Animator animator;
        private SpriteRenderer spriteRenderer;

        protected GameObject targetGameObject = GameWorld.Instance.GameObjects[0]; //HQ by default

        protected float rotation = 0;
        protected float movementSpeed;
        protected float attackRate;

        protected int health;

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
        public Enemy(GameObject gameObject, int health, float movementSpeed, float attackRate) : base(gameObject)
        {
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.attackRate = attackRate;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;


            FollowHQ();
        }

        /// <summary>
        /// finds the 
        /// </summary>
        private void FollowHQ()
        {
            foreach (var go in GameWorld.Instance.GameObjects)
            {
                if (go.GetComponent("HQ") is HQ)
                {
                    targetGameObject = go;
                    break;
                }
            }
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

            MoveTo(targetGameObject); //Enemy moves towards player1

            spriteRenderer.Rotation = rotation;//Rotates the sprite so it fits with the gameobject

        }

        public virtual void Update()
        {
            AI();
        }



        /// <summary>
        /// Moves towards a targeted gameobject
        /// </summary>
        /// <param name="go"></param>
        private void MoveTo(GameObject go)
        {
            float x = go.Transform.Position.X;
            float y = go.Transform.Position.Y;

            Vector2 direction = new Vector2(x - this.GameObject.Transform.Position.X, y - this.GameObject.Transform.Position.Y);
            direction.Normalize();


            rotation = GetDegreesFromDestination(direction);

            TranslateMovement(direction);
        }

        /// <summary>
        /// Calculates the degrees from standart vector to destination vector.
        /// </summary>
        /// <param name="vector">The direction the enemy is moving</param>
        private float GetDegreesFromDestination(Vector2 destinationVec)
        {
            Vector2 positionVec = new Vector2(0, -1); //Standard position (UP)

            float toppart = 0;

            toppart += positionVec.X * destinationVec.X;
            toppart += positionVec.Y * destinationVec.Y;



            float destinationVector2 = 0; //destinationVec squared
            float positionVector2 = 0; //positionVec squared


            destinationVector2 += positionVec.X * positionVec.X;
            destinationVector2 += positionVec.Y * positionVec.Y;

            positionVector2 += destinationVec.X * destinationVec.X;
            positionVector2 += destinationVec.Y * destinationVec.Y;


            float bottompart = 0;
            bottompart = (float)Math.Sqrt(destinationVector2 * positionVector2);


            double returnValue = (float)Math.Acos(toppart / bottompart);

            returnValue *= 360.0 / (2 * Math.PI); //Coverts the radian to degrees

            if (destinationVec.X < 0)
            {
                returnValue -= (returnValue * 2);
            }

            return (float)returnValue;
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

        /// <summary>
        /// when somthing is inside the enemy
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionStay(Collider other)
        {
            if (other.GetAlignment != Alignment.Neutral)
            {
                float force = Constant.PushForce;

                Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                dir.Normalize();

                other.GameObject.Transform.Translate(dir * force);
            }
        }
    }
}
