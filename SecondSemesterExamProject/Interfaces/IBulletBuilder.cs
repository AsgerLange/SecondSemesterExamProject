using Microsoft.Xna.Framework;

namespace TankGame
{
    interface IBulletBuilder
    {

        void Build(Vector2 position, BulletType type, float rotation, Alignment alignment);

        GameObject GetResult();


    }
}