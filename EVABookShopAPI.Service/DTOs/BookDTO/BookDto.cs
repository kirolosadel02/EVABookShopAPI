﻿namespace EVABookShopAPI.Service.DTOs.BookDTO
{
    namespace EVABookShop.DTOs
    {
        public class BookDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public int CategoryId { get; set; }
            public string? CategoryName { get; set; }
        }
    }

}
