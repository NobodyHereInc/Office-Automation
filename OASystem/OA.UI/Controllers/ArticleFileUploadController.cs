using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OA.Model;
using OA.IService;

namespace OA.UI.Controllers
{
    public class ArticleFileUploadController : Controller
    {
        private IbookService booksService { get; set; }

        // GET: ArticleFileUpload
        public ActionResult Index()
        {
            return View();
        }

        #region Get All ArticleFile Info
        public ActionResult GetArticleFile()
        {
            // get page size and index.
            int pageSize = int.Parse(Request["rows"]);
            int pageIndex = int.Parse(Request["page"]);
            int totalCount = 0;

            // get all records from database.
            var bookList = booksService.GetPageList(b => b.Id != 0, b => b.Id, pageIndex, pageSize,out totalCount, true);

            // 
            var rows = from b in bookList
                      select new
                      {
                          ID = b.Id,
                          Title = b.Title,
                          Author = b.Author,
                          ISBN = b.ISBN,
                          PublishDate = b.PublishedDate
                      };

            // return Json Obj.
            return Json(new { rows = rows, total = totalCount });
        }
        #endregion

        #region Add Article File Upload Info
        public ActionResult AddArticleFileUploadInfo()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddArticleFileUploadInfo(book bookInfo)
        {
            //
            if (booksService.Add(bookInfo))
            {

            }
            

            return View();
        }
        #endregion

        #region Delete ArticleInfo
        public ActionResult DeleteArticleInfo()
        {
            // get delete ids String.
            String strId = Request.Form["strId"];

            // split strID string.
            String[] strIds = strId.Split(',');

            // calling deleteIds method to get int list for delete Id.
            List<int> deleteIds = GetDeleteId(strIds);

            // whether delete successfully.
            if (booksService.DeleteEntities(deleteIds))
            {
                return Content("Ok");
            }
            else // otherwise.
            {
                return Content("No");
            }
        }
        #endregion

        #region Edit Article File Upload Info
        public ActionResult EditArticleFileUploadInfo(int id)
        {
            // get book info by id.
            var bookInfo = booksService.GetList(b => b.Id == id).FirstOrDefault();

            ViewData.Model = bookInfo;

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditArticleFileUploadInfo(book NewBook)
        {
            booksService.Edit(NewBook);

            return Content("Yes");
        }
        #endregion

        #region Tool Functions
        /// <summary>
        /// This Function is used to convert string array to int list.
        /// </summary>
        /// <param name="strIds">input string array.</param>
        /// <returns>int list.</returns>
        public List<int> GetDeleteId(String[] strIds)
        {
            // resutl in list.
            List<int> result = new List<int>();

            // convert each string to int.
            foreach (String item in strIds)
            {
                result.Add(int.Parse(item));
            }

            // return  int list.
            return result;
        }
        #endregion
    }
}