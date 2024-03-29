﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace TankGame
{
    class ShotgunTower : Tower
    {
        public ShotgunTower(GameObject gameObject) : base(gameObject)
        {
            this.attackRate = Constant.ShotgunTowerFireRate;
            this.health = Constant.ShotgunTowerHealth;
            this.attackRange = Constant.shotgunTowerAttackRange;
            this.bulletType = Constant.ShotgunTowerBulletType;
            this.spread = Constant.ShotgunTowerSpread;

        }

        public override void LoadContent(ContentManager content)
        {
            shootSound = content.Load<SoundEffect>("ShotgunShot");

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

        protected override void CreateAnimation()
        {
            animator.CreateAnimation("Idle", new Animation(5, 32, 0, 32, 32, 8, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(6, 64, 0, 32, 32, 6, Vector2.Zero));


            base.CreateAnimation();
        }

        protected override void Die()
        {
            base.Die();
        }

        /// <summary>
        /// Standard shooting behaviour for shotgun tower (Shoots an amount of  pellets, with a wide spread
        /// </summary>
        protected override void Shoot()
        {
            if (shootTimeStamp + attackRate <= GameWorld.Instance.TotalGameTime)
            {
                Collider target;

                target = FindEnemiesInRange();

                if (target != null)
                {
                    Vector2 direction = new Vector2(target.CollisionBox.Center.X - GameObject.Transform.Position.X, target.CollisionBox.Center.Y - GameObject.Transform.Position.Y);
                    direction.Normalize();

                    float rotation = GetDegreesFromDestination(direction);
                    for (int i = 0; i < Constant.shotgunTowerPelletAmount; i++)
                    {
                        BulletPool.CreateBullet(GameObject, Alignment.Friendly, bulletType, rotation + (GameWorld.Instance.Rnd.Next(-spread, spread)));

                    }
                    shootTimeStamp = GameWorld.Instance.TotalGameTime;
                    PlayShootSoundEffect();
                }
            }
        }

        /// <summary>
        /// Plays shoot sound effect
        /// </summary>
        protected override void PlayShootSoundEffect()
        {
            shootSound.Play(0.2f, 0, 0);

        }
    }
}
