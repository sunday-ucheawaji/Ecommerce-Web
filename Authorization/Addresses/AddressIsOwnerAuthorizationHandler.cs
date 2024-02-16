using EcommerceWeb.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace EcommerceWeb.Authorization.Addresses
{
    public class AddressIsOwnerAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Address>
    {
        UserManager<CustomUser> _userManager;

        public AddressIsOwnerAuthorizationHandler(UserManager<CustomUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Address resource)
        {
            context.Succeed(requirement);
            Console.WriteLine(context.User);
            Console.WriteLine(requirement.Name);
            Console.WriteLine(resource.City);

            return Task.CompletedTask;
        }
    }
}
