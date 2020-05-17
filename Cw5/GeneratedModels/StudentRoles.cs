using System;
using System.Collections.Generic;

namespace Cw5.GeneratedModels
{
    public partial class StudentRoles
    {
        public string IndexNumber { get; set; }
        public string RoleId { get; set; }

        public virtual Student IndexNumberNavigation { get; set; }
        public virtual Roles Role { get; set; }
    }
}
