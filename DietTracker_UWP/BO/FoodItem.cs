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
        public double Protein { get; set; }
        public double Sugars { get; set; }
        public double Carbs { get; set; }
        public double Fiber { get; set; }
        public double Calories { get; set; }
        public double Fat { get; set; }
    }
}