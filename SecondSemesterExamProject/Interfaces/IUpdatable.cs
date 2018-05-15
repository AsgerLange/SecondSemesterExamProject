using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    /// <summary>
    /// Can be loaded
    /// </summary>
    interface IUpdatable
    {
        /// <summary>
        /// Handles Updates for the object component
        /// </summary>
        void Update();
    }
}
