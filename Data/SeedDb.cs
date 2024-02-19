using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Enums;
using SalesCodeSpace.Helpers;

namespace SalesCodeSpace.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Rafael", "Santos", "rafaelsantos@yopmail.com", "213658959", "Rua da Luz", UserType.Admin);
            await CheckUserAsync("2020", "Madalena", "Santos", "madasantos@yopmail.com", "213658957", "Rua da Luz", UserType.User);
        }

        private async Task<User> CheckUserAsync
        (string document,
        string firstName,
        string lastName,
        string email,
        string phone,
        string address,
        UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
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
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }


        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Tecnologia" });
                _context.Categories.Add(new Category { Name = "Roupa" });
                _context.Categories.Add(new Category { Name = "Jogos" });
                _context.Categories.Add(new Category { Name = "Beleza" });
                _context.Categories.Add(new Category { Name = "Nutrição" });
                _context.Categories.Add(new Category { Name = "Calçado" });
                _context.Categories.Add(new Category { Name = "Desporto" });
                _context.Categories.Add(new Category { Name = "Brinquedos" });



            }

            await _context.SaveChangesAsync();
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