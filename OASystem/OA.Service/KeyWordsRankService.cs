using OA.Model;
using OA.IService;
using System;
using System.Collections.Generic;

namespace OA.Service
{
    public partial class KeyWordsRankService : BaseService<KeyWordsRank>, IKeyWordsRankService
    {
        /// <summary>
        /// This function is used to delete all data in table.
        /// </summary>
        /// <returns></returns>
        public bool DeleteKeyWords()
        {
            String sql = "TRUNCATE TABLE KeyWordsRank";

            return this.DbSession.ExecuteSql(sql) > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool InsertKeyWords()
        {
            //String sql = "INSERT INTO KeyWordsRank(id,KeyWords, SearchCount)" +
            //             "SELECT NEWID(), KeyWords, COUNT(*) " +
            //             "FROM SearchDetails " +
            //             "WHERE DATEDIFF(DAY, SearchDateTime, GETDATE()) <= 7 " +
            //             "GROUP BY KeyWords";
            string sql = "insert into KeyWordsRank(id,KeyWords, SearchCount) select newid(),KeyWords,count(*) from SearchDetails where DateDiff(day,SearchDateTime,getdate()) <=7 group by KeyWords";
            return this.DbSession.ExecuteSql(sql) > 0;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<String> GetSearchWord(String input)
        {
            String sql = "SELECT KeyWords From KeyWordsRank WHERE KeyWords LIKE @input";

            return this.DbSession.ExecuteQuery<String>(sql, new System.Data.SqlClient.SqlParameter("@input", input + "%"));
        }
    }
}
