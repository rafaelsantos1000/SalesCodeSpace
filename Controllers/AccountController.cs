using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesCodeSpace.Data;
using SalesCodeSpace.Data.Entities;
using SalesCodeSpace.Enums;
using SalesCodeSpace.Helpers;
using SalesCodeSpace.Models;
using SalesCodeSpace.Responses;
using Vereyon.Web;

namespace SalesCodeSpace.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;

        public AccountController(IUserHelper userHelper, DataContext context, ICombosHelper combosHelper, IBlobHelper blobHelper, IMailHelper mailHelper, IFlashMessage flashMessage)
        {
            _userHelper = userHelper;
            _context = context;
            _combosHelper = combosHelper;
            _blobHelper = blobHelper;
            _mailHelper = mailHelper;
            _flashMessage = flashMessage;
        }


        public IActionResult Login()
        {
            if (User!.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Foi superado o número máximo de tentativas, a sua conta está bloqueada, tente novamente mais tarde.");
                }
                else if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "O utilizador ainda não foi confirmado, deve seguir as instruções enviadas para o email.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email e palavra-passe incorretos.");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult NotAuthorized()
        {
            return View();
        }


        public async Task<IActionResult> Register()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                UserType = UserType.User,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
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
                    ModelState.AddModelError(string.Empty, "Este email já está a ser usado.");
                    model.Countries = await _combosHelper.GetComboCountriesAsync();
                    model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
                    model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
                    return View(model);
                }

                string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                string tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                Response response = _mailHelper.SendMail(
                    $"{model.FirstName} {model.LastName}",
                    model.Username,
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
            Country country = _context.Countries
                .Include(c => c.States)
                .FirstOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return null!;
            }

            return Json(country.States!.OrderBy(d => d.Name));
        }

        public JsonResult GetCities(int stateId)
        {
            State state = _context.States
                .Include(s => s.Cities)
                .FirstOrDefault(s => s.Id == stateId);
            if (state == null)
            {
                return null!;
            }

            return Json(state.Cities.OrderBy(c => c.Name));
        }


        public async Task<IActionResult> ChangeUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }

            EditUserViewModel model = new()
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                ImageId = user.ImageId,
                Cities = await _combosHelper.GetComboCitiesAsync(user.City!.State!.Id),
                CityId = user.City.Id,
                Countries = await _combosHelper.GetComboCountriesAsync(),
                CountryId = user.City.State.Country!.Id,
                StateId = user.City.State.Id,
                States = await _combosHelper.GetComboStatesAsync(user.City.State.Country.Id),
                Id = user.Id,
                Document = user.Document
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                user!.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageId = imageId;
                user.City = await _context.Cities.FindAsync(model.CityId);
                user.Document = model.Document;

                await _userHelper.UpdateUserAsync(user);
                return RedirectToAction("Index", "Home");
            }

            model.Countries = await _combosHelper.GetComboCountriesAsync();
            model.States = await _combosHelper.GetComboStatesAsync(model.CountryId);
            model.Cities = await _combosHelper.GetComboCitiesAsync(model.StateId);
            return View(model);
        }


        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.OldPassword == model.NewPassword)
                {
                    ModelState.AddModelError(string.Empty, "Deve inserir uma palavra-passe diferente.");
                    return View(model);
                }

                var user = await _userHelper.GetUserAsync(User.Identity!.Name!);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword!, model.NewPassword!);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault()!.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Utilizador não encontrado.");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Email!);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "O Email não corresponde ao email registado.");
                    return View(model);
                }

                string myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);
                string link = Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);
                _mailHelper.SendMail(
                    $"{user.FullName}",
                    model.Email!,
                    "SalesCodeSpace 2024 - Recuperação da Palavra-passe",
                    $"<h1>Shopping - Recuperação da Palavra-passe</h1>" +
                    $"Para recuperar a palavra-passe clique no link:" +
                    $"<p><a href = \"{link}\">Reset Password</a></p>");
                ViewBag.Message = "As instruções para recuperar a sua palavra-passe foram enviadas para o seu correio.";
                return View();
            }

            return View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            User user = await _userHelper.GetUserAsync(model.UserName!);
            if (user != null)
            {
                IdentityResult result = await _userHelper.ResetPasswordAsync(user, model.Token!, model.Password!);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Palavra-passe alterada com êxito.";
                    return View();
                }

                ViewBag.Message = "Erro ao trocar a palavra-passe.";
                return View(model);
            }

            ViewBag.Message = "Utilizador não encontrado.";
            return View(model);
        }

        // GET: User/Details/5
        public async Task<IActionResult> ResendToken(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User user = await _userHelper.GetUserAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            ResendTokenViewModel model = new()
            {
                Username = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendToken(ResendTokenViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(model.Username!);
                if (!user.EmailConfirmed)
                {
                    string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user!);
                    string tokenLink = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user!.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    Response response = _mailHelper.SendMail(
                        $"{model.FirstName} {model.LastName}",
                        model.Username!,
                        "SalesCodeSpace 2024 - Confirmação de Email",
                        $"<h1>SalesCodeSpace 2024 - Confirmação de Email</h1>" +
                            $"Para entrar como utilizador por favor clique no seguinte link:, " +
                            $"<p><a href = \"{tokenLink}\">Confirmar Email</a></p>");
                    if (response.IsSuccess)
                    {
                        _flashMessage.Info("Email reenviado!");
                    }
                    else
                    {
                        _flashMessage.Danger(response.Message);
                    }
                }
                else
                {
                    _flashMessage.Warning("Email não foi enviando porque este utilizador já tinha confirmado.");
                }
            }
            return View(model);
        }

    }
}