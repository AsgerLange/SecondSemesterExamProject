using Microsoft.Xna.Framework;

namespace TankGame
{
     interface IEnemyBuilder
    {
        void Build(Vector2 position, EnemyType type);

        GameObject GetResult();
    }
}