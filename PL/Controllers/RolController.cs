using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    //[Authorize(Roles = "Administrador, Usuario")]
    public class RolController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RolController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        [HttpGet]
        public IActionResult GetAll() 
        { 
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]

        public IActionResult Asignar(Guid idRole, string name)
        {
            ML.Result result = BL.IdentityUser.GetAll(idRole);
            ML.UserIdentity user = new ML.UserIdentity();
            if (result.Correct)
            {
                user.IdentityUsers = result.Objects;
            }
            user.Rol = new ML.Rol();
            user.Rol.Name = name;
            user.Rol.RoleId = idRole;

            return View(user);
        }

        [HttpPost]
        public IActionResult Asignar(ML.UserIdentity user)
        {
            ML.Result result = BL.IdentityUser.Asignar(user);
            if (result.Correct)
            {
                ViewBag.Message = "Se ha asignado correctamente";

            }
            else
            {
                ViewBag.Message = result.Message;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult Form(Guid idRole, string name)
        {
            IdentityRole role = new IdentityRole();

            if (name != null)
            {
                role.Id = idRole.ToString();
                return View(role);
            }
            else
            {
                return View(role);
            }
        }

        [HttpPost] 
        public async Task<IActionResult> Form([Required] IdentityRole rol)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await roleManager.FindByIdAsync(rol.Id.ToString());

                if (role == null)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                }
                else
                {
                    role.Id = await roleManager.GetRoleIdAsync(rol);
                    role.Name = await roleManager.GetRoleNameAsync(rol);

                    IdentityResult result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("GetAll");
                    }
                }
                 
            }

            return View(rol);
        }

        public async Task<IActionResult> Delete(Guid IdRole)
        {
            IdentityRole role = await roleManager.FindByIdAsync(IdRole.ToString());
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("GetAll");
                //else
                //    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("GetAll", roleManager.Roles);
        }
    }
}
