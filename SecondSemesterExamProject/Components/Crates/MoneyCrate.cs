using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MoneyCrate : LootCrate
    {
        private int amount;

        public MoneyCrate(GameObject gameObject, int amount) : base(gameObject)
        {
            int half = amount / 2;
            int oneAndAHalf = (amount + half)+1;

            this.amount = GameWorld.Instance.Rnd.Next(half, oneAndAHalf);
                       
        }

        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        public override string ToString()
        {
            return base.ToString();
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
        }
    }
}
