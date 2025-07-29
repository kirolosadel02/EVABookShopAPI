namespace EVABookShopAPI.Service.DTOs.CategoryDTO
{
        public class CategoryDto
        {
            public int Id { get; set; }
            public string CatName { get; set; }
            public int CatOrder { get; set; }
            public bool IsActive { get; set; }
        }
}
