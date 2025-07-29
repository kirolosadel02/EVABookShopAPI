﻿using EVABookShopAPI.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVABookShopAPI.DB.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "MasterSchema");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CatName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(c => c.CatOrder)
                .IsRequired();
            // Configure the relationship with Book
            builder.Ignore(c => c.CreatedDate);
            builder.Property(c => c.MarkedAsDeleted).HasColumnName("isDeleted");

            builder.HasIndex(c => c.CatName)
                .IsUnique()
                .HasDatabaseName("IX_Categories_CatName");
        }
    }
}
