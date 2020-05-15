using ShoesStoreApi.DTOs;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Mapping {
    public static class MappingExtensions {
        public static ProductDTO ToDTO (this Product product) {
            return new ProductDTO {
                Id = product.Id,
                    Name = product.Name,
                    Img = product.Img,
                    Price = product.Price,
                    Size = product.Size,
                    Number = product.Number,
                    Description = product.Description
            };
        }
        public static Product ToProduct (this ProductDTO productDTO) {
            return new Product {
                Id = productDTO.Id,
                    Name = productDTO.Name,
                    Img = productDTO.Img,
                    Price = productDTO.Price,
                    Size = productDTO.Size,
                    Number = productDTO.Number,
                    Description = productDTO.Description
            };
        }
    }
}