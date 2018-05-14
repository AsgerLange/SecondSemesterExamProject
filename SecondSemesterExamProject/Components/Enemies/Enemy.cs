using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    enum EnemyType {BasicEnemy };
    class Enemy : Component
    {
        private bool canRelease;

        public bool CanRelease
        {
            get { return canRelease; }
            set { canRelease = value; }
        }
    }
}
