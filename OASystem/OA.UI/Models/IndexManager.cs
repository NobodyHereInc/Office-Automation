using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using OA.Model.Enum;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace OA.UI.Models
{
    public sealed class IndexManager
    {
        private static readonly IndexManager indexManager = new IndexManager();

        private IndexManager()
        {
        }
        public static IndexManager GetInstance()
        {
            return indexManager;
        }

        Queue<IndexContent> queue = new Queue<IndexContent>();

        /// <summary>
        /// This function is used to add new book info into database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void AddQueue(int id, string title, string content)
        {
            IndexContent newContent = new IndexContent();

            newContent.Id = id;
            newContent.Title = title;
            newContent.Content = content;
            newContent.luceneEnumType = LuceneEnumType.Add;

            // 
            queue.Enqueue(newContent);
        }




        /// <summary>
        /// This function is used to delete book info into database.
        /// </summary>
        /// <param name="id"></param>
        public void DeletQueue(int id)
        {
            IndexContent newContent = new IndexContent();

            newContent.Id = id;
            newContent.luceneEnumType = LuceneEnumType.Delete;

            //
            queue.Enqueue(newContent);
        }




        /// <summary>
        /// This function is used to create thread.
        /// </summary>
        public void CreateThread()
        {
            Thread myThread = new Thread(CreateSearchIndex);
            myThread.IsBackground = true;
            myThread.Start();
        }




        /// <summary>
        /// 
        /// </summary>
        private void CreateSearchIndex()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    try
                    {
                        WriteSearchContent();
                    }
                    catch (System.Exception)
                    {

                    }
                    
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }





        /// <summary>
        /// 
        /// </summary>
        private void WriteSearchContent()
        {
            string indexPath = @"D:\lucenedir";
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isUpdate = IndexReader.IndexExists(directory);
            if (isUpdate)
            {
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            IndexWriter writer = new IndexWriter(directory, new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);

            while (queue.Count > 0)
            {
                IndexContent indexContent = queue.Dequeue();

                writer.DeleteDocuments(new Term("id", indexContent.Id.ToString()));

                if (indexContent.luceneEnumType == LuceneEnumType.Delete)
                {
                    continue;
                }

                Document document = new Document();

                document.Add(new Field("id", indexContent.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("title", indexContent.Title, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                document.Add(new Field("Content", indexContent.Content, Field.Store.YES, Field.Index.ANALYZED, Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));
                writer.AddDocument(document);
            }

            writer.Close();
            directory.Close();
        }
    }
}