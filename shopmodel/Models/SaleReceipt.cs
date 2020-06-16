using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace shopmodel.Models
{
    public class SaleReceipt
    {
        public int Id { get; set; }
        public int ProdQuantity { get; set; }
        public decimal Total { get; set; }
        public string DateReceipt { get; set; }
        public int IdProduct { get; set; }



        [ForeignKey("IdProduct")]
        public Product Product { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
