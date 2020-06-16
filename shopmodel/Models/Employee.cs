using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace shopmodel.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int IdEmpFunction { get; set; }




        [ForeignKey("IdEmpFunction")]
        public EmpFunction EmpFunction { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
