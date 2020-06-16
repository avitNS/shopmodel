using System.Collections.Generic;


namespace shopmodel.Models
{
    public class EmpFunction
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
