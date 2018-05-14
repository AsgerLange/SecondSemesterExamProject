using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// component can be animated
    /// </summary>
    interface IAnimatable
    {
        /// <summary>
        /// when finished with an animation
        /// </summary>
        /// <param name="animationName"></param>
        void OnAnimationDone(string animationName);
    }
}
