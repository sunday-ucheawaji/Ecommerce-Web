﻿using AutoMapper;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Address;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {

        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IAddressRepository addressRepository, IMapper mapper, ILogger<AddressesController> logger)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles ="Customer, Staff, Supplier")]
        public async Task<IActionResult> GetAllAddress()
        {

            var addressDomain = await _addressRepository.GetAllAsync();

            var addressDto = _mapper.Map<List<AddressDto>>(addressDomain);

            return Ok(addressDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAddress([FromRoute] Guid id)
        {
            var addressDomain = await _addressRepository.GetByIdAsync(id);
            if (addressDomain == null)
            {
                return NotFound();
            }
            var addressDto = _mapper.Map<AddressDto>(addressDomain);

            return Ok(addressDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateAddress(AddAddressDto addAddressDto)
        {
            var addressDomain = _mapper.Map<Address>(addAddressDto);
            addressDomain = await _addressRepository.CreateAsync(addressDomain);

            var addressDto = _mapper.Map<AddressDto>(addressDomain);

            return CreatedAtAction(nameof(GetAddress), new { id = addressDto.AddressId }, addressDto);


        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAdress([FromRoute] Guid id, UpdateAddressDto updateAddressDto)
        {
            var addressDomain = _mapper.Map<Address>(updateAddressDto);
            addressDomain = await _addressRepository.UpdateAsync(id, addressDomain);
            if (addressDomain == null)
            {
                return NotFound();
            }

            var addressDto = _mapper.Map<AddressDto>(addressDomain);

            return Ok(addressDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] Guid id)
        {
            var addressDomain = await _addressRepository.DeleteAsync(id);
            if (addressDomain == null)
            {
                return NotFound();
            }

            var addressDto = _mapper.Map<AddAddressDto>(addressDomain);

            return Ok(addressDto);
        }
    }
}
