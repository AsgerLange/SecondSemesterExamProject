using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// Handles the moment 2 objects collide
    /// </summary>
    interface ICollisionEnter
    {
        void OnCollisionEnter(Collider other);
    }
}
