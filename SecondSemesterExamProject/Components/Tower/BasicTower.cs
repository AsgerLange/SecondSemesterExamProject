using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace TankGame
{
    class BasicTower : Tower
    {

        public BasicTower(GameObject gameObject): base(gameObject)
        {
            this.attackRate = Constant.basicTowerFireRate;
            this.health = Constant.basicTowerHealth;
            this.attackRange = Constant.basicTowerAttackRange;
            this.bulletType = Constant.basicTowerBulletType;
            this.spread = Constant.basicTowerSpread;
        }
        /// <summary>
        /// loads content
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            shootSound = content.Load<SoundEffect>("BasicWeaponShot");

            base.LoadContent(content);
        }

        /// <summary>
        /// Handles what happens when an animation ends
        /// </summary>
        /// <param name="animationName"></param>
        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        /// <summary>
        /// handles what happens when this collider enters another collider
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollisionEnter(Collider other)
        {
            base.OnCollisionEnter(other);
        }

        /// <summary>
        /// updates
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// Creates animations for basicTower
        /// </summary>
        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(5, 32, 0, 32, 32, 8, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(6, 64, 0, 32, 32, 6, Vector2.Zero));


            base.CreateAnimation();

        }

        /// <summary>
        /// handles death
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }
        /// <summary>
        /// handles BasicTower's Shooting behaviour
        /// </summary>
        protected override void Shoot()
        {

            base.Shoot();
        }


        /// <summary>
        /// Plays shoot sound effect
        /// </summary>
        protected override void PlayShootSoundEffect()
        {
            shootSound.Play(0.4f, 0, 0);
          

        }
    }
}
