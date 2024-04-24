﻿namespace BankAPI.CrossCutting.AppModelDtos
{
    public class AmountDTO
    {
        public string CardId { get; set; }
        public string TotalValue { get; set; }
        public string Currency { get; set; }
        public string AmountType { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public string TaxRate { get; set; }
        public string Concept { get; set; }
        public string Status { get; set; }
    }
}
