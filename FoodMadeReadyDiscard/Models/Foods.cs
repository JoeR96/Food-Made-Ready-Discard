using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMadeReadyDiscard.Models
{
    class Foods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefrostDuration { get; set; }
        public int ShelfLifeHours { get; set; }
       
    }
}
