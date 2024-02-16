using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Cart;
using EcommerceWeb.Models.DTO.Category;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categoryDomain = await _categoryRepository.GetAllAsync();

            var categoryDto = _mapper.Map<List<Category>>(categoryDomain);

            return Ok(categoryDto);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetCategory([FromRoute] Guid id)
        {
            var categoryDomain = await _categoryRepository.GetByIdAsync(id);

            if (categoryDomain == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<Category>(categoryDomain);

            return Ok(categoryDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCategory(AddCategoryDto addCategoryDto)
        {
            var categoryDomain = _mapper.Map<Category>(addCategoryDto);

            categoryDomain = await _categoryRepository.CreateAsync(categoryDomain);

            var categoryDto = _mapper.Map<CategoryDto>(categoryDomain);


            return CreatedAtAction(nameof(GetCategory), new { id = categoryDto.CategoryId }, categoryDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var categoryDomain = _mapper.Map<Category>(updateCategoryDto);
            categoryDomain = await _categoryRepository.UpdateAsync(id, categoryDomain);

            if (categoryDomain == null)
            {
                return NotFound();
            }
            var categoryDto = _mapper.Map<CategoryDto>(categoryDomain);

            return Ok(categoryDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var categoryDomain = await _categoryRepository.DeleteAsync(id);
            if (categoryDomain == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(categoryDomain);

            return Ok(categoryDto);
        }

    }
}
