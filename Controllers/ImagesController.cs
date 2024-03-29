﻿using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Image;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Staff")]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImagesController(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // Convert Dto to Domain model
                var imageDomainModel = new ProductImage
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                // User repository to upload image
                await _imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if(!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            // 10485760 is equivalent to 10MB
            if(request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }

        [HttpGet]
        [Route("GetAllImages")]
        public async Task<IActionResult> GetAllImages()
        {
            var imageDomainModel = await _imageRepository.GetAll();

            var imageDto = _mapper.Map<List<ProductImageDto>>(imageDomainModel);

            return Ok(imageDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetImageById([FromRoute] Guid id)
        {
            var imageDomainModel = await _imageRepository.GetById(id);
            var imageDto = _mapper.Map<ProductImageDto>(imageDomainModel);

            return Ok(imageDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateImage(Guid id, UpdateImageUploadDto updateImageUploadDto)
        {
            var imageDomainModel = _mapper.Map<ProductImage>(updateImageUploadDto);
            imageDomainModel = await _imageRepository.Update(id, imageDomainModel);

            var imageDto = _mapper.Map<ProductImageDto>(imageDomainModel);
            return Ok(imageDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var imageDomainModel = await _imageRepository.Delete(id);
            var imageDto = _mapper.Map<ProductImageDto>(imageDomainModel);
            return Ok(imageDto);
        }

    }
}
