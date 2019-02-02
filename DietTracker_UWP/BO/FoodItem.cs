using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietTracker_UWP.BO
{
    public class FoodItem
    {
        public int NDBNum { get; set; }
        public String FoodName { get; set; }
        public String Manufacturer { get; set; }
        public String Serving { get; set; }
        public int Protein { get; set; }
        public int Sugars { get; set; }
        public int Carbs { get; set; }
        public int Fiber { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
    }
}
