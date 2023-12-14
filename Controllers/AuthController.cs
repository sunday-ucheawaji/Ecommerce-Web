using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Auth;
using EcommerceWeb.Models.DTO.Customer;
using EcommerceWeb.Models.DTO.Staff;
using EcommerceWeb.Models.DTO.Supplier;
using EcommerceWeb.Repositories;
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
        private readonly ISupplierRepository supplierRepository;
        private readonly IStaffRepository staffRepository;
        private readonly IMapper mapper;

        public AuthController(
            UserManager<CustomUser> userManager, 
            ITokenRepository tokenRepository, 
            ICustomerRepository customerRepository, 
            ISupplierRepository supplierRepository, 
            IStaffRepository staffRepository,
            IMapper mapper
            ) {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.customerRepository = customerRepository;
            this.supplierRepository = supplierRepository;
            this.staffRepository = staffRepository;
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

        [HttpPost]
        [Route("RegisterSupplier")]
        public async Task<IActionResult> RegisterSupplier([FromBody] RegisterSupplierDto registerSupplierDto)
        {
            var identityUser = new CustomUser
            {
                UserName = registerSupplierDto.Email,
                Email = registerSupplierDto.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerSupplierDto.Password);
            if (identityResult.Succeeded)
            {
                var roles = new List<string> { "Supplier" };
                identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                if (identityResult.Succeeded)
                {
                    var supplierIdentityUser = new Supplier
                    {
                        CompanyName = registerSupplierDto.CompanyName,
                        ContactName = registerSupplierDto.ContactName,
                        CustomUserId = identityUser.Id
                    };

                    var supplierDomainModel = await supplierRepository.CreateAsync(supplierIdentityUser);
                    var supplierDto = mapper.Map<SupplierDto>(supplierDomainModel);
                    return Ok(supplierDto);
                }

            }
            return BadRequest("Something went wrong!");

        }


        [HttpPost]
        [Route("RegisterStaff")]
        public async Task<IActionResult> RegisterStaff([FromBody] RegisterStaffDto registerStaffDto)
        {
            var identityUser = new CustomUser
            {
                Email = registerStaffDto.Email,
                UserName = registerStaffDto.Email,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerStaffDto.Password);
            if (identityResult.Succeeded)
            {
                var roles = new List<string> { "Staff" };

                identityResult = await userManager.AddToRolesAsync(identityUser, roles);

                if (identityResult.Succeeded)
                {
                    var staffIdentityUser = new Staff
                    {
                        FirstName = registerStaffDto.FirstName,
                        LastName = registerStaffDto.LastName,
                        Department = registerStaffDto.Department,
                        OfficePhone = registerStaffDto.OfficePhone,
                        Position = registerStaffDto.Position,
                        CustomUserId = identityUser.Id
                    };

                    var staffDomainModel = await staffRepository.CreateAsync(staffIdentityUser);

                    var staffDto = mapper.Map<StaffDto>(staffDomainModel); 

                    return Ok(staffDto);
                }
            }
            return BadRequest("Something went wrong!");
        }



        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (checkPasswordResult)
                {
                    // Get roles of the user
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        // Create token
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or Password incorrect");
        }

    }
}
