﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// Makes it posible for the object to move
    /// </summary>
    class Transform : Component
    {
        private Vector2 position;
        public bool canMove = true;
        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }
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
            if (canMove)
            {
                Component comp = null;
                foreach (Component bul in GameObject.GetComponentList)
                {
                    if (bul is Bullet)
                    {
                        comp = bul;
                        break;
                    }
                }
                if (position.Y <= 0 && translation.Y <= 0 && comp == null)
                {
                    translation.Y = 0;
                }
                if (position.X <= 0 && translation.X <= 0 && comp == null)
                {
                    translation.X = 0;
                }
                if (position.Y >= Constant.hight && translation.Y >= 0 && comp == null)
                {
                    translation.Y = 0;
                }
                if (position.X >= Constant.width && translation.X >= 0 && comp == null)
                {
                    translation.X = 0;
                }
                position += translation;
            }
        }
    }
}
