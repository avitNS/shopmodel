using System.Collections.Generic;


namespace shopmodel.Models
{
    public class MediaType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
