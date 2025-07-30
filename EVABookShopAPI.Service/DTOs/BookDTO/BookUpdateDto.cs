namespace EVABookShopAPI.Service.DTOs.BookDTO
{
    public class BookUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
}
