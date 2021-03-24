using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormularioAsistencia.Data;
using FormularioAsistencia.Models;
using Microsoft.AspNetCore.Identity;

namespace FormularioAsistencia.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly FormularioAsistenciaContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInMan;

     /*   public UsuariosController(UserManager<IdentityUser>userManager, SignInManager<IdentityUser> signInMan)
        {
            this.userManager = userManager;
            this.signInMan = signInMan;
        }
        [HttpGet]
        public IActionResult Create()
        {
         
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TipoUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Usuario.Correo, Email = model.Usuario.Correo };
                var result= await userManager.CreateAsync(user, model.Usuario.Contraseña);

                if (result.Succeeded)
                {
                    await signInMan.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("index", "home");

                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        */
        public UsuariosController(FormularioAsistenciaContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            //var tipos = from a in _context.Id_Tipo select a;

            // return View(await _context.Usuario.ToListAsync());
            return View(await _context.Usuario.Include(x=>x.Tipo).ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
     

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            Usuario model = new Usuario();
            var tipos = from a in _context.Id_Tipo select a;
            model.Tipos = tipos.ToList();
            /*TipoUsuarioViewModel model = new TipoUsuarioViewModel();
            var tipos = from a in _context.Id_Tipo select a;
            model.Tipos = tipos.ToList();*/
            return View(model);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,Correo,Contraseña,Comparacion,TipoUsuario")] Usuario usuario)
        {

            var emailExiste = _context.Usuario.Where(x => x.Correo == usuario.Correo).ToArray();
;
            if (emailExiste.Length > 0)
            {
                ModelState.AddModelError("Correo", "El correo electrónico ya está registrado.");
                return View(usuario);
            }
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,Correo,Contraseña,TipoUsuario")] Usuario usuario)
        {
            if (id != usuario.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.UserID == id);
        }

        /* prueba*/

        
    }
}
