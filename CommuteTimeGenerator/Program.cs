// See https://aka.ms/new-console-template for more information
using CommuteTimeGenerator;
using System.Security.AccessControl;
using System.Linq;

var shortestRoutes = new List<CommuteResults>();
string directory = "C:\\Users\\edwins\\source\\repos\\CommuteTimeGenerator\\MapData";
string key = "x"; //insert personal Google API key

//Input
CommuteType type = CommuteType.Walking;
var coordinates = FunctionSet.BuildDataTableFromCSV(directory, "coordinates.csv");

//int count = coordinates.Rows.Count - 1;
int count = 2;
for (int i = 1; i < count; i++)
{
    if(i % 10 == 0)
    {
        Console.WriteLine($"Location {i} of {coordinates.Rows.Count}");
    }

    string latString = (string)coordinates.Rows[i][0];
    string lonString = (string)coordinates.Rows[i][1];
    Double.TryParse(latString, out double lat);
    Double.TryParse(lonString, out double lon);

    //Output
    string location = $"{lat},{lon}";
    var results = CommuteTime.TotalCommuteTime(location, "Brisbane+Central+Station", type, key);
    CommuteResults shortestRoute = results.OrderBy(t => t.TotalCommuteTime).First();

    shortestRoutes.Add(shortestRoute);
}

FunctionSet.WriteToCSV(shortestRoutes, directory);
