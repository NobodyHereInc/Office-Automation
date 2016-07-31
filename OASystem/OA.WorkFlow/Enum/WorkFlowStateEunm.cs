using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.WorkFlow.Eeum
{
    public enum WorkFlowStateEunm
    {
        /// <summary>
        /// Allow.
        /// </summary>
        IsPass = 1,
        /// <summary>
        /// Not Allow.
        /// </summary>
        Reject = 2,
        /// <summary>
        /// Final not Allow.
        /// </summary>
        Refute = 3,
        /// <summary>
        /// Next step.
        /// </summary>
        Continue = 4,
        /// <summary>
        /// 
        /// </summary>
        untreated = 5
    }
}
