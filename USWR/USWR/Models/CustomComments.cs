using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USWR.Models
{
    public partial class CustomComments
    {
        public Guid Id { get; set; }
        public string Header { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }
        public string Login { get; set; }

    }
}
