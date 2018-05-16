using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Animation
    {
        private float fps;
        private Vector2 offSet;
        private Rectangle[] rectangles;

        public float FPS
        {
            get { return fps; }
        }

        public Vector2 Offset
        {
            get { return offSet; }
            set { offSet = value; }
        }

        public float Fps
        { get { return fps; } }

        public Rectangle[] Rectangles
        {
            get { return rectangles; }
        }

        /// <summary>
        /// Creates a new Animation
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="yPos"></param>
        /// <param name="xStartFrame"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fps"></param>
        /// <param name="offSet"></param>
        public Animation(int frames, int yPos, int xStartFrame, int width, int height, float fps, Vector2 offSet)
        {
            rectangles = new Rectangle[frames];
            this.offSet = offSet;
            this.fps = fps;
            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }
        }
    }
}