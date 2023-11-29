using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DemandDistribution = new List<Distribution>();
            LeadDaysDistribution = new List<Distribution>();
            SimulationTable = new List<SimulationCase>();
            PerformanceMeasures = new PerformanceMeasures();
        }

        ///////////// INPUTS /////////////

        public int OrderUpTo { get; set; }
        public int ReviewPeriod { get; set; }
        public int NumberOfDays { get; set; }
        public int StartInventoryQuantity { get; set; }
        public int StartLeadDays { get; set; }
        public int StartOrderQuantity { get; set; }
        public List<Distribution> DemandDistribution { get; set; }
        public List<Distribution> LeadDaysDistribution { get; set; }

        ///////////// OUTPUTS /////////////

        public List<SimulationCase> SimulationTable { get; set; }
        public PerformanceMeasures PerformanceMeasures { get; set; }

        public void CalculateDemandCummulativeProbability()
        {
            for (int i = 0;  i < DemandDistribution.Count; i++)
            {
                
                if (i == 0)
                {
                    DemandDistribution[i].CummProbability = DemandDistribution[i].Probability;
                    DemandDistribution[i].MinRange = 1;
                }
                else
                {
                    DemandDistribution[i].CummProbability = DemandDistribution[i - 1].CummProbability + DemandDistribution[i].Probability;
                    DemandDistribution[i].MinRange = DemandDistribution[i - 1].MaxRange + 1;
                }
                DemandDistribution[i].MaxRange = Decimal.ToInt32(DemandDistribution[i - 1].CummProbability * 100);
            }
        }

        public void CalculateLeadDaysCummulativeProbability()
        {
            for (int i = 0; i < LeadDaysDistribution.Count; i++)
            {
                
                if (i == 0)
                {
                    LeadDaysDistribution[i].CummProbability = LeadDaysDistribution[i].Probability;
                    LeadDaysDistribution[i].MinRange = 1;
                }
                else
                {
                    LeadDaysDistribution[i].CummProbability = LeadDaysDistribution[i - 1].CummProbability + LeadDaysDistribution[i].Probability;
                    DemandDistribution[i].MinRange = DemandDistribution[i - 1].MaxRange + 1;
                }
                LeadDaysDistribution[i].MaxRange = Decimal.ToInt32(LeadDaysDistribution[i - 1].CummProbability * 100);
            }
        }

    }
}
