using System;
using System.Collections.Generic;

namespace USWR.Models
{
    public partial class Users
    {
        public Users()
        {
            Comments = new HashSet<Comments>();
            Ratings = new HashSet<Ratings>();
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual Roles Role { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Ratings> Ratings { get; set; }
    }
}
