using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace shopmodel.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int IdEmployee { get; set; }
        public int IdSaleReceipt { get; set; }



        [ForeignKey("IdEmployee")]
        public Employee Employee { get; set; }

        [ForeignKey("IdSaleReceipt")]
        public SaleReceipt SaleReceipt { get; set; }
    }
}
