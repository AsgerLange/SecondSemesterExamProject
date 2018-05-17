using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    enum BulletType { BaiscBullet };
    class Bullet : Component, IUpdatable, ILoadable,IAnimatable
    {
        #region Attributes for object pool
        private bool canRelease;
        private BulletType bulletType;
        private Vector2 direction;
        private float rotation;
        private float movementSpeed;

        private SpriteRenderer spriteRenderer;
        private Animator animator;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;

        public Bullet(GameObject gameObject, BulletType type) : base(gameObject)
        {
            canRelease = true;
            this.bulletType = type;
            this.direction = new Vector2(0, 0);
            movementSpeed = Constant.basicBulletMovementSpeed;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        public void Update()
        {
            MoveTowardsTarget(EnemyPool.ActiveEnemies[0]);

            spriteRenderer.Rotation = rotation;
        }
        /// <summary>
        /// Moves towards a targeted gameobject
        /// </summary>
        /// <param name="go"></param>
        private void MoveTowardsTarget(GameObject go)
        {
            //TODO: MAKE BULLET TRAVEL TOWARDS LOCATION

            float x = go.Transform.Position.X;
            float y = go.Transform.Position.Y;

            Vector2 direction = new Vector2(x - this.GameObject.Transform.Position.X, y - this.GameObject.Transform.Position.Y);
            direction.Normalize();


            rotation = GetDegreesFromDestination(direction);

            TranslateMovement(direction);
        }
        /// <summary>
        /// Makes the Bullet actually move
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="movementSpeed"></param>
        public void TranslateMovement(Vector2 translation)
        {
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);
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

    
        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }
        public virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 1, 22, 3, Vector2.Zero));
        }
        public void OnAnimationDone(string animationName)
        {
            Console.WriteLine(new  NotImplementedException()); 
        }
    }
}
