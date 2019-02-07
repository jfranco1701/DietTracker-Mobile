using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietTracker_UWP.BO
{
    public class Weight
    {
        public int id { get; set; }
        public int userid { get; set; }
        public double userweight { get; set; }
        public DateTime weightdate { get; set; }
        public DateTime timestamp { get; set; }
    }
}
