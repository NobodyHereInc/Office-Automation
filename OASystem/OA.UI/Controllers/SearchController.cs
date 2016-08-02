using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Web.Mvc;
using OA.Model;
using OA.IService;
using System.IO;
using OA.UI.Models;
using System.Collections.Generic;

namespace OA.UI.Controllers
{
    public class SearchController : Controller
    {
        private IbookService bookService { get; set; }
        private ISearchDetailService searchDetailService { get; set; }
        private IKeyWordsRankService keyWordsRankService { get; set; }

        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        #region SearchContent
        public ActionResult SearchContent()
        {
            if (!String.IsNullOrEmpty(Request["btnSearch"]))
            {
                ViewData["list"] = SearchBookContent();
                return View("index");
            }
            else
            {
                createContent();
            }

            return Content("ox");
        }

        private void createContent()
        {

        }

        private List<SearchResultViewModel> SearchBookContent()
        {
            string indexPath = @"D:\lucenedir";
            string kw = Request["txtSearchContent"];
            string[] keywords = Common.WebCommon.SplitWords(kw);
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);

            PhraseQuery query = new PhraseQuery();

            foreach (string word in keywords)
            {
                query.Add(new Term("Content", word));
            }

            query.SetSlop(100);

            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            searcher.Search(query, null, collector);
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;

            List<SearchResultViewModel> searchResultList = new List<SearchResultViewModel>();
            for (int i = 0; i < docs.Length; i++)
            {
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                SearchResultViewModel viewModel = new SearchResultViewModel();
                viewModel.Id = int.Parse(doc.Get("id"));
                viewModel.Title = doc.Get("title");
                viewModel.Url = "/Book/ShowBookDetail/?id=" + viewModel.Id;
                viewModel.Content = Common.WebCommon.CreateHightLight(kw, doc.Get("Content"));
                searchResultList.Add(viewModel);
            }

            // save search Info into database. (Hot Words)
            SearchDetail searchDetail = new SearchDetail();
            searchDetail.id = Guid.NewGuid();
            searchDetail.KeyWords = kw;
            searchDetail.SearchDateTime = DateTime.Now;
            searchDetailService.Add(searchDetail);

            return searchResultList;
        }
        #endregion


        #region Get Hot Word
        public ActionResult GetSarch()
        {
            // get user input.
            String term = Request["term"];
            // search in database to get match hot word.
            List<String> list = keyWordsRankService.GetSearchWord(term);
            // return result.
            return Json(list.ToArray(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}