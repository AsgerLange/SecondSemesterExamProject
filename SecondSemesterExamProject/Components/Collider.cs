using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //public Rectangle CollisionBox
        //{
        //get{
        //}
        //}

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
            throw new NotImplementedException();
        }

        /// <summary>
        /// updates the collisionbox
        /// </summary>
        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
