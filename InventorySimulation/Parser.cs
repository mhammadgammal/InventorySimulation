using InventoryModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySimulation
{
    internal class Parser
    {

        public static SimulationSystem ReadTestCase(String testCasePath)
        {
            SimulationSystem System = new SimulationSystem();
            Stream stream = File.Open(testCasePath, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Equals("OrderUpTo"))
                {
                    System.OrderUpTo = Int32.Parse(reader.ReadLine());
                }
                else if (line.Equals("ReviewPeriod"))
                {
                    System.ReviewPeriod = Int32.Parse(reader.ReadLine());
                }
                else if (line.Equals("StartLeadDays"))
                {
                    System.StartLeadDays = Int32.Parse(reader.ReadLine());
                }
                else if (line.Equals("StartOrderQuantity"))
                {
                    System.StartOrderQuantity = Int32.Parse(reader.ReadLine());
                }
                else if (line.Equals("NumberOfDays"))
                {
                    System.NumberOfDays = Int32.Parse(reader.ReadLine());
                }
                else if (line.Equals("DemandDistribution"))
                {
                    while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        line = line.Replace(" ", string.Empty);
                        string[] values = line.Split(',');
                        int demand = Int32.Parse((string)values[0]);
                        decimal prob = decimal.Parse(values[1]);
                        System.DemandDistribution.Add(new Distribution(demand, prob));  
                    }
                }
                else if (line.Equals("LeadDaysDistribution"))
                {
                    while (!String.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        line = line.Replace(" ", string.Empty);
                        string[] values = line.Split(',');
                        int demand = Int32.Parse((string)values[0]);
                        decimal prob = decimal.Parse(values[1]);
                        System.DemandDistribution.Add(new Distribution(demand, prob));
                    }
                }
            }

            return System;
        }
    }
}
