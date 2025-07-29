﻿namespace EVABookShopAPI.Service.DTOs
{
    public class PaginationDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public int Skip => (Page - 1) * PageSize;
    }
}
