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
    class Bullet : Component, IUpdatable, ILoadable, IAnimatable, ICollisionEnter
    {
        private bool canRelease;
        private BulletType bulletType;
        private Vector2 direction;
        private float rotation;
        private float movementSpeed;
        private float vehicleRotation;
        private float lifeSpan;
        private float timeStamp;

        private SpriteRenderer spriteRenderer;
        private Animator animator;

        #region Attributes for object pool
        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;


        public float VehicleRotation
        {
            get { return vehicleRotation; }
            set { vehicleRotation = value; }
        }
        public float LifeSpan
        {
            get { return lifeSpan; }
            set { lifeSpan = value; }
        }
        public float TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
        public Bullet(GameObject gameObject, BulletType type, float vehicleRotation) : base(gameObject)
        {
            canRelease = true;
            this.bulletType = type;
            this.direction = new Vector2(0, 0);
            movementSpeed = Constant.basicBulletMovementSpeed;
            this.vehicleRotation = vehicleRotation;
            this.lifeSpan = Constant.basicBulletLifeSpan;
            this.timeStamp = GameWorld.Instance.TotalGameTime;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        public void Update()
        {
            BulletMovement();
            spriteRenderer.Rotation = rotation;

            CheckIfBulletIsExpired();

        }

        /// <summary>
        /// Destroys bullet when it reaches the end of its lifespan
        /// </summary>
        public void CheckIfBulletIsExpired()
        {
            if (GameWorld.Instance.TotalGameTime >= (timeStamp + lifeSpan))
            {
                DestroyBullet();
            }
        }
        /// <summary>
        /// Handles movement for the bullet
        /// </summary>
        public void BulletMovement()
        {
            Vector2 translation = Vector2.Zero;
            //Bullet travels forward
            translation = MoveForward(translation);

            //"forward" is changed to fit the angle
            translation = RotateMove(translation);

            //Translates movemement
            TranslateMovement(translation);

            //Rotates the bullet to fit its direction
            rotation = GetDegreesFromDestination(translation);

        }
        /// <summary>
        /// Moves forwards
        /// </summary>
        /// <param name="go"></param>
        private Vector2 MoveForward(Vector2 translation)
        {
            translation += new Vector2(0, -1);

            return translation;
        }

        /// <summary>
        /// Returns a rotated version of the given translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 RotateMove(Vector2 translation)
        {
            return Vector2.Transform(translation, Matrix.CreateRotationZ(MathHelper.ToRadians(vehicleRotation)));
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
            Console.WriteLine(new NotImplementedException());
        }
        public void OnCollisionEnter(Collider other)
        {
            Collider thisCollider = (Collider)GameObject.GetComponent("Collider");

            if (thisCollider != null)
            {
                if (thisCollider.GetAlignment != other.GetAlignment)
                {
                    DestroyBullet();
                }
            }

        }
        /// <summary>
        /// Adds the bullet to the bulletpool's release list, allowing it to be recycled.
        /// </summary>
        public void DestroyBullet()
        {
            BulletPool.releaseList.Add(this.GameObject);
        }
    }
}
