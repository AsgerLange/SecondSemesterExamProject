using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    interface ITowerBuilder
    {
        void Build(Vector2 position, TowerType type);

        GameObject GetResult();
    }
}
