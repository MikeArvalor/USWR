using System;
using System.Collections.Generic;

namespace USWR.Models
{
    public partial class Ratings
    {
        public Guid Id { get; set; }
        public Guid SiteId { get; set; }
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }

        public virtual Sites Site { get; set; }
        public virtual Users User { get; set; }
    }
}
