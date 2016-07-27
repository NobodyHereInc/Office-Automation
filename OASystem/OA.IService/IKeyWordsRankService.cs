using OA.Model;
using System;
using System.Collections.Generic;

namespace OA.IService
{
    public partial interface IKeyWordsRankService : IBaseService<KeyWordsRank>
    {
        /// <summary>
        /// Delete all data in KeyWordsRank table.
        /// </summary>
        /// <returns></returns>
        bool DeleteKeyWords();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool InsertKeyWords();

        /// <summary>
        /// This function is used to get hot word.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<String> GetSearchWord(String input);
    }
}
