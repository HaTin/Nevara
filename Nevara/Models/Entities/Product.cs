using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nevara.Data.Interfaces;
using Nevara.Models.Interfaces;

namespace Nevara.Models.Entities
{
    [Table("Products")]
    public class Product : IHasSoftDelete
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]       
        [StringLength(255)]
        public string Name { get; set; }

        public double? Width { get; set; }
        public double? Depth { get; set; }
        public double? Height { get; set; }
        [Column(TypeName = "varchar(255)")]
        [StringLength(255)]        
        public string Thumbnail { get; set; }
        
        [Required]        
        public decimal OriginalPrice { get; set; }
        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }        
        public decimal? PromotionPrice { get; set; }
        [Required]       
        public int CategoryId { get; set; }
        [Required]      
        public int ColorId { get; set; }
        [Required]
        public int MaterialId { get; set; }
        public int CollectionId { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }    
        public bool? HomeFlag { get; set; }      
        public bool? NewFlag { get; set; }      
        public bool? HotFlag { get; set; }            
        
        public bool IsDeleted { get; set; }    
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("ColorId")]
        public  Color Color { get; set; }
        [ForeignKey("MaterialId")]
        public Material Material { get; set; }
        [ForeignKey("CollectionId")]
        public Collection Collection { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }
        public  ICollection<Image> Images { get; set; }
    }
}
