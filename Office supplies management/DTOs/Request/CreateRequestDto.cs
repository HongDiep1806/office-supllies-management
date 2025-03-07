﻿
using Office_supplies_management.DTOs.ProductRequest;

namespace Office_supplies_management.DTOs.Request

{
    public class CreateRequestDto
    {
        public int TotalPrice { get; set; }
        public string RequestCode { get; set; }
        public int UserID { get; set; }
        public List<ProductRequestDto> Products { get; set; }
    }
}
