using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace OA.WorkFlow
{
    /// <summary>
    /// BookMark.
    /// </summary>
    public sealed class Input4WaitBookMark<T> : NativeActivity //CodeActivity
    {
        // Define an activity input argument of type string
        public InOutArgument<string> BookMark { get; set; }
        public OutArgument<int> StepId { get; set; }
        public OutArgument<T> Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context) 
        {
            String bookMarkName = context.GetValue(BookMark);
            context.CreateBookmark(bookMarkName, ContinueExecuteWorkFlow);
        }

        private void ContinueExecuteWorkFlow(NativeActivityContext context, Bookmark bookmark, object value)
        {
            var data = (ResumeBookMarkObj<T>)value;
            context.SetValue(BookMark, data.BookMarkName);
            context.SetValue(StepId, data.StepId);
            context.SetValue(Result, data.Result);
        }
    }
}
