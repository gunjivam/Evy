using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eevee.Data;
using Eevee.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Eevee.Pages.Users
{
    public class SignInModel : PageModel
    {
        private readonly Eevee.Data.EeveeContext _context;

        public SignInModel(Eevee.Data.EeveeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string user_info{ get; set; }

        [BindProperty]
        public string user_password { get; set; }

        public string msg;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(_context.User.Any(u => u.Email == user_info)){
                var user = from u in _context.User
                           where u.Email == user_info
                           select u;
                var pass = user.Select(u => u.Password).FirstOrDefault();

                if(pass == user_password)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Select(u => u.Username).FirstOrDefault()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Select(u => u.Username).FirstOrDefault()));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true});

                    HttpContext.Session.SetString("Username", user.Select(u => u.Username).FirstOrDefault());
                    return RedirectToPage("/Index");
                }
                msg = "Wrong password.";
                return Page();

            }else if (_context.User.Any(u => u.Username == user_info)){
                var user = from u in _context.User
                           where u.Username == user_info
                           select u;
                var pass = user.Select(u => u.Password).FirstOrDefault();

                if (pass == user_password)
                {

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Select(u => u.Username).FirstOrDefault()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Select(u => u.Username).FirstOrDefault()));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true });

                    HttpContext.Session.SetString("Username", user.Select(u => u.Username).FirstOrDefault());
                    return RedirectToPage("/Index");
                }
                msg = "Wrong password.";
                return Page();
            }

            msg = "Not user matches that description.";
            return Page();
        }
    }

}