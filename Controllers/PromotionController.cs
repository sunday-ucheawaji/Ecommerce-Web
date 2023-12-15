using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Promotion;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionRepository promotionRepository;
        private readonly IMapper mapper;

        public PromotionController(IPromotionRepository promotionRepository, IMapper mapper)
        {
            this.promotionRepository = promotionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("promotions")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> GetAllPromotion()
        {
            var promotionsDomainModel = await promotionRepository.GetAllAsync();
            var promotionDto = mapper.Map<List<PromotionDto>>(promotionsDomainModel);

            return Ok(promotionDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Staff")]

        public async Task<IActionResult> GetPromotion([FromRoute] Guid id)
        {
            var promotion = await promotionRepository.GetByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            var promotionDto = mapper.Map<PromotionDto>(promotion); 

            return Ok(promotionDto);
        }

        [HttpPost]
        [Route("promotions")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreatePromotion([FromBody] AddPromotionDto addPromotionDto)
        {
            var promotionDomainModel = mapper.Map<Promotion>(addPromotionDto);
            promotionDomainModel = await promotionRepository.CreateAsync(promotionDomainModel);
            var promotionDto = mapper.Map<PromotionDto>(promotionDomainModel);

            return CreatedAtAction(nameof(GetPromotion), new { id = promotionDto.PromotionId }, promotionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdatePromotion([FromBody] UpdatePromotionDto updatePromotionDto, [FromRoute] Guid id)
        {
            var promotionDomainModel = mapper.Map<Promotion>(updatePromotionDto);
            promotionDomainModel = await promotionRepository.UpdateAsync(id, promotionDomainModel);
            if (promotionDomainModel == null)
            {
                return NotFound();
            }
            var promotionDto = mapper.Map<PromotionDto>(promotionDomainModel);
            return Ok(promotionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> DeletePromotion([FromRoute] Guid id)
        {
            var promotioDomainModel = await promotionRepository.DeleteAsync(id);
            if(promotioDomainModel == null)
            {
                return NotFound();
            }

            var promotionDto = mapper.Map<PromotionDto>(promotioDomainModel);
           
            return Ok(promotionDto);

        }
    }
}
