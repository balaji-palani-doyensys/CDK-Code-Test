using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Core.Models
{
    public class Approval
    {
        public int Id { get; set; }

        public int Product_id { get; set; }

        public DateTime RequestedOn { get; set; }

        public Boolean IsApproved { get; set; }

        public int Action { get; set; }
    }
}
