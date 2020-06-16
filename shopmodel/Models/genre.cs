using System.Collections.Generic;


namespace shopmodel.Models
{
    public class Genre
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
