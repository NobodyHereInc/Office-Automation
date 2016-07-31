using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;

namespace OA.WorkFlow
{
    public class WorkFlowApplicationHelper
    {
        // use to wake  up main process. 
        static AutoResetEvent syncEvent = new AutoResetEvent(false);

        /// <summary>
        /// This function is used to create WorkFlow Application.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static WorkflowApplication CreateWorkflowApplication(Activity activity, IDictionary<string, object> dict)
        {
            // instance a WorkflowApplication obj to set properties of workflow.
            WorkflowApplication appliction = new WorkflowApplication(activity, dict);

            // instace a SqlWorkflowInstanceStore to store Workflow Instance into database.
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(ConfigurationManager.AppSettings["workflowConnection"]);

            appliction.InstanceStore = store;

            // each event.
            appliction.Unloaded += OnUloaded;
            appliction.Aborted += OnAborted;
            appliction.Completed += OnCompleted;
            appliction.Idle += OnIdle;
            appliction.PersistableIdle += OnPersistableIdle;
            appliction.OnUnhandledException += OnUnhandledException;

            // start WorkFlow.
            appliction.Run();

            // return.
            return appliction;
        }




        /// <summary>
        /// This function is used to reload Workflow from database.
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static WorkflowApplication LoadWorkflowApplication(Activity activity, Guid guid)
        {
            // instance a WorkflowApplication obj to set properties of workflow.
            WorkflowApplication appliction = new WorkflowApplication(activity);

            // instace a SqlWorkflowInstanceStore to store Workflow Instance into database.
            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(ConfigurationManager.AppSettings["workflowConnection"]);

            //workflow store into database.
            appliction.InstanceStore = store;

            // each event.
            appliction.Unloaded += OnUloaded;
            appliction.Aborted += OnAborted;
            appliction.Completed += OnCompleted;
            appliction.Idle += OnIdle;
            appliction.PersistableIdle += OnPersistableIdle;
            appliction.OnUnhandledException += OnUnhandledException;

            // reload workFlow application by guid.
            appliction.Load(guid);

            // return.
            return appliction;
        }





        private static UnhandledExceptionAction OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("Exception!!");
            syncEvent.Set();
            return UnhandledExceptionAction.Abort;
        }

        private static PersistableIdleAction OnPersistableIdle(WorkflowApplicationIdleEventArgs arg)
        {
            Console.WriteLine("Persistable");
            return PersistableIdleAction.Unload;
        }

        private static void OnIdle(WorkflowApplicationIdleEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("WorkFlow is Idle!!");
        }

        private static void OnCompleted(WorkflowApplicationCompletedEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("WorkFlow is completed!!");
        }

        private static void OnAborted(WorkflowApplicationAbortedEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("WorkFlow is Aborted!!");
        }

        private static void OnUloaded(WorkflowApplicationEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("WorkFlow is Uloaded");
        }


    }
}
