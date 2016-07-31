using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.WorkFlow
{
    public class ResumeBookMarkObj<T>
    {
        public string BookMarkName { get; set; }
        public int StepId { get; set; }
        public T Result { get; set; }
    }
}
