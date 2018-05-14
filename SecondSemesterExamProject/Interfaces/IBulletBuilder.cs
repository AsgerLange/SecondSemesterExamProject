using Microsoft.Xna.Framework;

namespace SecondSemesterExamProject
{
    interface IBulletBuilder
    {

        void Build(Vector2 position, BulletType type);

        GameObject GetResult();


    }
}