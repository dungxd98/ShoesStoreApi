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
                        Name = "adidas Giày Stan Smith",
                            Img = "https://assets.adidas.com/images/w_600,f_auto,q_auto/8b9f0b3fb4af47259912aa9b010d82db_9366/Giay_Stan_Smith_Mau_trang_M20324_07_standard.jpg",
                            Price = "1500000",
                            Size = "43",
                            Category="Adidas",
                            Amount = 5,
                            Description = "Giày Puma Hybrid Fuegor là mẫu giày thể thao được yêu thích của thương hiệu Puma",
                            Status="processed"
                    },
                    new Product {
                        Name = "SKECHERS Multicolour Chunky Sneakers",
                            Img = "https://k.nooncdn.com/t_desktop-pdp-v1/v1574232580/N32126694V_2.jpg",
                            Price = "2450000",
                            Size = "41",
                        Category = "Skechers",
                        Amount = 5,
                            Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    },
                    new Product {
                        Name = "Men's S Sport by Skechers Brennen Athletic Shoes - Black",
                            Img = "https://target.scene7.com/is/image/Target/GUEST_03da9a68-e278-4e7d-b7e4-b367cc997ba7?wid=1701&hei=1701&fmt=webp",
                            Price = "800000",
                            Size = "42",
                        Category = "Skechers",
                        Amount = 5,
                            Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    },
                    new Product {
                        Name = "Men's S Sport by Skechers Jerald Wide Width Sneakers - Gray",
                            Img = "https://target.scene7.com/is/image/Target/GUEST_97a82497-dd5f-4c68-863f-9a2ff7761279?wid=1701&hei=1701&fmt=webp",
                            Price = "1250000",
                            Size = "40",
                        Category = "Skechers",
                        Amount = 5,
                            Description = "Striped Shirt 902Peacoat",
                        Status = "processed"
                    },
                    new Product {
                        Name = "Skechers Men's D'Lites 3 Zenway – urbanAthletics",
                            Img = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcRqgOeBz0insw-mk40k-lM9VZQAui8wvOU3F-sb4YfxQ0-7NZfQ&usqp=CAU",
                            Price = "1480000",
                            Size = "39",
                        Category = "Skechers",
                        Amount = 5,
                            Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    },
                    new Product {
                        Name = "Skechers d'lites chunky trainers in black white",
                            Img = "https://images.asos-media.com/products/skechers-dlites-chunky-trainers-in-black-white/11350002-2?$XXL$&wid=513&fit=constrain",
                            Price = "1250000",
                            Size = "42",
                        Category = "Skechers",
                        Amount = 5,
                            Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    },
                    new Product
                    {
                        Name = "Men's Originals Shoes & Sneakers",
                        Img = "https://assets.adidas.com/images/w_385,h_385,f_auto,q_auto:sensitive,fl_lossy/710afa24baf940998f32ab0b00ee99fc_9366/superstar-shoes.jpg",
                        Price = "1700000",
                        Size = "42",
                        Category = "Adidas",
                        Amount = 5,
                        Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    },
                    new Product
                    {
                        Name = "Calçado adidas para running",
                        Img = "https://assets.adidas.com/images/w_385,h_385,f_auto,q_auto:sensitive,fl_lossy/f20d3002606a4ccca59aab0b00ad2d4c_9366/sapatos-ultraboost-20.jpg",
                        Price = "2200000",
                        Size = "42",
                        Category = "Adidas",
                        Amount = 5,
                        Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    },
                    new Product
                    {
                        Name = "adidas Supernova Shoes - White",
                        Img = "https://assets.adidas.com/images/h_320,f_auto,q_auto:sensitive,fl_lossy/a980d5d3fc494246ad96ab9600b4c640_9366/Supernova_Shoes_White_FV6026_01_standard.jpg",
                        Price = "2100000",
                        Size = "42",
                        Category = "Adidas",
                        Amount = 5,
                        Description = "Striped Shirt 902Peacoat ",
                        Status = "processed"
                    }


                );
                await context.SaveChangesAsync ();
            }
        }
    }
}