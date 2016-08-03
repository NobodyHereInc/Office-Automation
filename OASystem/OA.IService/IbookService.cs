using System;
using System.Collections.Generic;
using OA.Model;

namespace OA.IService
{
    public partial interface IbookService : IBaseService<book>
    {
        /// <summary>
        /// Batch Delete Book info.
        /// </summary>
        /// <param name="DeleteList"></param>
        /// <returns></returns>
        bool DeleteEntities(List<int> DeleteList);
    }
}
