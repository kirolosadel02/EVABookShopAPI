using EVABookShopAPI.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EVABookShopAPI.DB
{
    public static class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedCategories(modelBuilder);
            SeedBooks(modelBuilder);
        }

        private static void SeedCategories(ModelBuilder modelBuilder)
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    CatName = "Fiction",
                    CatOrder = 1,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 2,
                    CatName = "Non-Fiction",
                    CatOrder = 2,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 3,
                    CatName = "Science Fiction",
                    CatOrder = 3,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 4,
                    CatName = "Mystery",
                    CatOrder = 4,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 5,
                    CatName = "Romance",
                    CatOrder = 5,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 6,
                    CatName = "Biography",
                    CatOrder = 6,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 7,
                    CatName = "History",
                    CatOrder = 7,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 8,
                    CatName = "Fantasy",
                    CatOrder = 8,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 9,
                    CatName = "Thriller",
                    CatOrder = 9,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 10,
                    CatName = "Self-Help",
                    CatOrder = 10,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 11,
                    CatName = "Business",
                    CatOrder = 11,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 12,
                    CatName = "Technology",
                    CatOrder = 12,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 13,
                    CatName = "Philosophy",
                    CatOrder = 13,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 14,
                    CatName = "Poetry",
                    CatOrder = 14,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 15,
                    CatName = "Travel",
                    CatOrder = 15,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 16,
                    CatName = "Cooking",
                    CatOrder = 16,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 17,
                    CatName = "Art",
                    CatOrder = 17,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 18,
                    CatName = "Music",
                    CatOrder = 18,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 19,
                    CatName = "Sports",
                    CatOrder = 19,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                },
                new Category
                {
                    Id = 20,
                    CatName = "Children's Books",
                    CatOrder = 20,
                    CreatedDate = DateTime.UtcNow,
                    MarkedAsDeleted = false
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }

        private static void SeedBooks(ModelBuilder modelBuilder)
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "The Great Gatsby",
                    Description = "A story of the fabulously wealthy Jay Gatsby and his love for the beautiful Daisy Buchanan.",
                    Author = "F. Scott Fitzgerald",
                    Price = 12.99m,
                    CategoryId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "The story of young Scout Finch and her father Atticus in a racially divided Alabama town.",
                    Author = "Harper Lee",
                    Price = 14.99m,
                    CategoryId = 1
                },
                new Book
                {
                    Id = 3,
                    Title = "1984",
                    Description = "A dystopian novel about totalitarianism and surveillance society.",
                    Author = "George Orwell",
                    Price = 11.99m,
                    CategoryId = 3
                },
                new Book
                {
                    Id = 4,
                    Title = "The Hobbit",
                    Description = "A fantasy novel about Bilbo Baggins' journey with thirteen dwarves.",
                    Author = "J.R.R. Tolkien",
                    Price = 16.99m,
                    CategoryId = 8
                },
                new Book
                {
                    Id = 5,
                    Title = "Sherlock Holmes: A Study in Scarlet",
                    Description = "The first Sherlock Holmes novel introducing the famous detective.",
                    Author = "Arthur Conan Doyle",
                    Price = 9.99m,
                    CategoryId = 4
                },
                new Book
                {
                    Id = 6,
                    Title = "Pride and Prejudice",
                    Description = "A romantic novel of manners that follows the emotional development of Elizabeth Bennet.",
                    Author = "Jane Austen",
                    Price = 13.99m,
                    CategoryId = 5
                },
                new Book
                {
                    Id = 7,
                    Title = "Steve Jobs",
                    Description = "The biography of Apple's visionary co-founder and CEO.",
                    Author = "Walter Isaacson",
                    Price = 19.99m,
                    CategoryId = 6
                },
                new Book
                {
                    Id = 8,
                    Title = "The Art of War",
                    Description = "An ancient Chinese text on military strategy and tactics.",
                    Author = "Sun Tzu",
                    Price = 8.99m,
                    CategoryId = 2
                },
                new Book
                {
                    Id = 9,
                    Title = "The Da Vinci Code",
                    Description = "A mystery thriller novel about a murder in the Louvre Museum and a religious mystery.",
                    Author = "Dan Brown",
                    Price = 15.99m,
                    CategoryId = 9
                },
                new Book
                {
                    Id = 10,
                    Title = "The Power of Now",
                    Description = "A guide to spiritual enlightenment and living in the present moment.",
                    Author = "Eckhart Tolle",
                    Price = 17.99m,
                    CategoryId = 10
                },
                new Book
                {
                    Id = 11,
                    Title = "Rich Dad Poor Dad",
                    Description = "A personal finance book that advocates financial literacy and investment.",
                    Author = "Robert Kiyosaki",
                    Price = 21.99m,
                    CategoryId = 11
                },
                new Book
                {
                    Id = 12,
                    Title = "Clean Code",
                    Description = "A handbook of agile software craftsmanship with practical examples.",
                    Author = "Robert C. Martin",
                    Price = 45.99m,
                    CategoryId = 12
                },
                new Book
                {
                    Id = 13,
                    Title = "The Republic",
                    Description = "A philosophical work that explores the nature of justice and the ideal society.",
                    Author = "Plato",
                    Price = 18.99m,
                    CategoryId = 13
                },
                new Book
                {
                    Id = 14,
                    Title = "The Waste Land",
                    Description = "A landmark modernist poem that captures the disillusionment of post-World War I society.",
                    Author = "T.S. Eliot",
                    Price = 12.99m,
                    CategoryId = 14
                },
                new Book
                {
                    Id = 15,
                    Title = "Eat, Pray, Love",
                    Description = "A memoir of a woman's journey of self-discovery through Italy, India, and Indonesia.",
                    Author = "Elizabeth Gilbert",
                    Price = 16.99m,
                    CategoryId = 15
                },
                new Book
                {
                    Id = 16,
                    Title = "The Joy of Cooking",
                    Description = "A comprehensive cookbook with recipes and cooking techniques for all skill levels.",
                    Author = "Irma S. Rombauer",
                    Price = 35.99m,
                    CategoryId = 16
                },
                new Book
                {
                    Id = 17,
                    Title = "The Story of Art",
                    Description = "A comprehensive survey of the history of art from prehistoric times to the modern era.",
                    Author = "E.H. Gombrich",
                    Price = 29.99m,
                    CategoryId = 17
                },
                new Book
                {
                    Id = 18,
                    Title = "The Beatles: The Biography",
                    Description = "A detailed biography of the most influential band in rock music history.",
                    Author = "Bob Spitz",
                    Price = 24.99m,
                    CategoryId = 18
                },
                new Book
                {
                    Id = 19,
                    Title = "Moneyball",
                    Description = "The story of how the Oakland Athletics used statistical analysis to build a competitive baseball team.",
                    Author = "Michael Lewis",
                    Price = 19.99m,
                    CategoryId = 19
                },
                new Book
                {
                    Id = 20,
                    Title = "The Little Prince",
                    Description = "A poetic tale about a young prince who visits various planets in space.",
                    Author = "Antoine de Saint-Exupéry",
                    Price = 11.99m,
                    CategoryId = 20
                }
            };

            modelBuilder.Entity<Book>().HasData(books);
        }
    }
} 