using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Rock : Component, ICollisionStay
    {
        private SpriteRenderer spriteRenderer;

        public Rock(GameObject gameObject, float size, float rotation, Alignment alignment) : base(gameObject)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            spriteRenderer.Scale = 1 * size / 100;
            spriteRenderer.Rotation = rotation;
        }

        public void OnCollisionStay(Collider other)
        {
            // force is how forcefully we will push the player away from the enemy.
            float force = Constant.rockPushForce;

            // If the object we hit is the enemy
            if (other.GetAlignment != Alignment.Neutral)
            {
                Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                dir.Normalize();

                other.GameObject.Transform.Translate(dir * force);
                Console.WriteLine("push: " + dir);
            }
        }
    }
}
