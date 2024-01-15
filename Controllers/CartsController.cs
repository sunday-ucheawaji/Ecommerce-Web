using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Cart;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {

        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartsController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCarts()
        {
            var cartDomain = await _cartRepository.GetAllAsync();

            var cartDto = _mapper.Map<List<CartDto>>(cartDomain);

            return Ok(cartDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCart([FromRoute] Guid id)
        {
            var cartDomain = await _cartRepository.GetByIdAsync(id);

            if (cartDomain == null)
            {
                return NotFound();
            }

            var cartDto = _mapper.Map<CartDto>(cartDomain);

            return Ok(cartDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCart()
        {
            var cartDomain = new Cart();
            cartDomain = await _cartRepository.CreateAsync(cartDomain);

       
            var cartDto = _mapper.Map<CartDto>(cartDomain);

            return CreatedAtAction(nameof(GetCart), new { id = cartDto.CartId }, cartDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCart([FromRoute] Guid id, UpdateCartDTo updateCartDTo)
        {

            var cartDomain = _mapper.Map<Cart>(updateCartDTo);

            cartDomain = await _cartRepository.UpdateAsync(id, cartDomain);

            if (cartDomain == null)
            {
                return NotFound();
            }

            var cartDto = _mapper.Map<CartDto>(cartDomain);

            return Ok(cartDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCart([FromRoute] Guid id)
        {
            var cartDomain = await _cartRepository.DeleteAsync(id);

            if (cartDomain == null)
            {
                return NotFound();
            }

            var cartDto = _mapper.Map<CartDto>(cartDomain);

            return Ok(cartDto);
        }
    }
}
