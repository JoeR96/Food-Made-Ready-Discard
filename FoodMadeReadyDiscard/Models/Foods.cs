using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodMadeReadyDiscard.Models
{
    class Foods
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("productCode")]
        public int ProductCode { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("defrostDuration")]
        public int DefrostDuration { get; set; }
        [JsonPropertyName("shelfLifeHours")]
        public int ShelfLifeHours { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }

    }


    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int id { get; set; }
        public int productCode { get; set; }
        public string name { get; set; }
        public int defrostDuration { get; set; }
        public int shelfLifeHours { get; set; }
        public string category { get; set; }
    }

}
