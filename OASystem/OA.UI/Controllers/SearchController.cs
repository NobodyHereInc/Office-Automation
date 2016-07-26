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
using System.Linq;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Analysis.Standard;

namespace OA.UI.Controllers
{
    public class SearchController : Controller
    {
        private IbookService bookService { get; set; }

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
                viewModel.Content = Common.WebCommon.CreateHightLight(kw, doc.Get("Content"));
                searchResultList.Add(viewModel);
            }

            //SearchDetails searchDetail = new SearchDetails();
            //searchDetail.KeyWords = kw;
            //searchDetail.Id = Guid.NewGuid();
            //searchDetail.SearchDateTime = DateTime.Now;
            //searchDetailService.AddEntity(searchDetail);
            return searchResultList;
        }
        #endregion
    }
}