using Microsoft.Xna.Framework;

namespace SecondSemesterExamProject
{
     interface IEnemyBuilder
    {
        void Build(Vector2 position, EnemyType type);

        GameObject GetResult();
    }
}