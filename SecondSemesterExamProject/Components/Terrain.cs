using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Terrain : Component, ICollisionStay, ICollisionEnter
    {
        private SpriteRenderer spriteRenderer;

        public Terrain(GameObject gameObject, float size, float rotation, Alignment alignment) : base(gameObject)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
            GameObject.Transform.canMove = false;
            spriteRenderer.Scale = 1 * size / 100;
            spriteRenderer.Rotation = rotation;
        }

        /// <summary>
        /// what happens when something is inside the rock
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionStay(Collider other)
        {
            float force = Constant.pushForce * 2;

            if (other.GetAlignment != Alignment.Neutral)
            {
                Vector2 dir = other.GameObject.Transform.Position - GameObject.Transform.Position;
                dir.Normalize();

                other.GameObject.Transform.Translate(dir * force);
                Console.WriteLine("push: " + dir);
            }
        }

        /// <summary>
        /// what happens when something enters the rock
        /// </summary>
        /// <param name="other"></param>
        public void OnCollisionEnter(Collider other)
        {
            float force = Constant.pushForce;

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
