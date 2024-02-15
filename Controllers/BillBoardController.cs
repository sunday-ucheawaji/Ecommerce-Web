using AutoMapper;
using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.BillBoard;
using EcommerceWeb.Models.DTO.Cart;
using EcommerceWeb.Models.DTO.Product;
using EcommerceWeb.Repositories;
using EcommerceWeb.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillBoardController : ControllerBase
    {
        private readonly IBillBoardRepository _billboardRepository;
        private readonly EcommerceWebDbContext dbContext;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public BillBoardController(IBillBoardRepository billBoardRepository, IMapper mapper, EcommerceWebDbContext dbContext, IImageRepository imageRepository)
        {
            _billboardRepository = billBoardRepository;
            _mapper = mapper;
            this.dbContext = dbContext;
            _imageRepository = imageRepository;
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBillBoard(Guid id)
        {
            var billBoard = await _billboardRepository.GetById(id);
            if (billBoard == null)
            {
                return NotFound();
            }
            var billBoardDto = _mapper.Map<BillBoardDto>(billBoard);
            return CreatedAtAction(nameof(GetBillBoard), new { id = billBoardDto.BillBoardId }, billBoardDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateBillBoard(AddBillBoardDto addBillBoardDto)
        {
            var billBoardDomain = _mapper.Map<BillBoard>(addBillBoardDto);
            billBoardDomain = await _billboardRepository.CreateAsync(billBoardDomain);


            var productImagesList = new List<ProductImage>();
            if (addBillBoardDto.ProductImageIds != null)
            {
                foreach (var productImageId in addBillBoardDto.ProductImageIds)
                {
                    var existingProductImage = await _imageRepository.UpdateBillBoardId(productImageId, billBoardDomain.BillBoardId);
                    if (existingProductImage != null)
                    {
                        productImagesList.Add(existingProductImage);
                    }
                }
                billBoardDomain.ProductImages = productImagesList;
                await dbContext.SaveChangesAsync();
            }

            var billBoardDto = _mapper.Map<BillBoardDto>(billBoardDomain);

            var response = new ApiResponse<BillBoardDto>
            {
                Status = true,
                Message = "Billboards Retrieved Successfull",
                Data = billBoardDto,
                Errors = null
            };

            return Ok(response);
        }
    }
}
