using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    /// <summary>
    /// Handles the moment 2 objects collide
    /// </summary>
    interface ICollisionEnter
    {
        void OnCollisionEnter(Collider other);
    }
}
