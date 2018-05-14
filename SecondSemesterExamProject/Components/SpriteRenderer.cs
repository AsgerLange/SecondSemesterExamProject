﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    class SpriteRenderer : Component, IDrawable, ILoadable
    {
        private Rectangle rectangle;
        private Texture2D sprite;
        private string spriteName;
        private float layerDepth;
        private GameObject gameObject;
        private bool useRect = false;
        public bool UseRect { get { return useRect; } set { useRect = value; } }
        private Vector2 offset;

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }


        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
            this.gameObject = gameObject;

        }

        /// <summary>
        /// draws the sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (UseRect)
            {
                spriteBatch.Draw(sprite, gameObject.Transform.Position + offset, rectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
            }
            else
            {
                spriteBatch.Draw(sprite, gameObject.Transform.Position + offset, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, layerDepth);
            }
        }

        /// <summary>
        /// loads the sprite needed to be drawed
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>(spriteName);
        }
    }
}