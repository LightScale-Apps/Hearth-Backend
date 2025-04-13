using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("PatientData")]
    public class PatientData
    {
        public int Id { get; set; }
        public string Property {get; set;} = string.Empty;
        public string Value {get; set;} = string.Empty;
        public string OwnedBy {get; set;} = string.Empty;
    }
}