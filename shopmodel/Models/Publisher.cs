using System.Collections.Generic;


namespace shopmodel.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public ICollection<Product> Products { get; set; }

    }
}
