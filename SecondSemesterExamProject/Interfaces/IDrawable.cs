using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    /// <summary>
    /// Component can be drawn
    /// </summary>
    interface IDrawable
    {
        /// <summary>
        /// draws object
        /// </summary>
        /// <param name="spriteBatch"></param>
        void Draw(SpriteBatch spriteBatch);
    }
}
