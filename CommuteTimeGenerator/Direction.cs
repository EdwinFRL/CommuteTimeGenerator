using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CommuteTimeGenerator
{
    public class Direction
    {
        /// <summary>
        /// A User's name. eg: "Sergio Tapia, John Cosack, Lucy McMillan"
        /// </summary>
        [JsonProperty("routes")]
        public Routes[] Routes { get; set; }

    }
    public class Routes
    {
        [JsonProperty("legs")]
        public Legs[] Legs { get; set; }

    }

    public class Legs
    {
        [JsonProperty("duration")]
        public Duration Duration { get; set; }
    }

    public class Duration
    {
        [JsonProperty("value")]
        public int Value { get; set; }
        

    }
}
