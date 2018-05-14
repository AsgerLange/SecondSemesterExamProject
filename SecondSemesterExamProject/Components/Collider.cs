using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SecondSemesterExamProject
{
    class Collider : Component, IDrawable, ILoadable, IUpdatable
    {
        private Texture2D texture;
        private SpriteRenderer spriteRenderer;
        private bool doCollisionChecks;
        private List<Collider> otherColliders = new List<Collider>();
        private List<Collider> removeOtherColliders = new List<Collider>();

        public List<Collider> RemoveOtherColliders
        {
            get { return removeOtherColliders; }
            set { removeOtherColliders = value; }
        }

        public bool DoCollsionChecks
        {
            get { return doCollisionChecks; }
            set { doCollisionChecks = value; }
        }

        public Collider(GameObject gameObject) : base(gameObject)
        {
            doCollisionChecks = true;
            GameWorld.Instance.Colliders.Add(this);
        }

        /// <summary>
        /// Creates a collisionbox
        /// </summary>
        public Rectangle CollisionBox
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// draws the collisionbox in debugmode
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            throw new NotImplementedException();
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

        /// <summary>
        /// empties all list for referances to a collider
        /// </summary>
        public void EmptyLists()
        {
            removeOtherColliders.Clear();
            otherColliders.Clear();
        }
    }
}
