using OA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA.UI.Models
{
    public class IndexContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public LuceneEnumType luceneEnumType { get; set; }
    }
}