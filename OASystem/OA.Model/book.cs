//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Nullable<int> PublisherId { get; set; }
        public string PublishedDate { get; set; }
        public string ISBN { get; set; }
        public Nullable<int> WordsCount { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string ContentDescription { get; set; }
        public string AuthorDescription { get; set; }
        public string EditorComment { get; set; }
        public string TOC { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> Clicks { get; set; }
    }
}