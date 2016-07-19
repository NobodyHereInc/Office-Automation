using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WorkflowConsoleApplication1
{

    public sealed class MoneyCodeActivity : CodeActivity
    {
        // Define an activity input argument of type string
        // public InArgument<string> Text { get; set; }

            public OutArgument<int> Money { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            // string text = context.GetValue(this.Text);

            int money;
            String m = Console.ReadLine();
            int.TryParse(m, out money);

            context.SetValue(Money, money);
        }
    }
}
