using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace The25th.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public string Author { get; set; } = string.Empty;
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        [Display(Name = "List Price")]
        [Required]
        public double ListPrice { get; set; }
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        [Display(Name = "Price (1-50)")]
        [Required]
        public double Price { get; set; }
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        [Display(Name = "Price (50+)")]
        [Required]
        public double Price50 { get; set; }
        [Range(1, 1000, ErrorMessage = "Price must be between 1 and 1000.")]
        [Display(Name = "Price (100+)")]
        [Required]
        public double Price100 { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        [Display(Name = "Product Image")]
        public string? ImageUrl { get; set; } = string.Empty;
    }
}