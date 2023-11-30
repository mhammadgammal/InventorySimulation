using System;
using System.Collections.Generic;

namespace InventoryModels
{
    public class SimulationCase
    {
        public SimulationCase()
        {

        }
        private static int NoOfDay = 1;
        private static int LeadTime = 0;
        private static int OldBegginingInventory = 1;
        public int Day { get; set; }
        public int Cycle { get; set; }
        public int DayWithinCycle { get; set; }
        public int BeginningInventory { get; set; }
        public int RandomDemand { get; set; }
        public int Demand { get; set; }
        public int EndingInventory { get; set; }
        public int ShortageQuantity { get; set; }
        public int OrderQuantity { get; set; }
        public int RandomLeadDays { get; set; }
        public int LeadDays { get; set; }

        public SimulationCase MapRandomDemand(List<Distribution> DemandDistribution)
        {
            Random random = new Random();
            this.RandomDemand = random.Next(1, 100);
            //If this doesn't work, Go to default apraoach
            // If..else
            for (int i = 0; i < DemandDistribution.Count; i++)
            {
                if (this.RandomDemand >= DemandDistribution[i].MinRange && this.RandomDemand <= DemandDistribution[i].MaxRange)
                {
                    this.Demand = i;
                }

            }
            return this;
        }

        public SimulationCase MapRandomLeadDays(List<Distribution> LeadDaysDistribution)
        {
            Random random = new Random();
            this.RandomLeadDays = random.Next(1, 100);
            //If this doesn't work, Go to default apraoach
            // If..else
            for (int i = 0; i < LeadDaysDistribution.Count; i++)
            {
                if (this.RandomLeadDays >= LeadDaysDistribution[i].MinRange && this.RandomLeadDays <= LeadDaysDistribution[i].MaxRange)
                {
                    this.LeadDays = i;
                }

            }
            return this;
        }

        public void costructCaseRow(SimulationSystem Sys, int Cycle, int DayWithinCycle )
        {
            Sys.CalculateDemandCummulativeProbability();
            Sys.CalculateLeadDaysCummulativeProbability();
            this.Day = NoOfDay;
            this.Cycle = Cycle;
            this.DayWithinCycle = DayWithinCycle;
            MapRandomDemand(Sys.DemandDistribution);
            Console.WriteLine(Sys.StartInventoryQuantity);
            this.BeginningInventory = _BeginningInventory(Sys.StartInventoryQuantity);
            this.EndingInventory = this.Demand - this.BeginningInventory;
            EndingInventoryAndShortage();
            CheckIfOrder(Sys.OrderUpTo);

        }

        private void CheckIfOrder(int UpToLevel)
        {
            if (this.DayWithinCycle == 5)
            {
                this.OrderQuantity = UpToLevel - this.EndingInventory + this.ShortageQuantity;
            }
        }

        private void EndingInventoryAndShortage()
        {
            var DemandResult = this.BeginningInventory - this.Demand;
            if (DemandResult > 0)
            {
                this.EndingInventory = DemandResult;
                this.ShortageQuantity = 0;
            }
            else
            {
                this.EndingInventory = 0;
                
                this.ShortageQuantity = Math.Abs(DemandResult);
            }
            OldBegginingInventory = this.EndingInventory;
        }

        private int _BeginningInventory(int StartInventoryQuantity)
        {
            if (this.Day != 1)
            {
                return OldBegginingInventory;
            }
            else
            {
                return StartInventoryQuantity;
            }
        }
    }
}
