using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietTracker_UWP.BO
{
    public class User
    {
        public int userid { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public DateTime joinDate { get; set; }
        public String token { get; set; }
        public DateTime tokenExpires { get; set; }
    }
}

