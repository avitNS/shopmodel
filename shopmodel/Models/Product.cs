using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace shopmodel.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string DateAdd { get; set; }
        public decimal Price { get; set; }
        public int IdAlbum { get; set; }
        public int IdPublisher { get; set; }
        public int IdMediaType { get; set; }
        public int IdEnableStatus { get; set; }
        public int IdGenre { get; set; }



        [ForeignKey("IdAlbum")]
        public Album Album { get; set; }

        [ForeignKey("IdPublisher")]
        public Publisher Publisher { get; set; }

        [ForeignKey("IdMediaType")]
        public MediaType MediaType { get; set; }

        [ForeignKey("IdEnableStatus")]
        public EnableStatuses EnableStatuses { get; set; }

        [ForeignKey("IdGenre")]
        public Genre Genre { get; set; }

        public ICollection<SaleReceipt> SalesReceipts { get; set; }
    }
}
