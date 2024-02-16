﻿using EcommerceWeb.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.BillBoard
{
    public class AddBillBoardDto
    {

        [StringLength(50, MinimumLength = 3)]
        public string BillBoardName { get; set; }

        // Navigation Properties 

        public List<Guid> ProductImageIds { get; set; }

    }
}
