using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    enum BulletType {BaiscBullet };
    class Bullet : Component
    {
        private bool canRelease;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
    }
}
