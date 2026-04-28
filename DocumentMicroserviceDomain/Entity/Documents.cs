using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMicroserviceDomain.Entity
{
    public class Documents
    {
        [Key]
        public int DocId { get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public string DocPath { get; set; }

        public int PolicyId { get; set; }
        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }


    }
}
