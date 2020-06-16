using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace shopmodel.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateCreation { get; set; }

        public int IdArtist { get; set; }



        [ForeignKey("IdArtist")]
        public Artist Artist { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
