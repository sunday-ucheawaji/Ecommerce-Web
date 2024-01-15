using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Cart;
using EcommerceWeb.Models.DTO.CartItem;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {

        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemsController(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cartItemsDomain = await _cartItemRepository.GetAllAsync();

            var cartItemDto = _mapper.Map<List<CartItemDto>>(cartItemsDomain);

            return Ok(cartItemDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetCartItem([FromRoute] Guid id)
        {
            var cartItemDomain = await _cartItemRepository.GetByIdAsync(id);

            if (cartItemDomain == null)
            {
                return NotFound();
            }

            var cartItemDto = _mapper.Map<CartItemDto>(cartItemDomain);

            return Ok(cartItemDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCartItem(AddCartItemDto addCartItemDto)
        {
            var cartItemDomain = _mapper.Map<CartItem>(addCartItemDto);
            cartItemDomain = await _cartItemRepository.CreateAsync(cartItemDomain);

            var cartItemDto = _mapper.Map<CartItemDto>(cartItemDomain);

            return CreatedAtAction(nameof(GetCartItem), new { id = cartItemDto.CartItemId }, cartItemDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateCartItem([FromRoute] Guid id, UpdateCartItemDto updateCartItemDto)
        {
            var cartItemDomain = await _cartItemRepository.UpdateAsync(id, updateCartItemDto.Quantity);

            if (cartItemDomain == null)
            {
                return NotFound();
            }

            var cartItemDto = _mapper.Map<CartItemDto>(cartItemDomain);

            return Ok(cartItemDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteCartItem([FromRoute] Guid id)
        {
            var cartItemDomain = await _cartItemRepository.DeleteAsync(id);

            if (cartItemDomain == null)
            {
                return NotFound();
            }

            var cartItemDto = _mapper.Map<CartItemDto>(cartItemDomain);

            return Ok(cartItemDto);

        }

    }
}
