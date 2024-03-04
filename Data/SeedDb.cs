using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Enums;
using SalesCodeSpace.Helpers;

namespace SalesCodeSpace.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;

        public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Rafael", "Santos", "rafaelsaints@yopmail.com", "213658959", "Rua da Luz", "rafasaints.jpg", UserType.Admin);
            await CheckUserAsync("2020", "Madalena", "Santos", "madasaints@yopmail.com", "213658957", "Rua da Luz", "madasaints.jpg", UserType.Admin);
            await CheckUserAsync("3030", "Brad", "Pitt", "brad@yopmail.com", "322 311 4620", "Rua da Lua e do Sol", "Brad.jpg", UserType.User);
            await CheckUserAsync("4040", "Angelina", "Jolie", "angelina@yopmail.com", "322 311 4620", "Rua da Lua e do Sol", "Angelina.jpg", UserType.User);
            await CheckUserAsync("4040", "Bob", "Marley", "bob@yopmail.com", "322 311 4620", "Rua da Lua e do Sol", "bob.jpg", UserType.User);
            await CheckUserAsync("4040", "Liv", "Taylor", "liv@yopmail.com", "322 311 4620", "Rua da Lua e do Sol", "Liv.jpg", UserType.User);
            await CheckUserAsync("4040", "Marta", "Ferreia", "marta@yopmail.com", "322 311 4620", "Rua da Lua e do Sol", "marta-ferreira.jpg", UserType.User);
            await CheckUserAsync("4040", "Rafael", "Santos", "rafa@yopmail.com", "322 311 4620", "Rua da Lua e do Sol", "rafa.jpg", UserType.User);
            await CheckProductsAsync();
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calçado", "Desporto" }, new List<string>() { "adidas_barracuda.png" });
                await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calçado", "Desporto" }, new List<string>() { "Adidas_superstar.png" });
                await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnologia", "Apple" }, new List<string>() { "airpos.png", "airpos2.png" });
                await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnologia" }, new List<string>() { "audifonos_bose.png" });
                await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Desporto" }, new List<string>() { "bicicleta_ribble.png" });
                await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Roupa" }, new List<string>() { "camisa_cuadros.png" });
                await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Desporto" }, new List<string>() { "casco_bicicleta.png", "casco.png" });
                await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnologia", "Apple" }, new List<string>() { "ipad.png" });
                await AddProductAsync("iPhone 13", 5200000M, 6F, new List<string>() { "Tecnologia", "Apple" }, new List<string>() { "iphone13.png", "iphone13b.png", "iphone13c.png", "iphone13d.png" });
                await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnologia", "Apple" }, new List<string>() { "mac_book_pro.png" });
                await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Desporto" }, new List<string>() { "mancuernas.png" });
                await AddProductAsync("Mascarilla Cara", 26000M, 100F, new List<string>() { "Beleza" }, new List<string>() { "mascarilla_cara.png" });
                await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calçado", "Desporto" }, new List<string>() { "newbalance530.png" });
                await AddProductAsync("New Balance 565", 179000M, 12F, new List<string>() { "Calçado", "Desporto" }, new List<string>() { "newbalance565.png" });
                await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calçado", "Desporto" }, new List<string>() { "nike_air.png" });
                await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calçado", "Desporto" }, new List<string>() { "nike_zoom.png" });
                await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Roupa", "Desporto" }, new List<string>() { "buso_adidas.png" });
                await AddProductAsync("Suplemento Boots Original", 15600M, 12F, new List<string>() { "Nutrição" }, new List<string>() { "Boost_Original.png" });
                await AddProductAsync("Whey Protein", 252000M, 12F, new List<string>() { "Nutrição" }, new List<string>() { "whey_protein.png" });
                await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Animais" }, new List<string>() { "arnes_mascota.png" });
                await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Animais" }, new List<string>() { "cama_mascota.png" });
                await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnologia" }, new List<string>() { "teclado_gamer.png" });
                await AddProductAsync("Silla Gamer", 980000M, 12F, new List<string>() { "Gamer", "Tecnologia" }, new List<string>() { "silla_gamer.png" });
                await AddProductAsync("Mouse Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnologia" }, new List<string>() { "mouse_gamer.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnologia" });
                _context.Categories.Add(new Category { Name = "Roupa" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Beleza" });
                _context.Categories.Add(new Category { Name = "Nutrição" });
                _context.Categories.Add(new Category { Name = "Calçado" });
                _context.Categories.Add(new Category { Name = "Desporto" });
                _context.Categories.Add(new Category { Name = "Animais" });
                _context.Categories.Add(new Category { Name = "Apple" });



            }

            await _context.SaveChangesAsync();
        }


        private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
        {
            Product product = new()
            {
                Description = name,
                Name = name,
                Price = price,
                Stock = stock,
                ProductCategories = new List<ProductCategory>(),
                ProductImages = new List<ProductImage>()
            };

            /*foreach (string? category in categories)
            {
                product.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
            }*/

            foreach (var categoryName in categories)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
                if (category != null)
                {
                    product.ProductCategories.Add(new ProductCategory { Category = category });
                }
            }


            foreach (string? image in images)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync(Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "products", image), "products");
                /*Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}//wwwroot//images//products//{image}", "products");*/
                product.ProductImages.Add(new ProductImage { ImageId = imageId });
            }

            _context.Products.Add(product);
        }

        private async Task<User> CheckUserAsync
        (string document,
        string firstName,
        string lastName,
        string email,
        string phone,
        string address,
        string image,
        UserType userType)
        {
            User? user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {

                Guid imageId = await _blobHelper.UploadBlobAsync(Path.Combine(Environment.CurrentDirectory, "wwwroot", "images", "users", image), "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = _context.Cities.FirstOrDefault(),
                    UserType = userType,
                    ImageId = imageId
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Portugal",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Estremadura",
                            Cities = new List<City>() {
                                new City() { Name = "Lisboa" },
                                new City() { Name = "Amadora" },
                                new City() { Name = "Almada" },
                                new City() { Name = "Torres Vedras" },
                                new City() { Name = "Cascais" },
                            }
                        },
                        new State()
                        {
                            Name = "Ribatejo",
                            Cities = new List<City>() {
                                new City() { Name = "Santarém" },
                                new City() { Name = "Coruche" },
                                new City() { Name = "Cartaxo" },
                                new City() { Name = "Vila Franca de Xira" },
                                new City() { Name = "Almeirim" },
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos da América",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Florida",
                            Cities = new List<City>() {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" },
                            }
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = new List<City>() {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El Paso" },
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Alemanha",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Baviera",
                            Cities = new List<City>() {
                                new City() { Name = "Munique" },
                                new City() { Name = "Nuremberguer" },
                                new City() { Name = "Erlangen" },
                                new City() { Name = "Aschafemburgo" },
                                new City() { Name = "Kempten" },
                            }
                        },
                        new State()
                        {
                            Name = "Baixa Saxônia",
                            Cities = new List<City>() {
                                new City() { Name = "Ammerland" },
                                new City() { Name = "Goslar" },
                                new City() { Name = "Northeim" },
                            }
                        },
                    }
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}