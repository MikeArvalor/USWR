using System;
using System.Collections.Generic;

namespace USWR.Models
{
    public partial class Sites
    {
        public Sites()
        {
            Comments = new HashSet<Comments>();
            Ratings = new HashSet<Ratings>();
        }

        public Guid Id { get; set; }
        public string Link { get; set; }
        public string Header { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
