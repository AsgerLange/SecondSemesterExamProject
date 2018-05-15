using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// Handles what happens when 2 objects stop colliding
    /// </summary>
    interface ICollisionExit
    {
        void OnCollisionExit(Collider other);
    }
}
