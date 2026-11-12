using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SchoolAppliction.Dto;
using SchoolAppliction.Services;
using System.Security.Claims;

namespace SchoolAppliction.Controllers
{
    public class AuthUserController : Controller
    {
        private readonly ApiUserServie _apiservice;

        public AuthUserController()
        {
            _apiservice = new ApiUserServie();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUser obj)
        {
            TokenResponseDto objResponse =
                await _apiservice.AuthenticateUser(obj);

            if (objResponse!=null && objResponse.AccessToken.ToString() != "")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name,obj.Username));

                identity.AddClaim(new Claim(ClaimTypes.Role,objResponse.Role));

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                HttpContext.Session.SetString("ApiToken", objResponse.AccessToken);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("ApiToken", "");
            }

            //if (objResponse is not null &&
            //    !string.IsNullOrWhiteSpace(objResponse.AccessToken))
            //{
            //    HttpContext.Session.SetString("ApiToken", objResponse.AccessToken);
            //    return RedirectToAction("Index", "Home");
            //}

            //ModelState.AddModelError(
            //    string.Empty,
            //    "Invalid username or password.");

            return View(obj);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString("ApiToken", "");
            return RedirectToAction("Login","AuthUser");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
