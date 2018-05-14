using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    /// <summary>
    /// Makes it posible for the object to move
    /// </summary>
    class Transform : Component
    {
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.position = position;
        }

        /// <summary>
        /// Moves a gameobject
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector2 translation)
        {
            position += translation;
        }
    }
}
