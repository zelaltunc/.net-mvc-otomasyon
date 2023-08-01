using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;
using zelal_tunc_211216069.Entites;
using zelal_tunc_211216069.Models;

namespace zelal_tunc_211216069.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)

        {
            if (ModelState.IsValid)
            {
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                User user  = _databaseContext.Users.FirstOrDefault(x => x.Username.ToLower() == model.UserName.ToLower()&& x.Password == hashedPassword);


                if (User !=null)
                    {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.UserName), "Bu isim zaten var");
                    }
                }

                else {
                    ModelState.AddModelError("", "İsim ya da şifre giriniz");
                        
                        }
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty));
                claims.Add(new Claim("Username", user.Username));

                ClaimsIdentity identity= new ClaimsIdentity(claims,
                   CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");


            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(RegisterViewModel model)

        {
            if (ModelState.IsValid)
            {
                if(_databaseContext.Users.Any(x => x.Username.ToLower() == model.UserName.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.UserName), "Bu isim zaten var");
                    View(model);
                }
                   
                string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");
                string saltedPassword = model.Password + md5Salt;
                string hashedPassword = saltedPassword.MD5();

                User user = new()
                {
                    Username = model.UserName,
                    FullName = model.FullName,
                    Password = hashedPassword,
                };
                _databaseContext.Users.Add(user); 
                _databaseContext.SaveChanges();

            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           return RedirectToAction(nameof(Login));

        }


    }
}
