using EVABookShopAPI.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVABookShopAPI.DB.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books", "MasterSchema");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(b => b.Description)
                .IsRequired(false)
                .HasMaxLength(250);
            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(b => b.Price)
                .IsRequired()
                .HasColumnName("BookPrice")
                .HasColumnType("decimal(18,2)");

            builder.ToTable("Books", t =>
            {
                t.HasCheckConstraint("CK_Books_Price_Range", "[BookPrice] >= 1 AND [BookPrice] <= 1000");
            });

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
