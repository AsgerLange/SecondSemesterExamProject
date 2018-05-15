using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TankGame
{
    enum BuilderType { BulletBuilder, EnemyBuilder }
    class GameObjectDirector
    {
        private static GameObjectDirector instance;

        /// <summary>
        /// Get Property to the GameObjectDirector's Singleton instance
        /// </summary>
        public static GameObjectDirector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObjectDirector();

                }

                return instance;
            }
        }
            


        private GameObjectDirector()
        {

        }


        public GameObject Construct(Vector2 position,BuilderType type)
        {
            //Call Build on the correct Ibuilder, based on Buildertype enum

            return null; //Gameobject
        }
    }
}
