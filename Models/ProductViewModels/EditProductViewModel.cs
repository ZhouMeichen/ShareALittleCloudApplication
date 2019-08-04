using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace ShareALittle.Models.ProductViewModels
{
    public class EditProductViewModel
    {
        [Display(Name = "Image")]
        public IFormFile ProductImage{ get; set; }

        public int ProductID { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Price")]
        public decimal ProductPrice { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        public Category Category { get; set; }
        [Display(Name = "Contact Detail")]
        public string ContactDetail { get; set; }

        [Display(Name = "Owner")]
        public string OwnerID { get; set; }
    }
}
