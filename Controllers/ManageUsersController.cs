using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ShareALittle.Models;
using Microsoft.EntityFrameworkCore;

namespace ShareALittle.Controllers
{

    public class ManageUsersController : Controller
    {
        private readonly ShareALittleContext _context;

        private readonly UserManager<ApplicationUser>
            _userManager;

        public ManageUsersController(
            UserManager<ApplicationUser> userManager, ShareALittleContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager
                .GetUsersInRoleAsync("Admin"))
                .ToArray();

            var everyone = await _userManager.Users
                .ToArrayAsync();

            var model = new ManageUsersViewModel
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id != null)
                {

                    var user = await _userManager.FindByIdAsync(id);
                    var rolesForUser = await _userManager.GetRolesAsync(user);

                    using (var transaction = _context.Database.BeginTransaction())
                    {

                        if (rolesForUser.Count() > 0)
                        {
                            foreach (var item in rolesForUser.ToList())
                            {
                                // item should be the name of the role
                                var result = await _userManager.RemoveFromRoleAsync(user, item);
                            }
                        }

                        await _userManager.DeleteAsync(user);
                        transaction.Commit();
                    }

                    return RedirectToAction("Index");
                }
            }

            return View();

        }

    }
}
