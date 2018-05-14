using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecondSemesterExamProject
{
    enum Controls { WASD, UDLR }
    class Vehicle : Component, IAnimatable, IUpdatable, IDrawable, ILoadable
    {
        public Animator animator;
        protected int health;
        protected Controls control;
        protected float movementSpeed;
        protected float fireRate;
        protected SpriteRenderer spriteRenderer;

        public Vehicle(Controls control, int health, float movementSpeed, float fireRate)
        {
            this.control = control;
            this.health = health;
            this.movementSpeed = movementSpeed;
            this.fireRate = fireRate;

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.UseRect = true;
        }

        protected virtual void Die()
        {

        }

        /// <summary>
        /// Handles the vehicles movement etc...
        /// </summary>
        public virtual void Update()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// draws the vehicle
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// handles animation for the vehicle
        /// </summary>
        /// <param name="animationName"></param>
        public virtual void OnAnimationDone(string animationName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// loads the vehicle sprite
        /// </summary>
        /// <param name="content"></param>
        public virtual void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");

            CreateAnimation();

            animator.PlayAnimation("IdleDown");
        }

        public virtual void CreateAnimation()
        {
            //EKSEMPEL
            animator.CreateAnimation("IdleDown", new Animation(5, 0, 0, 64, 112, 3, Vector2.Zero));
        }
    }
}
