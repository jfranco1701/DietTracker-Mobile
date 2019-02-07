using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietTracker_UWP.BO
{
    public class Favorite
    {
        public int id { get; set; }
        public int userid { get; set; }
        public String foodname { get; set; }
        public double calories { get; set; }
        public double protein { get; set; }
        public double fat { get; set; }
        public double fiber { get; set; }
        public double carbs { get; set; }
        public double sugars { get; set; }
        public String measure { get; set; }
        public DateTime timestamp { get; set; }
    }
}
