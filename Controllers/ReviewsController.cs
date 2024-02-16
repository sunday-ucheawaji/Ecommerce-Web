using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Review;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewsController(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviewDomain = await _reviewRepository.GetAllAsync();

            var reviewDto = _mapper.Map<List<ReviewDto>>(reviewDomain);

            return Ok(reviewDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetReview([FromRoute] Guid id)
        {
            var reviewDomain = await _reviewRepository.GetByIdAsync(id);
            if (reviewDomain == null)
            {
                return NotFound();
            }

            var reviewDomainDto = _mapper.Map<ReviewDto>(reviewDomain);

            return Ok(reviewDomainDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateReview(AddReviewDto addReviewDto)
        {
            var reviewDomain = _mapper.Map<Review>(addReviewDto);

            reviewDomain = await _reviewRepository.CreateAsync(reviewDomain);

            var reviewDto = _mapper.Map<ReviewDto>(reviewDomain);

            return CreatedAtAction(nameof(GetReview), new { id = reviewDto.ReviewId}, reviewDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateReview([FromRoute] Guid id, UpdateReviewDto updateReviewDto)
        {
            var reviewDomain = _mapper.Map<Review>(updateReviewDto);

            reviewDomain = await _reviewRepository.UpdateAsync(id, reviewDomain);

            if (reviewDomain == null)
            {
                return NotFound();
            }

            var reviewDto = _mapper.Map<ReviewDto>(reviewDomain);

            return Ok(reviewDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteReview([FromRoute,] Guid id)
        {
            var reviewDomain = await _reviewRepository.DeleteAsync(id);
            if (reviewDomain == null)
            {
                return NotFound();
            }

            var reviewDto = _mapper.Map<ReviewDto>(reviewDomain);

            return Ok(reviewDto);

        }

    }
}
