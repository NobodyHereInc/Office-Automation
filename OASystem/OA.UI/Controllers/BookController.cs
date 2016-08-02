using System.Linq;
using System.Web.Mvc;
using OA.Model;
using OA.IService;

namespace OA.UI.Controllers
{
    public class BookController : Controller
    {
        private IbookService booksService { get; set; }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        #region Show Book Detail
        public ActionResult ShowBookDetail(int id)
        {
            // get book'id
            int bookId = id;

            book bookInfo = booksService.GetList(b => b.Id == bookId).FirstOrDefault();

            ViewBag.BookInfo = bookInfo;

            return View();
        }
        #endregion
    }
}