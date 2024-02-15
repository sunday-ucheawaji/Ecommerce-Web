using AutoMapper;
using EcommerceWeb.Models.DTO.BillBoard;
using EcommerceWeb.Models.DTO.Product;
using EcommerceWeb.Repositories;
using EcommerceWeb.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillBoardController : ControllerBase
    {
        private readonly IBillBoardRepository _billboardRepository;
        private readonly IMapper _mapper;

        public BillBoardController(IBillBoardRepository billBoardRepository, IMapper mapper)
        {
            _billboardRepository = billBoardRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBillBoards()
        {
            var billBoards = await _billboardRepository.GetAllAsync();

            var billBoardDto = _mapper.Map<List<BillBoardDto>>(billBoards);

            var response = new ApiResponse<List<BillBoardDto>>
            {
                Status = true,
                Message = "Data retrieved successfully",
                Errors = null,
                Data = billBoardDto,

            };

            return Ok(response);
        }
    }
}
