using System;
using System.Collections.Generic;

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
            for (int i = 0; i < DemandDistribution.Count; i++)
            {

                //Console.WriteLine("Deman Destribution Count = " + DemandDistribution.Count);
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
                DemandDistribution[i].MaxRange = Decimal.ToInt32(DemandDistribution[i].CummProbability * 100);
            }
        }

        public void CalculateLeadDaysCummulativeProbability()
        {
            for (int i = 0; i < LeadDaysDistribution.Count; i++)
            {
                if (i == 0)
                {
                    LeadDaysDistribution[i].CummProbability = LeadDaysDistribution[i].Probability;
                    LeadDaysDistribution[i].MaxRange = Decimal.ToInt32(LeadDaysDistribution[i].CummProbability * 100);
                    continue;

                }

                LeadDaysDistribution[i].CummProbability = LeadDaysDistribution[i - 1].CummProbability + LeadDaysDistribution[i].Probability;
                LeadDaysDistribution[i].MinRange = LeadDaysDistribution[i - 1].MaxRange + 1;
                LeadDaysDistribution[i].MaxRange = Decimal.ToInt32(LeadDaysDistribution[i].CummProbability * 100);


                if (LeadDaysDistribution[i].MinRange == LeadDaysDistribution[i].MaxRange)
                    LeadDaysDistribution[i].MinRange = 0;
            }
        }

        public void BuildSimulationTable()
        {
            int Cycle = 1;
            int DayWithinCycle = 1;
            int TotalEndInventory = 0;
            int TotalShortage = 0;
            Random random = new Random();   
            for (int i = 0; i < this.NumberOfDays; i++)
            {
                SimulationCase SCase = new SimulationCase(random);
                if (i == 0)
                {
                    SCase.costructCaseRow(this, new SimulationCase(), Cycle, DayWithinCycle);
                }
                else
                {
                    SCase.costructCaseRow(this, this.SimulationTable[i - 1], Cycle, DayWithinCycle);
                }
                
                this.SimulationTable.Add(SCase);
                
                if ((i + 1) % 5 == 0)
                {
                    DayWithinCycle = 1;
                    Cycle++;
                }
                else{ DayWithinCycle++; }
                TotalEndInventory += SCase.EndingInventory;
                TotalShortage += SCase.ShortageQuantity;
                PrintData(SCase);
            }
            PerformanceMeasures.EndingInventoryAverage = (TotalEndInventory / (decimal)this.NumberOfDays);
            PerformanceMeasures.ShortageQuantityAverage = (TotalShortage / (decimal)this.NumberOfDays);

        }

        private void PrintData(SimulationCase SCase)
        {
            Console.WriteLine("Day: " + SCase.Day);
            Console.WriteLine("Cycle: " + SCase.Cycle);
            Console.WriteLine("DayWithinCycle: " + SCase.DayWithinCycle);
            Console.WriteLine("BegginningInventory: " + SCase.BeginningInventory);
            Console.WriteLine("Demand: " + SCase.Demand);
            Console.WriteLine("EndingInventory: " + SCase.EndingInventory);
            Console.WriteLine("-------------------------------------------------");
        }

    }
}
