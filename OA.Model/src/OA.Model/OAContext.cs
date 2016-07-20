using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OA.Model
{
    public partial class OAContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=OA_DB;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=false;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActionInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionInfoName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.ActionMethodName).HasMaxLength(32);

                entity.Property(e => e.ControllerName).HasMaxLength(128);

                entity.Property(e => e.HttpMethod)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.MenuIcon).HasColumnType("varchar(512)");

                entity.Property(e => e.ModifiedOn).IsRequired();

                entity.Property(e => e.Remark).HasMaxLength(256);

                entity.Property(e => e.SubTime).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(512);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DepName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.TreePath)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<DepartmentActionInfo>(entity =>
            {
                entity.HasKey(e => new { e.ActionInfoId, e.DepartmentId })
                    .HasName("PK_DepartmentActionInfo");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("IX_FK_DepartmentActionInfo_Department");

                entity.Property(e => e.ActionInfoId).HasColumnName("ActionInfo_ID");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

                entity.HasOne(d => d.ActionInfo)
                    .WithMany(p => p.DepartmentActionInfo)
                    .HasForeignKey(d => d.ActionInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DepartmentActionInfo_ActionInfo");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentActionInfo)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_DepartmentActionInfo_Department");
            });

            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<RUserInfoActionInfo>(entity =>
            {
                entity.ToTable("R_UserInfo_ActionInfo");

                entity.HasIndex(e => e.ActionInfoId)
                    .HasName("IX_FK_ActionInfoR_UserInfo_ActionInfo");

                entity.HasIndex(e => e.UserInfoId)
                    .HasName("IX_FK_UserInfoR_UserInfo_ActionInfo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionInfoId).HasColumnName("ActionInfoID");

                entity.Property(e => e.UserInfoId).HasColumnName("UserInfoID");

                entity.HasOne(d => d.ActionInfo)
                    .WithMany(p => p.RUserInfoActionInfo)
                    .HasForeignKey(d => d.ActionInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ActionInfoR_UserInfo_ActionInfo");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.RUserInfoActionInfo)
                    .HasForeignKey(d => d.UserInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserInfoR_UserInfo_ActionInfo");
            });

            modelBuilder.Entity<RoleInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(256);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.SubTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<RoleInfoActionInfo>(entity =>
            {
                entity.HasKey(e => new { e.ActionInfoId, e.RoleInfoId })
                    .HasName("PK_RoleInfoActionInfo");

                entity.HasIndex(e => e.RoleInfoId)
                    .HasName("IX_FK_RoleInfoActionInfo_RoleInfo");

                entity.Property(e => e.ActionInfoId).HasColumnName("ActionInfo_ID");

                entity.Property(e => e.RoleInfoId).HasColumnName("RoleInfo_ID");

                entity.HasOne(d => d.ActionInfo)
                    .WithMany(p => p.RoleInfoActionInfo)
                    .HasForeignKey(d => d.ActionInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleInfoActionInfo_ActionInfo");

                entity.HasOne(d => d.RoleInfo)
                    .WithMany(p => p.RoleInfoActionInfo)
                    .HasForeignKey(d => d.RoleInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RoleInfoActionInfo_RoleInfo");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(256);

                entity.Property(e => e.SubTime).HasColumnType("datetime");

                entity.Property(e => e.Uname)
                    .HasColumnName("UName")
                    .HasMaxLength(32);

                entity.Property(e => e.Upwd)
                    .IsRequired()
                    .HasColumnName("UPwd")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<UserInfoDepartment>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentId, e.UserInfoId })
                    .HasName("PK_UserInfoDepartment");

                entity.HasIndex(e => e.UserInfoId)
                    .HasName("IX_FK_UserInfoDepartment_UserInfo");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");

                entity.Property(e => e.UserInfoId).HasColumnName("UserInfo_ID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.UserInfoDepartment)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserInfoDepartment_Department");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.UserInfoDepartment)
                    .HasForeignKey(d => d.UserInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserInfoDepartment_UserInfo");
            });

            modelBuilder.Entity<UserInfoRoleInfo>(entity =>
            {
                entity.HasKey(e => new { e.RoleInfoId, e.UserInfoId })
                    .HasName("PK_UserInfoRoleInfo");

                entity.HasIndex(e => e.UserInfoId)
                    .HasName("IX_FK_UserInfoRoleInfo_UserInfo");

                entity.Property(e => e.RoleInfoId).HasColumnName("RoleInfo_ID");

                entity.Property(e => e.UserInfoId).HasColumnName("UserInfo_ID");

                entity.HasOne(d => d.RoleInfo)
                    .WithMany(p => p.UserInfoRoleInfo)
                    .HasForeignKey(d => d.RoleInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserInfoRoleInfo_RoleInfo");

                entity.HasOne(d => d.UserInfo)
                    .WithMany(p => p.UserInfoRoleInfo)
                    .HasForeignKey(d => d.UserInfoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UserInfoRoleInfo_UserInfo");
            });
        }

        public virtual DbSet<ActionInfo> ActionInfo { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentActionInfo> DepartmentActionInfo { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<RUserInfoActionInfo> RUserInfoActionInfo { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<RoleInfoActionInfo> RoleInfoActionInfo { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserInfoDepartment> UserInfoDepartment { get; set; }
        public virtual DbSet<UserInfoRoleInfo> UserInfoRoleInfo { get; set; }
    }
}