using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Animator : Component, IUpdatable
    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex;
        private float timeElapsed;
        private float fps;
        private Rectangle[] rectangles;
        string animationName;
        private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();

        public Dictionary<string, Animation> Animations
        {
            get { return animations; }
            set { animations = value; }
        }

        public Animator(GameObject gameObject) : base(gameObject)
        {
            fps = 5;

            this.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
        }

        public void Update()
        {
            timeElapsed += GameWorld.Instance.DeltaTime;
            currentIndex = (int)(timeElapsed * fps);

            if (currentIndex > rectangles.Length - 1)
            {
                GameObject.OnAnimationDone(animationName);

                timeElapsed = 0;

                currentIndex = 0;
            }
            spriteRenderer.Rectangle = rectangles[currentIndex];
        }

        public void CreateAnimation(string animationName, Animation animation)
        {
            animations.Add(animationName, animation);
        }

        public void PlayAnimation(string animationName)
        {
            if (this.animationName != animationName)
            {
                //sets the rectangles
                this.rectangles = animations[animationName].Rectangles;

                //resets the rectangle
                //this.spriteRenderer.Rectangle = rectangles[0];

                //sets the offset
                this.spriteRenderer.Offset = animations[animationName].Offset;

                //sets the animation name
                this.animationName = animationName;

                //sets fps
                this.fps = animations[animationName].FPS;

                //resets the animation
                timeElapsed = 0;
                currentIndex = 0;

            }
        }
    }
}