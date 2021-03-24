using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using FormularioAsistencia.Data;
using FormularioAsistencia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FormularioAsistencia.Controllers
{   
    [Authorize(Roles = "Administrativos")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly FormularioAsistenciaContext _context;

        public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, FormularioAsistenciaContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;
        }
        [HttpGet]
        public IActionResult CrearRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(CrearRolViewModel model)
        {
            if (ModelState.IsValid)
            {

                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Rol
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("VerRoles", "admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);

        }
        [HttpGet]
        public IActionResult VerRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public  async Task<IActionResult> EditarRol(string id)
        {
            var rol = await _context.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con ID = {id} no se puede encontrar.";
                return View("EditarRol/" + id);
            }

            var model = new EditarRolViewModel
            {
                Id = rol.Id,
                Rol = rol.Name
            };
            var usuarios = await _context.UserRoles.Where(x => x.RoleId == id).ToListAsync();
            for (int i = 0; i < usuarios.Count(); i++)
            {
                var nombre = await _context.Users.Where(x => x.Id == usuarios[i].UserId).FirstOrDefaultAsync();
                if (nombre != null)
                {
                    model.Users.Add(nombre.UserName);
                }
            }
            

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditarRol(EditarRolViewModel model)
        {
            var rol = await roleManager.FindByIdAsync(model.Id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con ID = {model.Id} no se puede encontrar.";
                return View("EditarRol");
            }
            else
            {
                rol.Name = model.Rol;

                var result = await roleManager.UpdateAsync(rol);

                if (result.Succeeded)
                {
                    return RedirectToAction("VerRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditarRolDeUsuario(string Id)
        {
            ViewBag.Id = Id;

            var rol = _context.Roles.Where(x => x.Id == Id).FirstOrDefault();

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con Id = {Id} no se puede encontrar.";
                return View("EditarRol");
            }

            var model = new List<UsuarioRolViewModel>();
            var usuariosRol = await _context.UserRoles.Where(x => x.RoleId == Id).ToListAsync();
            var usuarios = await _context.Users.ToListAsync();
            foreach (var user in usuarios)
            {
                var nombre = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
                var userRoleViewModel = new UsuarioRolViewModel
                {
                    Id = nombre.Id,
                    Nombre = nombre.UserName
                };

                if (await userManager.IsInRoleAsync(user, rol.Name))
                {
                    userRoleViewModel.Seleccionado = true;
                }
                else
                {
                    userRoleViewModel.Seleccionado = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditarRolDeUsuario(List<UsuarioRolViewModel> model, string Id)
        {
            var rol = await roleManager.FindByIdAsync(Id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con Id = {Id} no se puede encontrar.";
                return View("EditarRol");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].Id);

                IdentityResult result = null;

                if (model[i].Seleccionado && !(await userManager.IsInRoleAsync(user, rol.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, rol.Name);
                }
                else if (!model[i].Seleccionado && await userManager.IsInRoleAsync(user, rol.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, rol.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditarRol", new { Id = Id });
                }
            }

            return RedirectToAction("EditarRol", new { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> BorrarRol(string id)
        {
            var rol = await roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con Id = {id} no se puede encontrar.";
                return View("EditarRol");
            }
            else
            {
                var result = await roleManager.DeleteAsync(rol);

                if (result.Succeeded)
                {
                    return RedirectToAction("VerRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("VerRoles");
            }
        }
        
    }
    }
