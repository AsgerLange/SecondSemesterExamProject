﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    enum BulletType { BasicBullet, BiggerBullet };
    class Bullet : Component, IUpdatable, ILoadable, IAnimatable, ICollisionEnter
    {
        protected BulletType bulletType;
        protected int bulletDmg;
        protected float rotation;
        protected float movementSpeed;
        protected float dirRotation;
        protected float lifeSpan;
        protected float timeStamp;
        protected SpriteRenderer spriteRenderer;
        protected Animator animator;

        #region Attributes for object pool
        protected bool canRelease;
        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
        #endregion;

        public BulletType GetBulletType
        {
            get { return bulletType; }
        }
        public float DirRotation
        {
            get { return dirRotation; }
            set { dirRotation = value; }
        }
        /// <summary>
        /// The amount of seconds a bullet should survive
        /// </summary>
        public float LifeSpan
        {
            get { return lifeSpan; }
            set { lifeSpan = value; }
        }
        /// <summary>
        /// TimeStamp for when the bullet was created or recycled. - For controling firerate
        /// </summary>
        public float TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        public int BulletDamage
        {
            get { return bulletDmg; }
            set { bulletDmg = value; }
        }
        public Bullet(GameObject gameObject, BulletType type, float dirRotation) : base(gameObject)
        {
            canRelease = true;

            switch (type)
            {
                case BulletType.BasicBullet:
                    this.movementSpeed = Constant.basicBulletMovementSpeed;
                    this.lifeSpan = Constant.basicBulletLifeSpan;
                    this.bulletDmg = Constant.basicBulletDmg;
                    break;
                case BulletType.BiggerBullet:
                    this.movementSpeed = Constant.biggerBulletMovementSpeed;
                    this.lifeSpan = Constant.biggerBulletLifeSpan;
                    this.bulletDmg = Constant.biggerBulletDmg;
                    break;

                default:
                    break;
            }
            this.bulletType = type;
            movementSpeed = Constant.basicBulletMovementSpeed;
            this.dirRotation = dirRotation;

            this.timeStamp = GameWorld.Instance.TotalGameTime;
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        public virtual void Update()
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
            translation = RotateVector(translation);

            //Translates movemement
            TranslateMovement(translation);

            //Rotates the bullet to fit its direction
            rotation = GetDegreesFromDestination(translation);

        }

        /// <summary>
        /// Moves forwards
        /// </summary>
        /// <param name="go"></param>
        protected Vector2 MoveForward(Vector2 translation)
        {
            translation += new Vector2(0, -1);

            return translation;
        }

        /// <summary>
        /// Returns a rotated version of the given translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 RotateVector(Vector2 translation)
        {
            return Vector2.Transform(translation, Matrix.CreateRotationZ(MathHelper.ToRadians(dirRotation)));
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
        protected float GetDegreesFromDestination(Vector2 destinationVec)
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

        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }

        public virtual void CreateAnimation()
        {
            //PlaceHolder Animation
            //animator.CreateAnimation("Idle", new Animation(1, 0, 0, 1, 22, 3, Vector2.Zero));
        }

        public virtual void OnAnimationDone(string animationName)
        {

        }

        /// <summary>
        /// Handles what happens when bullet collides with other colliders
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollisionEnter(Collider other)
        {
            Collider thisCollider = (Collider)GameObject.GetComponent("Collider");

            if (thisCollider != null)
            {
                if (thisCollider.GetAlignment != other.GetAlignment)
                {
                    if (canRelease)
                    {
                        if (!(other.GameObject.GetComponent("HQ") is HQ) && thisCollider.GetAlignment == Alignment.Friendly || thisCollider.GetAlignment == Alignment.Enemy)
                        {

                            foreach (Component go in other.GameObject.GetComponentList)
                            {
                                if (go is Enemy && thisCollider.GetAlignment == Alignment.Friendly)
                                {
                                    (go as Enemy).Health -= bulletDmg;
                                }
                                if (go is Vehicle && thisCollider.GetAlignment == Alignment.Enemy)
                                {
                                    (go as Vehicle).Health -= bulletDmg;
                                }
                                if (go is Tower && thisCollider.GetAlignment == Alignment.Enemy)
                                {
                                    (go as Tower).Health -= bulletDmg;
                                }
                            }
                            DestroyBullet();

                        }
                    }
                }
            }

        }
        /// <summary>
        /// Adds the bullet to the bulletpool's release list, allowing it to be recycled.
        /// </summary>
        public void DestroyBullet()
        {
            canRelease = false;
            BulletPool.releaseList.Add(this.GameObject);
        }
    }
}
