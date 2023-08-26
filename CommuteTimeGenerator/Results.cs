using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommuteTimeGenerator
{
    public class CommuteResults
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string IntermediateStation { get; set; }
        public Enum CommuteType { get; set; }
        public TimeSpan CycleOrWalkTime { get; set; }
        public TimeSpan TrainTime { get; set; }
        public TimeSpan TotalCommuteTime { get; set; }

        public CommuteResults(string origin, string destination, string intermediateStation, CommuteType commuteType, TimeSpan cyclingTime, TimeSpan trainTime)
        {
            Origin = origin;
            Destination = destination;
            IntermediateStation = intermediateStation;
            CommuteType = commuteType;
            CycleOrWalkTime = cyclingTime;
            TrainTime = trainTime;
            TotalCommuteTime = cyclingTime + trainTime;
        }   
    }
}
