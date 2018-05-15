﻿using System;
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
    class Vehicle : Component, IAnimatable, IUpdatable, ILoadable
    {
        public Animator animator;
        protected int health;
        protected Controls control;
        protected float movementSpeed;
        protected float fireRate;
        protected float rotation = 0;
        protected float rotateSpeed;
        protected SpriteRenderer spriteRenderer;

        /// <summary>
        /// creates a vehicle
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public Vehicle(GameObject gameObject, Controls control, int health, float movementSpeed, float fireRate, float rotateSpeed) : base(gameObject)
        {
            this.control = control;
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.fireRate = fireRate;
            this.rotateSpeed = rotateSpeed;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        /// <summary>
        /// handles what happens when a vehicle dies
        /// </summary>
        protected virtual void Die()
        {
            Console.WriteLine(new NotImplementedException("die Vehicle"));
        }

        /// <summary>
        /// Handles the vehicles movement etc...
        /// </summary>
        public virtual void Update()
        {
            Vector2 translation = Vector2.Zero;
            //is the player Rotating?
            Rotate(translation);
            //Is the player moving
            translation = Move(translation);
            //calculate direction of movement
            translation = RotateMove(translation);
            //move the vehicle
            TranslateMovement(translation);
            //rotate sprite
            spriteRenderer.Rotation = rotation;
        }

        /// <summary>
        /// moves the vehicle
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 Move(Vector2 translation)
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.W) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Up) && control == Controls.UDLR))
            {
                translation += new Vector2(0, -1);
            }
            else if ((keyState.IsKeyDown(Keys.S) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Down) && control == Controls.UDLR))
            {
                translation += new Vector2(0, 1);
            }
            return translation;
        }

        /// <summary>
        /// Rotates the vehicle depending on the reotateSpeed
        /// </summary>
        /// <param name="translation"></param>
        public void Rotate(Vector2 translation)
        {
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.D) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Right) && control == Controls.UDLR))
            {
                rotation += rotateSpeed;
            }
            if ((keyState.IsKeyDown(Keys.A) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Left) && control == Controls.UDLR))
            {
                rotation -= rotateSpeed;
            }
        }

        /// <summary>
        /// Returns a rotated version of the given translation
        /// </summary>
        /// <param name="translation"></param>
        /// <returns></returns>
        public Vector2 RotateMove(Vector2 translation)
        {
            return Vector2.Transform(translation, Matrix.CreateRotationZ(MathHelper.ToRadians(rotation)));
        }

        /// <summary>
        /// Makes the vehicle actually move
        /// </summary>
        /// <param name="translation"></param>
        /// <param name="movementSpeed"></param>
        public void TranslateMovement(Vector2 translation)
        {
            GameObject.Transform.Translate(translation * GameWorld.Instance.DeltaTime * movementSpeed);
        }

        /// <summary>
        /// handles animation for the vehicle
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            Console.WriteLine(new NotImplementedException("OnAnimationDone Vehicle"));
        }

        /// <summary>
        /// loads the vehicle sprite
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("Idle");
        }

        public virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("Idle", new Animation(1, 0, 0, 20, 40, 3, Vector2.Zero));
        }
    }
}