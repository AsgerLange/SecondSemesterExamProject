using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    class Collider : Component, IDrawable, ILoadable, IUpdatable
    {
        public static readonly object alignmentKey = new object();
        public static readonly object removeOtherCollidersKey = new object();
        public static readonly object doCollsionChecksKey = new object();
        private Texture2D texture;
        private SpriteRenderer spriteRenderer;
        private bool doCollisionChecks;
        private List<Collider> otherColliders = new List<Collider>();
        private List<Collider> removeOtherColliders = new List<Collider>();
        private Alignment alignment;

        public Alignment GetAlignment
        {
            get
            {
                lock (alignmentKey)
                {
                    return alignment;
                }
            }
            set
            {
                lock (alignmentKey)
                {
                    alignment = value;
                }
            }
        }

        public List<Collider> RemoveOtherColliders
        {
            get
            {
                lock (removeOtherCollidersKey)
                {
                    return removeOtherColliders;
                }
            }
            set
            {
                lock (removeOtherCollidersKey)
                {
                    removeOtherColliders = value;
                }
            }
        }

        public bool DoCollsionChecks
        {
            get
            {
                lock (doCollsionChecksKey)
                {
                    return doCollisionChecks;
                }
            }
            set
            {
                lock (doCollsionChecksKey)
                {
                    doCollisionChecks = value;
                }
            }
        }

        public Collider(GameObject gameObject, Alignment alignment) : base(gameObject)
        {
            this.alignment = alignment;

            doCollisionChecks = true;
            lock (GameWorld.colliderKey)
            {
                GameWorld.Instance.Colliders.Add(this);
            }
        }

        /// <summary> 
        /// Creates a collisionbox 
        /// </summary> 
        public Circle CollisionBox
        {
            get
            {
                if (spriteRenderer != null)
                {
                    if (spriteRenderer.UseRect)
                    {
                        return new Circle
                        (new Vector2(
                            GameObject.Transform.Position.X,
                            GameObject.Transform.Position.Y),
                            (spriteRenderer.Rectangle.Width / 2) * spriteRenderer.Scale);
                    }
                    else
                    {
                        return new Circle
                        (new Vector2(
                            GameObject.Transform.Position.X,
                            GameObject.Transform.Position.Y),
                            (spriteRenderer.Sprite.Width / 2) * spriteRenderer.Scale);
                    }
                }
                else//failure fallback circle if spriterenderer is null to awoid crash in some situations
                {   //new collisionbox will be given next check
                    return new Circle(new Vector2(-1000, -1000), 1);
                }
            }
        }

        /// <summary>
        /// draws the collisionbox in debugmode
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            spriteBatch.Draw(texture, new Rectangle(CollisionBox.Bounds.X - (CollisionBox.Bounds.Width / 2) + (int)spriteRenderer.Offset.X, CollisionBox.Bounds.Y - (CollisionBox.Bounds.Height / 2) + (int)spriteRenderer.Offset.Y, CollisionBox.Bounds.Width, CollisionBox.Bounds.Height), Color.Red);
#endif
        }

        /// <summary>
        /// Loads whats needed for the collisionboxes
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");

            texture = content.Load<Texture2D>("CollisionTexture");
        }

        /// <summary>
        /// updates the collisionbox
        /// </summary>
        public void Update()
        {
            CheckCollision();
        }

        /// <summary>
        /// checks if there is a collision between colliders
        /// </summary>
        private void CheckCollision()
        {
            if (doCollisionChecks)
            {
                lock (GameWorld.colliderKey)
                {
                    foreach (Collider other in GameWorld.Instance.Colliders)
                    {
                        if (other != this)
                        {
                            if (CollisionBox.Intersects(other.CollisionBox))
                            {
                                if (!(otherColliders.Contains(other)))
                                {
                                    GameObject.OnCollisionEnter(other);
                                    otherColliders.Add(other);
                                }
                            }
                        }
                    }
                }
                if (otherColliders.Count > 0)
                {
                    foreach (Collider other in otherColliders)
                    {
                        if (other != this)
                        {
                            if (CollisionBox.Intersects(other.CollisionBox))
                            {
                                GameObject.OnCollisionStay(other);
                            }
                            else
                            {
                                GameObject.OnCollisionExit(other);
                                removeOtherColliders.Add(other);
                            }
                        }
                    }
                }

            }
            RemoveCollider();
        }

        /// <summary>
        /// Removes collides
        /// </summary>
        private void RemoveCollider()
        {
            if (removeOtherColliders.Count > 0)
            {
                foreach (Collider other in removeOtherColliders)
                {
                    otherColliders.Remove(other);
                }
                removeOtherColliders.Clear();
            }
        }
    }
}
