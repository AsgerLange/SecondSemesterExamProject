using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondSemesterExamProject
{
    /// <summary>
    /// Has some content that needs to be loaded
    /// </summary>
    interface ILoadable
    {
        /// <summary>
        /// Loads Some Content
        /// </summary>
        /// <param name="content"></param>
        void LoadContent(ContentManager content);
    }
}
