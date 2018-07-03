namespace Nevara.ViewModel
{
    public class ProductViewModel
    {
     
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public string Thumbnail { get; set; }
        public double? Depth { get; set; }
        public decimal OriginalPrice { get; set; } 
        public decimal Price { get; set; }        
        public decimal? PromotionPrice { get; set; }
        public int CategoryId { get; set; }
        public int ColorId { get; set; }
        public int MaterialId { get; set; }
        public int CollectionId { get; set; }
        public int ManufacturerId { get; set; }
        public int? Quantity { get; set; } 
        public bool? HomeFlag { get; set; }
        public bool? NewFlag { get; set; }
        public bool? HotFlag { get; set; }
    }
}
