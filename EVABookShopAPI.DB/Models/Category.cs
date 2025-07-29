namespace EVABookShopAPI.DB.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public int CatOrder { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool MarkedAsDeleted { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
