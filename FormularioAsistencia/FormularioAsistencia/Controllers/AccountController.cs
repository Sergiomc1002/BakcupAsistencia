using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormularioAsistencia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Text;
using System.Security.Cryptography;

using MailKit.Net.Smtp;
using MimeKit;

namespace FormularioAsistencia.Controllers
{
    [Authorize(Roles = "Administrativos")]
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            var roles = roleManager.Roles.ToList();
            model.Roles = roles;
            model.RolesAñadir = new List<bool>();
            model.RolesAñadir.AddRange(Enumerable.Repeat(false, roles.Count));
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var roles = roleManager.Roles.ToList();
            model.Roles = roles;

            if (ModelState.IsValid)
            {

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.CreateAsync(user, token);
                token = await userManager.GeneratePasswordResetTokenAsync(user);


                if (result.Succeeded)
                {
                    var linkReiniciarPassword = Url.Action("ReiniciarContraseña", "Account",
                       new { email = model.Email, token = token }, Request.Scheme);
                    string[] usuario = model.Email.Split("@");
                    EnviarCorreo(usuario[0], linkReiniciarPassword);
                    if (model.Roles != null && model.RolesAñadir != null)
                        for (int i = 0; i < model.RolesAñadir.Count; i++)
                        {
                            if (model.RolesAñadir[i])
                            {
                                await userManager.AddToRoleAsync(user, model.Roles[i].Name);
                            }

                        }

                    return RedirectToAction("ListaUsuarios", "Account");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegistrarEstudiante()
        {
            var model = new RegistrarEstudianteViewModel();
            var roles = roleManager.Roles.ToList();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegistrarEstudiante(RegistrarEstudianteViewModel model)
        {


            if (ModelState.IsValid)
            {

                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var result = await userManager.CreateAsync(user, token);
                token = await userManager.GeneratePasswordResetTokenAsync(user);


                if (result.Succeeded)
                {
                    var linkReiniciarPassword = Url.Action("ReiniciarContraseña", "Account",
                       new { email = model.Email, token = token }, Request.Scheme);
                    string[] usuario = model.Email.Split("@");
                    EnviarCorreo(usuario[0], linkReiniciarPassword);
                    await userManager.AddToRoleAsync(user,"Estudiante");


                    return View("UsuarioCreado");
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        static void EnviarCorreo(string Email, string link)
        {
            MimeMessage mensaje = new MimeMessage();
            MailboxAddress from = new MailboxAddress("Asistencias Física", "asistencias.efis@ucr.ac.cr");
            mensaje.From.Add(from);
            MailboxAddress to = new MailboxAddress(Email, Email + "@ucr.ac.cr");
            mensaje.To.Add(to);
            mensaje.Subject= "Cambiar Contraseña Plataforma de Solitudes de Asistencia";
            BodyBuilder body = new BodyBuilder();
            body.HtmlBody = "<h3>Por favor dirijase a: " + link + " , para poder cambiar su contraseña.<h3>";
            body.TextBody= "Por favor dirijase a: " + link + " , para poder cambiar su contraseña.";
            mensaje.Body = body.ToMessageBody();
            SmtpClient cliente = new SmtpClient();
            cliente.Connect("smtp.ucr.ac.cr", 587, false);
            cliente.Authenticate("asistencias.efis", "DnF3bg2a@EF1s");
            cliente.Send(mensaje);
            cliente.Disconnect(true);
            cliente.Dispose();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            var user = userManager.GetUserAsync(HttpContext.User);
            if (user.Result != null)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "No se pudo iniciar Sesion");
            }

            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CambiarContraseña()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CambiarContraseña(CambiarContraseñaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await userManager.ChangePasswordAsync(user, model.Actual, model.NuevaContrasena);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Description == "Incorrect password.")
                        {
                            ModelState.AddModelError(string.Empty, "Contraseña actual incorrecta.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    return View();
                }


                await signInManager.RefreshSignInAsync(user);
                return View("ContraseñaCambiada");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult OlvideContraseña()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> OlvideContraseña(OlvideContraseñaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {

                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var linkReiniciarPassword = Url.Action("ReiniciarContraseña", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);
                    string[] usuario = model.Email.Split("@");

                    EnviarCorreo(usuario[0], linkReiniciarPassword);

                    return View("ConfirmacionOlvideContraseña");
                }
                return View("ConfirmacionOlvideContraseña");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ReiniciarContraseña(string token, string email)
        {
            var model = new ReiniciarContraseñaViewModel();
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Token Inválido.");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ReiniciarContraseña(ReiniciarContraseñaViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await userManager.FindByEmailAsync(model.email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.token, model.Contraseña);
                    if (result.Succeeded)
                    {
                        return View("ConfirmacionReinicioContaseña");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }


                return View("ConfirmacionReinicioContaseña");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ListaUsuarios(ListaUsuariosViewModel model)
        {

            model.Users = userManager.Users.OrderBy(x => x.Email).ToList();
            model.Roles = roleManager.Roles.OrderBy(x => x.Name).ToList();
            model.Pertenece = new List<List<Boolean>>();
            foreach (var user in model.Users)
            {
                List<Boolean> pertenece = new List<Boolean>();
                foreach (var rol in model.Roles)
                {
                    if (await userManager.IsInRoleAsync(user, rol.Name))
                    {
                        pertenece.Add(true);
                    }
                    else
                    {
                        pertenece.Add(false); ;
                    }

                }
                model.Pertenece.Add(pertenece);
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditarUsuario(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuario con id = {id} no se puede encontrar.";
                return View("ListaUsuarios");
            }

            var userRoles = roleManager.Roles.OrderBy(x => x.Name).ToList();


            var model = new EditarUsuarioViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Roles = userRoles

            };
            for (int i = 0; i < userRoles.Count(); i++)
            {
                model.Pertenece.Add(await userManager.IsInRoleAsync(user, userRoles.ElementAt(i).Name));
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            var roles = roleManager.Roles.OrderBy(x => x.Name).ToList();
            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuario con id = {model.Id} no se puede encontrar.";
                return View("EditarUsuario");
            }
            else
            {
                for (int i = 0; i < roles.Count; i++)
                {
                    if (model.Pertenece[i])
                    {
                        await userManager.AddToRoleAsync(user, roles[i].Name);
                    }
                    else
                    {
                        await userManager.RemoveFromRoleAsync(user, roles[i].Name);
                    }


                }
                return RedirectToAction("ListaUsuarios");

            }
        }
        [HttpPost]
        public async Task<IActionResult> BorrarUsuario(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Usuario con id = {id} no se puede encontrar.";
                return View("ListaUsuarios");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListaUsuarios");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }

    }
}
