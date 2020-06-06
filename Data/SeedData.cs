using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Data {
    public class SeedData {
        public async static Task Initialize (ShoesStoreApiContext context) {
            context.Database.Migrate ();
            if (!context.Products.Any ()) {
                context.Products.AddRange (
                    new Product {
                        Name = "PUMA Multicolour Hybrid Fuego",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1577343219/N32909333V_1.jpg",
                            Price = "1200000",
                            Size = "43",
                            Amount = 5,
                            Description = "Striped Shirt 902Peacoat "
                    },
                    new Product {
                        Name = "SKECHERS Multicolour Chunky Sneakers",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1574232580/N32126694V_2.jpg",
                            Price = "1800000",
                            Size = "41",
                            Amount = 5,
                            Description = "Striped Shirt 902Peacoat "
                    },
                    new Product {
                        Name = "Stock Multicolour Chunky Sneakers",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1577617966/N33029524V_2.jpg",
                            Price = "800000",
                            Size = "42",
                            Amount = 5,
                            Description = "Striped Shirt 902Peacoat "
                    },
                    new Product {
                        Name = "Stock  White Mesh Detail Sneakers",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1577616941/N33029910V_2.jpg",
                            Price = "1250000",
                            Size = "40",
                            Amount = 5,
                            Description = "Striped Shirt 902Peacoat"
                    },
                    new Product {
                        Name = "adidas nfant Navy FortaRun Running Shoes",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1579505096/N33875062V_2.jpg",
                            Price = "1480000",
                            Size = "39",
                            Amount = 5,
                            Description = "Striped Shirt 902Peacoat "
                    },
                    new Product {
                        Name = "SKECHERS  Girls Nude Go Walk Joy Slip Ons",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1575525369/N32324768V_2.jpg",
                            Price = "850000",
                            Size = "38",
                            Amount = 5,
                            Description = "Striped Shirt 902Peacoat "
                    }
                );
                await context.SaveChangesAsync ();
            }
        }
    }
}