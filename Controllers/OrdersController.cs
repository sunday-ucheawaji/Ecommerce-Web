using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Cart;
using EcommerceWeb.Models.DTO.Order;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository orderRepository, IMapper mapper, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllOrders()
        {
            var ordersDomain = await _orderRepository.getAllAsync();

            var orderDto = _mapper.Map<List<OrderDto>>(ordersDomain); 
            
            return Ok(orderDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var ordersDomain = await _orderRepository.GetByIdAsync(id);

            if (ordersDomain == null)
            {
                return NotFound();
            }

            var orderDto = _mapper.Map<CartDto>(ordersDomain);

            return Ok(orderDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateOrder(AddOrderDto addOrderDto)
        {
            var orderDomain = new Order {CustomerId= addOrderDto.CustomerId};
            orderDomain = await _orderRepository.CreateAsync(orderDomain);

            if ( addOrderDto.CartItems != null)
            {
                
                foreach (var orderItem in addOrderDto.CartItems)
                {
                    var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                    if (product != null)
                    {
                        var orderDetail = new OrderDetail
                        {
                            ProductId = orderItem.ProductId,
                            UnitPrice = product.Price,
                            Quantity = orderItem.Quantity,
                            OrderId = orderDomain.OrderId
                        };
                        await _orderDetailRepository.CreateAsync(orderDetail);
                    }
                }
                orderDomain = await _orderRepository.GetByIdAsync(orderDomain.OrderId);
                //var orderDto = _mapper.Map<OrderDto>(orderDomain);
                var orderDto = new OrderDto
                {
                    OrderId = orderDomain.OrderId,
                    PlacedAt = orderDomain.PlacedAt,
                    PaymentStatus = (OrderDto.PaymentStatusEnum)orderDomain.PaymentStatus,
                    CustomerId = orderDomain.CustomerId,
                };

                return CreatedAtAction(nameof(GetOrder), new { id = orderDto.OrderId }, orderDto);

            }
            return BadRequest();
        }

    }
}
 