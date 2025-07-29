using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EVABookShopAPI.DB.Migrations
{
    /// <inheritdoc />
    public partial class SeededCategoryAndBookData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "MasterSchema",
                table: "Categories",
                columns: new[] { "Id", "CatName", "CatOrder", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Fiction", 1, false },
                    { 2, "Non-Fiction", 2, false },
                    { 3, "Science Fiction", 3, false },
                    { 4, "Mystery", 4, false },
                    { 5, "Romance", 5, false },
                    { 6, "Biography", 6, false },
                    { 7, "History", 7, false },
                    { 8, "Fantasy", 8, false },
                    { 9, "Thriller", 9, false },
                    { 10, "Self-Help", 10, false },
                    { 11, "Business", 11, false },
                    { 12, "Technology", 12, false },
                    { 13, "Philosophy", 13, false },
                    { 14, "Poetry", 14, false },
                    { 15, "Travel", 15, false },
                    { 16, "Cooking", 16, false },
                    { 17, "Art", 17, false },
                    { 18, "Music", 18, false },
                    { 19, "Sports", 19, false },
                    { 20, "Children's Books", 20, false }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "BookPrice", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", 1, "A story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan.", 12.99m, "The Great Gatsby" },
                    { 2, "Harper Lee", 1, "The story of young Scout Finch and her father Atticus in a racially divided Alabama town.", 14.99m, "To Kill a Mockingbird" },
                    { 3, "George Orwell", 3, "A dystopian novel about totalitarianism and surveillance society.", 11.99m, "1984" },
                    { 4, "J.R.R. Tolkien", 8, "A fantasy novel about Bilbo Baggins' journey with thirteen dwarves.", 16.99m, "The Hobbit" },
                    { 5, "Arthur Conan Doyle", 4, "The first Sherlock Holmes novel introducing the famous detective.", 9.99m, "Sherlock Holmes: A Study in Scarlet" },
                    { 6, "Jane Austen", 5, "A romantic novel of manners that follows the emotional development of Elizabeth Bennet.", 13.99m, "Pride and Prejudice" },
                    { 7, "Walter Isaacson", 6, "The biography of Apple's visionary co-founder and CEO.", 19.99m, "Steve Jobs" },
                    { 8, "Sun Tzu", 2, "An ancient Chinese text on military strategy and tactics.", 8.99m, "The Art of War" },
                    { 9, "Dan Brown", 9, "A mystery thriller novel about a murder in the Louvre Museum and a religious mystery.", 15.99m, "The Da Vinci Code" },
                    { 10, "Eckhart Tolle", 10, "A guide to spiritual enlightenment and living in the present moment.", 17.99m, "The Power of Now" },
                    { 11, "Robert Kiyosaki", 11, "A personal finance book that advocates financial literacy and investment.", 21.99m, "Rich Dad Poor Dad" },
                    { 12, "Robert C. Martin", 12, "A handbook of agile software craftsmanship with practical examples.", 45.99m, "Clean Code" },
                    { 13, "Plato", 13, "A philosophical work that explores the nature of justice and the ideal society.", 18.99m, "The Republic" },
                    { 14, "T.S. Eliot", 14, "A landmark modernist poem that captures the disillusionment of post-World War I society.", 12.99m, "The Waste Land" },
                    { 15, "Elizabeth Gilbert", 15, "A memoir of a woman's journey of self-discovery through Italy, India, and Indonesia.", 16.99m, "Eat, Pray, Love" },
                    { 16, "Irma S. Rombauer", 16, "A comprehensive cookbook with recipes and cooking techniques for all skill levels.", 35.99m, "The Joy of Cooking" },
                    { 17, "E.H. Gombrich", 17, "A comprehensive survey of the history of art from prehistoric times to the modern era.", 29.99m, "The Story of Art" },
                    { 18, "Bob Spitz", 18, "A detailed biography of the most influential band in rock music history.", 24.99m, "The Beatles: The Biography" },
                    { 19, "Michael Lewis", 19, "The story of how the Oakland Athletics used statistical analysis to build a competitive baseball team.", 19.99m, "Moneyball" },
                    { 20, "Antoine de Saint-Exupéry", 20, "A poetic tale about a young prince who visits various planets in space.", 11.99m, "The Little Prince" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "MasterSchema",
                table: "Categories",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
