using System;

namespace Models
{
    public class PurchaseData
    {
        public DateOnly Date { get; set; }
        public decimal Price { get; set; }
        public int StoreID { get; set; }
    }
}
