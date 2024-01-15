using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.OrderDetailFolder;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        private readonly IMapper _mapper;
        public OrderDetailsController(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetailDomain = await _orderDetailRepository.getAllAsync();

            var orderDetailDto = _mapper.Map<List<OrderDetailDto>>(orderDetailDomain);

            return Ok(orderDetailDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetOrderDetail([FromRoute] Guid id)
        {
            var orderDetailDomain = await _orderDetailRepository.GetByIdAsync(id);
            if (orderDetailDomain == null)
            {
                return NotFound();
            }
            var orderDetailDomainDto = _mapper.Map<OrderDetailDto>(orderDetailDomain);

            return Ok(orderDetailDomainDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateOrderDetail(AddOrderDetailDto addOrderDetailDto)
        {
            var orderDetailDomain = _mapper.Map<OrderDetail>(addOrderDetailDto);
            if (orderDetailDomain == null)
            {
                return BadRequest();
            }
            orderDetailDomain = await _orderDetailRepository.CreateAsync(orderDetailDomain);

            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetailDomain);

            return CreatedAtAction(nameof(GetOrderDetail), new { id = orderDetailDto.OrderDetailId }, orderDetailDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateOrderDetail ([FromRoute] Guid id, UpdateOrderDetailDto updateOrderDetailDto)
        {
            var orderDetailDomain = _mapper.Map<OrderDetail>(updateOrderDetailDto);
            orderDetailDomain = await _orderDetailRepository.GetByIdAsync(id);
            if (orderDetailDomain == null)
            {
                return NotFound();
            }
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetailDomain);

            return Ok(orderDetailDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteOrderDetail([FromRoute] Guid id)
        {
            var orderDetailDomain = await _orderDetailRepository.DeleteAsync(id);
            if (orderDetailDomain == null)
            {
                return NotFound();
            }
            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetailDomain);
            return Ok(orderDetailDto);
        }
    }
}
