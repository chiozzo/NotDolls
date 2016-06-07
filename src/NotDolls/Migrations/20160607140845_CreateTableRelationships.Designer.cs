using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NotDolls.Models;

namespace NotDolls.Migrations
{
    [DbContext(typeof(NotDollsContext))]
    [Migration("20160607140845_CreateTableRelationships")]
    partial class CreateTableRelationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NotDolls.Models.Geek", b =>
                {
                    b.Property<int>("GeekId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("Location");

                    b.Property<string>("UserName");

                    b.HasKey("GeekId");

                    b.ToTable("Geek");
                });

            modelBuilder.Entity("NotDolls.Models.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<int>("GeekId");

                    b.Property<string>("Height");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<string>("Quality");

                    b.Property<int>("Quantity");

                    b.Property<bool>("Sold");

                    b.Property<string>("Weight");

                    b.Property<int>("Year");

                    b.HasKey("InventoryId");

                    b.HasIndex("GeekId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("NotDolls.Models.Inventory_Image", b =>
                {
                    b.Property<int>("Inventory_ImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Image");

                    b.Property<int>("InventoryId");

                    b.HasKey("Inventory_ImageId");

                    b.HasIndex("InventoryId");

                    b.ToTable("Inventory_Image");
                });

            modelBuilder.Entity("NotDolls.Models.Inventory", b =>
                {
                    b.HasOne("NotDolls.Models.Geek")
                        .WithMany()
                        .HasForeignKey("GeekId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NotDolls.Models.Inventory_Image", b =>
                {
                    b.HasOne("NotDolls.Models.Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
