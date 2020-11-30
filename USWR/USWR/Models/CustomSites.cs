using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace USWR.Models
{
    public partial class CustomSites
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Header { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public int PositiveRating { get; set; }
        public int NegativeRating { get; set; }
        public string Comment { get; set; }
    }
}
