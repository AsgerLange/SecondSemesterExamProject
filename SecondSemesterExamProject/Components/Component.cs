using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    abstract class Component
    {
        private GameObject gameObject;

        /// <summary>
        /// returns the gameobject that the component belongs to
        /// </summary>
        public GameObject GameObject
        {
            get { return gameObject; }
        }

        /// <summary>
        /// creates a new component with a gameobject
        /// </summary>
        /// <param name="gameObject"></param>
        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Component()
        {

        }
    }
}
