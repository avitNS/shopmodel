using System.Collections.Generic;


namespace shopmodel.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartActivity { get; set; }
        public string Desc { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
