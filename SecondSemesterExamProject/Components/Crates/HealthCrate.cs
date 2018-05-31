using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class HealthCrate : Crate
    {
        private int healthToGive;

        public HealthCrate(GameObject gameObject) : base(gameObject)
        {
            int half = Constant.moneyCrateHealth / 2;
            int oneAndAHalf = (half*3) + 1;

            this.healthToGive = GameWorld.Instance.Rnd.Next(half, oneAndAHalf);
        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        public override string ToString()
        {
            return "+" + healthToGive + " HP";
        }

        protected override void CreateAnimation()
        {
            base.CreateAnimation();
        }

        protected override void Die()
        {
            base.Die();
        }

        protected override void GiveLoot(Vehicle vehicle)
        {
            vehicle.Health += healthToGive;

            vehicle.LatestLootCrate = this;
        }
    }
}
