using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MoneyCrate : Crate
    {
        private int amount;

        public MoneyCrate(GameObject gameObject ) : base(gameObject)
        {
            int half = Constant.moneyCrateMoney / 2;
            int oneAndAHalf = (half*3)+1;

            this.amount = GameWorld.Instance.Rnd.Next(half, oneAndAHalf);
                       
        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        public override string ToString()
        {
            return "+$"+amount;
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
            vehicle.Money += amount;

            vehicle.Stats.TotalAmountOfGold += amount;

            vehicle.LatestLootCrate = this;
        }
    }
}
