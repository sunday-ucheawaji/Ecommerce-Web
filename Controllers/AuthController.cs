using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Auth;
using EcommerceWeb.Models.DTO.Customer;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public AuthController(UserManager<CustomUser> userManager, ITokenRepository tokenRepository, ICustomerRepository customerRepository, IMapper mapper) {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerDto registerCustomerDto)
        {
            var identityUser = new CustomUser
            {
                UserName = registerCustomerDto.Email,
                Email = registerCustomerDto.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerCustomerDto.Password);

            if (identityResult.Succeeded)
            {
                // Add Customer Role to this user
                var roles = new List<string> { "Customer" };

                identityResult = await userManager.AddToRolesAsync(identityUser, roles);
                if (identityResult.Succeeded)
                {
                    var customerIdentityUser = new Customer
                    {
                        FirstName = registerCustomerDto.FirstName,
                        LastName = registerCustomerDto.LastName,
                        DateOfBirth = registerCustomerDto.DateOfBirth,
                        CustomUserId = identityUser.Id
                    };

                    var customerDomainModel = await customerRepository.CreateAsync(customerIdentityUser);
                    var customerDto = mapper.Map<CustomerDto>(customerDomainModel);
                    return Ok(customerDto);
                }
            }
            return BadRequest("Something went wrong!");
        }
    }
}
