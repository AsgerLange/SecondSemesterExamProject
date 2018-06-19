using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class SpriteRenderer : Component, IDrawable, ILoadable
    {
        private Rectangle rectangle;
        private Texture2D sprite;
        private string spriteName;
        private float layerDepth;
        private float scale = 1;
        private bool useRect = false;
        public bool UseRect { get { return useRect; } set { useRect = value; } }
        private Vector2 offset;
        private float rotation = 0;//in radians
        public Color color = Color.White;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = MathHelper.ToRadians(value); }//set with degrees
        }

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


        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth) : base(gameObject)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
        }

        /// <summary>
        /// draws the sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (UseRect)
            {
                Vector2 origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
                spriteBatch.Draw(sprite, GameObject.Transform.Position + offset, rectangle, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
            else
            {
                Vector2 origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
                spriteBatch.Draw(sprite, GameObject.Transform.Position + offset, null, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
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