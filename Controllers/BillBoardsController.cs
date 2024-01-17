using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.BillBoard;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillBoardsController : ControllerBase
    {

        private readonly IBillBoardRepository _billboardRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public BillBoardsController(IBillBoardRepository billboardRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _billboardRepository = billboardRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllBillBoards()
        {
            var billBoardDomain = await _billboardRepository.GetAllAsync();

            var billBoardDto = _mapper.Map<List<BillBoardDto>>(billBoardDomain);

            return Ok(billBoardDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBillBoard([FromRoute] Guid id)
        {
            var billBoardDomain = await _billboardRepository.GetByIdAsync(id);
            if (billBoardDomain == null)
            {
                return NotFound();
            }

            var billBoardDto = _mapper.Map<BillBoardDto>(billBoardDomain);

            return Ok(billBoardDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateBillBoard(AddBillBoardDto addBillBoardDto)
        {
            
            var billBoardDomain = _mapper.Map<BillBoard>(addBillBoardDto);
            
            billBoardDomain = await _billboardRepository.CreateAsync(billBoardDomain);

            foreach (var productImg in addBillBoardDto.ProductImageIds)
            {
                var productImageDomain = new ProductImage { BillBoardId = billBoardDomain.BillBoardId };
                await _imageRepository.Update(productImg, productImageDomain);
            }

            var billBoardDto = _mapper.Map<BillBoardDto>(billBoardDomain);
            return CreatedAtAction(nameof(GetBillBoard), new { id = billBoardDto.BillBoardId }, billBoardDto);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBillBoard([FromRoute] Guid id, UpdateBillBoardDto updateBillBoardDto)
        {
            var billBoardDomain = _mapper.Map<BillBoard>(updateBillBoardDto);
            billBoardDomain = await _billboardRepository.UpdateAsync(id, billBoardDomain);

            if (billBoardDomain == null)
            {
                return NotFound();
            }

            if (updateBillBoardDto.ProductImageIds != null && updateBillBoardDto.ProductImageIds.Any())
            {

                foreach (var productImg in updateBillBoardDto.ProductImageIds)
                {
                    var productImageDomain = new ProductImage { BillBoardId = billBoardDomain.BillBoardId };
                    await _imageRepository.Update(productImg, productImageDomain);
                }
            }
           
            var billBoardDto = _mapper.Map<BillBoardDto>(billBoardDomain);
            return Ok(billBoardDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBillBoard([FromRoute] Guid id)
        {
            var billBoard = await _billboardRepository.DeleteAsync(id);
            if (billBoard == null)
            {
                return NotFound();
            }

            var billBoardDto = _mapper.Map<BillBoardDto>(billBoard);
            return Ok(billBoardDto);
        }
    }
}
