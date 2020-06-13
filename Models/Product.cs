using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoesStoreApi.Models {
    public class Product {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Category { get; set; }
        public string Img { get; set; }

        [Required]
        public string Price { get; set; }
        //public int SizeId { get; set; }
        //public Size Size { get; set; }
        public string Size { get; set; }

        [Required]
        public int Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

    }
}