using System;
using Quartz;
using OA.IService;
using OA.Service;

namespace OA.Component.Quartz
{
    public class IndexJob : IJob
    {
        IKeyWordsRankService service = new KeyWordsRankService();


        /// <summary>
        /// This function is used to set job for quartz.
        /// </summary>
        /// <param name="context"></param>
        public void Execute(JobExecutionContext context)
        {
            // delete all data.
            service.DeleteKeyWords();
            // insert new data.
            service.InsertKeyWords();
        }
    }
}