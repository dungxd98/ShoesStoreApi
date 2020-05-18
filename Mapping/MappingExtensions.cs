using ShoesStoreApi.DTOs;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Mapping {
    public static class MappingExtensions {
        public static ProductDTO ToDTO (this Product product) {
            return new ProductDTO {
                Id = product.Id,
                    Name = product.Name,
                    Category = product.Category,
                    Img = product.Img,
                    Price = product.Price,
                    Size = product.Size,
                    Amount = product.Amount,
                    Description = product.Description
            };
        }
        public static Product ToProduct (this ProductDTO productDTO) {
            return new Product {
                Id = productDTO.Id,
                    Name = productDTO.Name,
                    Category = productDTO.Category,
                    Img = productDTO.Img,
                    Price = productDTO.Price,
                    Size = productDTO.Size,
                    Amount = productDTO.Amount,
                    Description = productDTO.Description
            };
        }
    }
}