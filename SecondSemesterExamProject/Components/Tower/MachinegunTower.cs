using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MachineGunTower : Tower
    {

        public MachineGunTower(GameObject gameObject): base(gameObject)
        {
            this.attackRate = Constant.machineGunTowerFireRate;
            this.health = Constant.machineGunTowerHealth;
            this.attackRange = Constant.machineGunTowerAttackRange;
            this.bulletType = Constant.machineGunTowerBulletType;
            this.spread = Constant.machineGunTowerSpread;
        }
        /// <summary>
        /// loads content
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
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
            animator.CreateAnimation("Idle", new Animation(5, 32, 0, 32, 32, 4, Vector2.Zero));
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
    }
}
