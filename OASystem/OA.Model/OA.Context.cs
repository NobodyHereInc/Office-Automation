﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OA_DBEntities : DbContext
    {
        public OA_DBEntities()
            : base("name=OA_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActionInfo> ActionInfoes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfoes { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<KeyWordsRank> KeyWordsRanks { get; set; }
        public virtual DbSet<SearchDetail> SearchDetails { get; set; }
    }
}
