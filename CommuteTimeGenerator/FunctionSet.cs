using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommuteTimeGenerator
{
    public static class FunctionSet
    {
        public static DataTable BuildDataTableFromCSV(string directory, string fileName)
        {
            DataTable tableFromCSV = new DataTable(fileName.Substring(0, fileName.Length - 4));
            DataRow myDataRow;

            StreamReader tr = new StreamReader(directory  + "\\" + fileName);
            string fullText = tr.ReadToEnd().ToString();
            string[] allRowsSplit = fullText.Split('\n'); //split full file text into rows

            var firstRow = allRowsSplit[0].Split(','); //split each row with comma to get individual values

            //add headers
            for (int j = 0; j < firstRow.Count(); j++)
            {
                tableFromCSV.Columns.Add(firstRow[j]);
            }

            //add values
            for (int i = 0; i < allRowsSplit.Count(); i++)
            {
                string[] rowValues = allRowsSplit[i].Split(',');
                {
                    myDataRow = tableFromCSV.NewRow();
                    for (int k = 0; k < rowValues.Count(); k++)
                    {
                        myDataRow[k] = rowValues[k].ToString();
                    }
                    tableFromCSV.Rows.Add(myDataRow);
                }
            }

            return tableFromCSV;
        }

        public static void WriteToJSon(List<CommuteResults> results, string path)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            var jsonString = JsonConvert.SerializeObject(results, settings);

            File.WriteAllText(path + "\\results.json", jsonString);
        }

        public static void WriteToCSV(List<CommuteResults> results, string outputDir)
        {
            if (outputDir != null)
            {
                var csv = new StringBuilder();
                string header = "";
                var properties = typeof(CommuteResults).GetProperties();
                foreach (var prop in properties)
                {
                    header = $"{header}{prop.Name.ToString()},";
                }
                header = header.Substring(0, header.Length - 1); //remove last comma
                csv.AppendLine(header);

                //Add results
                foreach (var res in results)
                {
                    string newLine = "";
                    foreach (var prop in properties)
                    {
                        string valueString = $"{prop.GetValue(res)}";
                        if (prop.Name.ToString() == "Origin")
                        {
                            if (valueString.Contains(","))
                            {
                                valueString = valueString.Replace(",", ";");
                            }
                        }

                        newLine = $"{newLine}{valueString},";
                    }
                    newLine = newLine.Substring(0, newLine.Length - 1); //remove last comma
                    csv.AppendLine(newLine);
                }

                if (Directory.Exists(outputDir))
                {
                    string filePath = outputDir + $@"\results.csv";
                    File.WriteAllText(filePath, csv.ToString());
                }
            }
        }
    }
}
