using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Model.Enum
{
    public enum DeleteEnumType
    {
        /// <summary>
        /// Normal User
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Logic Delete User
        /// </summary>
        LogicDelete = 1,
        /// <summary>
        /// Physic Delete User
        /// </summary>
        PhysicDelete = 2
    }
}
