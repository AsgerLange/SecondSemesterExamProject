using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    /// <summary>
    /// Handles What happens during a continuous collision
    /// </summary>
    interface ICollisionStay
    {
        void OnCollisionStay(Collider other);
    }
}
