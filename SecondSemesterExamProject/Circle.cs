using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    public struct Circle
    {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        /// <summary>
        /// Circle Construct
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Checks if a point is in the circle.
        /// Point in circle collision.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Contains(Vector2 point)
        {
            Vector2 relativePosition = point - Center;
            float distanceBetweenPoints = relativePosition.Length();
            if (distanceBetweenPoints <= Radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a circle is in the circle.
        /// circle on circle collision.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(Circle other)
        {
            Vector2 relativePosition = other.Center - this.Center;
            float distanceBetweenCenters = relativePosition.Length();
            if (distanceBetweenCenters <= this.Radius + other.Radius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns a rectangle with the bounds of the circle
        /// </summary>
        public Rectangle Bounds
        {
            get { return new Rectangle((int)Center.X, (int)Center.Y, 2 * (int)Radius, 2 * (int)Radius); }
        }
    }
}
