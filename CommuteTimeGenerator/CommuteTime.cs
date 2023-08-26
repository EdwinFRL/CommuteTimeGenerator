using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CommuteTimeGenerator
{
    public static class CommuteTime
    {
        public static List<CommuteResults> TotalCommuteTime(string origin, string destination, CommuteType commuteType, string key)
        {
            
            var trainStations = new List<string>()
            {
                "Manly+station",
                "Wynnum+Central+station",
                "Wynnum+station",
                "Wynnum+North+station",
                "Lindum+station"
            };

            var totalCommuteResults = new List<CommuteResults>();

            for (int i = 0; i < trainStations.Count; i++)
            {
                var trainTime = TrainDirections(key, trainStations[i], destination);
                var cyclingTime = CyclingDirections(key, origin, trainStations[i], commuteType);
                totalCommuteResults.Add(new CommuteResults(origin, destination, trainStations[i], commuteType, cyclingTime, trainTime));
            }
            
            return totalCommuteResults;
        }

        public static TimeSpan TrainDirections(string key, string originStation, string destination)
        {
            if (destination == "Brisbane+Central+Station")
            {
                var time = TimeSpan.Zero;
                switch (originStation)
                {
                    case "Manly+station":
                        time = TimeSpan.FromMinutes(43);
                        break;
                    case "Wynnum+Central+station":
                        time = TimeSpan.FromMinutes(41);
                        break;
                    case "Wynnum+station":
                        time = TimeSpan.FromMinutes(39);
                        break;
                    case "Wynnum+North+station":
                        time = TimeSpan.FromMinutes(37);
                        break;
                    case "Lindum+station":
                        time = TimeSpan.FromMinutes(34);
                        break;
                    default:
                        time = TimeSpan.Zero;
                        break;
                }
                return time;
            }
            else
            {
                return TimeSpan.Zero;
            }
            
        }

        public static TimeSpan CyclingDirections(string key, string origin, string destination, CommuteType commuteType)
        {
            string url = "";
            if (commuteType == CommuteType.Cycling)
            {
                url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&mode=bicycling&key={key}";
            }
            else if (commuteType == CommuteType.Walking)
            {
                url = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&mode=walking&key={key}";
            }
            
            var request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string responseFromServer = reader.ReadToEnd();
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var track = JsonConvert.DeserializeObject<Direction>(responseFromServer, settings);
            int timeValue = track.Routes[0].Legs[0].Duration.Value;
            response.Close();

            return TimeSpan.FromSeconds(timeValue);
        }



    }
}
