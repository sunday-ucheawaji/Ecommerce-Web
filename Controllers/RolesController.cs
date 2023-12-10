using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager) {
            this.roleManager = roleManager;
        }

        [HttpGet]
        [Route("SeedRoles")]
        public async Task<IActionResult> SeedRoles()
        {
            // Check if roles exist before creating
            if(!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (!await roleManager.RoleExistsAsync("Supplier"))
            {
                await roleManager.CreateAsync(new IdentityRole("Supplier"));
            }

            if (!await roleManager.RoleExistsAsync("Staff"))
            {
                await roleManager.CreateAsync(new IdentityRole("Staff"));
            }
            return Ok("Roles created Successfully");
        }
    }
}
