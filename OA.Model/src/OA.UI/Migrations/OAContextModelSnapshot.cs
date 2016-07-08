using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OA.DAL;

namespace OA.UI.Migrations
{
    [DbContext(typeof(OAContext))]
    partial class OAContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OA.Model.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("DelFlag");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Remark");

                    b.Property<string>("Sort");

                    b.Property<DateTime>("SubTime");

                    b.Property<string>("UPwd");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("UserInfo");
                });
        }
    }
}
