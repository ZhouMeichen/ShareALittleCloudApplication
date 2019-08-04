using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareALittle.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 1)]
        public string CategoryName { get; set; }

        [StringLength(200)]
        public string CategoryDescription { get; set; }

    }
}
