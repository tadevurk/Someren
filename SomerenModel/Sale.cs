using System;

namespace SomerenModel
{
    public class Sale
    {
        public int DrinkId { get; set; }
        public int StudentId { get; set; }
        public int SaleId { get; set; }
        public DateTime SaleDate { get; set; }

        public int SaleCount { get; set; }

        public decimal TotalPayment { get; set; }

    }
}
