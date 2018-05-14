using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    class GameObject : Component
    {
        private List<Component> components = new List<Component>();

        private bool isLoaded;
        private Transform transform;
        
        public Transform Transform
        {
            get { return transform; }
        }

        /// <summary>
        /// Gameobject initialization
        /// </summary>
        public GameObject()
        {
            this.transform = new Transform(this, Vector2.Zero);
            AddComponent(transform);
            isLoaded = false;
        }

        /// <summary>
        /// adds a component
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        /// <summary>
        /// returns the asked for component
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public Component GetComponent(string component)
        {
            foreach (Component comp in components)
            {
                if (comp.GetType().ToString() == "Beardman." + component)
                {
                    return comp;
                }
            }
            //return specefik component 
            return null;
        }

        /// <summary>
        /// Handles loading of content foreach loadable compenent
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            if (!(isLoaded))
            {
                foreach (var component in components)
                {
                    if (component is ILoadable)
                    {
                        (component as ILoadable).LoadContent(content);
                    }
                }
                isLoaded = true;
            }
        }

        /// <summary>
        /// Handles Updates foreach Updateble component
        /// </summary>
        public void Update()
        {
            foreach (var component in components)
            {
                if (component is IUpdatable)
                {
                    (component as IUpdatable).Update();
                }
            }
        }

        /// <summary>
        /// Handles Drawing foreach drawable component
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in components)
            {
                if (component is IDrawable)
                {
                    (component as IDrawable).Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Handles on animation done foreach animated components
        /// </summary>
        /// <param name="animationName"></param>
        public void OnAnimationDone(string animationName)
        {
            foreach (Component component in components)
            {
                if (component is IAnimatable)
                {
                    (component as IAnimatable).OnAnimationDone(animationName);
                }
            }
        }
    }
}
