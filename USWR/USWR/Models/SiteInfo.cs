using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USWR.Models
{
    public class SiteInfo
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Header { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
    }
}
