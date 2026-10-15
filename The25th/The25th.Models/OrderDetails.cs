using System.Collections.Generic;
using System;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace The25th.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}