﻿namespace OnlineStore.Presentation.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InventoryCount { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}