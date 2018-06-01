﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    class HQ : Tower, IDrawable
    {
        private SpriteFont font;
        public HQ(GameObject gameObject) : base(gameObject)
        {
            this.attackRate = Constant.HQFireRate;
            this.health = Constant.HQHealth;
            this.attackRange = Constant.HQAttackRange;
            this.bulletType = Constant.HQbulletType;
            this.spread = Constant.HQSpread;
        }

        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("Stat");
            base.LoadContent(content);
        }

        public override void OnAnimationDone(string animationName)
        {

            base.OnAnimationDone(animationName);
        }

        public override void OnCollisionEnter(Collider other)
        {
            base.OnCollisionEnter(other);
        }

        public override void Update()
        {
            base.Update();
        }

        protected override void Shoot()
        {
            base.Shoot();
        }

        protected override void CreateAnimation()
        {
            //HQ Animation
            animator.CreateAnimation("Idle", new Animation(6, 96, 0, 96, 96, 6, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(7, 192, 0, 96, 96, 4, Vector2.Zero));
            base.CreateAnimation();
        }

        /// <summary>
        /// handles what happens when a HQ dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
            GameWorld.Instance.GameOver();
        }

        /// <summary>
        /// draws HQ Health
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Health left: " + Health, new Vector2((Constant.width / 2) - (font.MeasureString(("Health left: " + Health)).X / 2), 2), Color.CornflowerBlue);
        }
    }
}
