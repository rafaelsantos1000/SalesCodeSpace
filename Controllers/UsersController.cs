using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCodeSpace.Data;
using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Enums;
using SalesCodeSpace.Helpers;
using SalesCodeSpace.Models;
using SalesCodeSpace.Responses;

namespace SalesCodeSpace.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IMailHelper _mailHelper;

        public UsersController(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper, ICombosHelper combosHelper, IMailHelper mailHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _combosHelper = combosHelper;
            _mailHelper = mailHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users
            .Include(u => u.City)
            .ThenInclude(c => c!.State)
            .ThenInclude(s => s!.Country)
            .ToListAsync());
        }


        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new()
            {
                Id = Guid.Empty.ToString(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                UserType = UserType.Admin
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                model.ImageId = imageId;
                User user = await _userHelper.AddUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este email já está a ser usado");
                    model.Countries = await _combosHelper.GetComboCountriesAsync();
                    model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
                    model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
                    return View(model);
                }

                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string? tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(
                    $"{model.FirstName} {model.LastName}",
                    model.Username!,
                    "SalesCodeSpace 2024 - Confirmação de Email",
                    $"<h1>SalesCodeSpace 2024 - Confirmação de Email</h1>" +
                        $"Clique no link para poder entrar como utilizador:, " +
                        $"<p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                if (response.IsSuccess)
                {
                    ViewBag.Message = "As instruções para poder entrar foram enviadas para o seu email.";
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
            return View(model);
        }


        public JsonResult GetStates(int countryId)
        {
            Country? country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);

            if (country == null) { return null!; }

            return Json(country.States!.OrderBy(d => d.Name));
        }


        public JsonResult GetCities(int stateId)
        {
            State? state = _context.States
                .Include(s => s.Cities)
                .FirstOrDefault(s => s.Id == stateId);

            if (state == null) { return null!; }

            return Json(state.Cities!.OrderBy(d => d.Name));
        }
    }
}