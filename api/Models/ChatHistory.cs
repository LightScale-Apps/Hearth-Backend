using System;
using System.DateTime;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("ChatHistory")]
    public class ChatHistory
    {
        public int Id { get; set; }

        public string OwnedBy {get; set;} = string.Empty;
        public string ChatName {get; set;} = string.Empty;

        public string Query {get; set;} = string.Empty;
        public string Response {get; set;} = string.Empty;
        public DateTime Timestamp {get; set;}
    }
}
