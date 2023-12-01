using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace InventoryModels
{
    public class SimulationCase
    {
        public SimulationCase(Random random)
        {
            this.random = random;
        }
        public SimulationCase() {}
        private Random random;
        private static int NoOfDay = 1;
        public static int DaysUntilOrderArrives = -2;
        private static int OldBegginingInventory = 1;
        private static int OderQuantity = 0;
        private static int Shortage = 0;
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
        public int DaysUntilArrive { get; set; }
        public SimulationCase MapRandomDemand(List<Distribution> DemandDistribution)
        {

            //If this doesn't work, Go to default apraoach
            // If..else
            this.RandomDemand = random.Next(1, 100);
            Console.WriteLine(DemandDistribution.Count);
            for (int i = 0; i < DemandDistribution.Count; i++)
            {
                Console.WriteLine("Before condition: " + this.RandomDemand);
                Console.WriteLine("Demand Distribution min range: " + DemandDistribution[i].MinRange);
                Console.WriteLine("Demand Distribution max range: " + DemandDistribution[i].MaxRange);
                Console.WriteLine("---------------------------------------");
                if (this.RandomDemand >= DemandDistribution[i].MinRange && this.RandomDemand <= DemandDistribution[i].MaxRange)
                {
                    Console.WriteLine("After condition: " + this.RandomDemand);
                    Console.WriteLine("Demand Distribution min range: " + DemandDistribution[i].MinRange);
                    Console.WriteLine("Demand Distribution max range: " + DemandDistribution[i].MaxRange);
                    this.Demand = DemandDistribution[i].Value;
                    break;
                }

            }
            return this;
        }

        public SimulationCase MapRandomLeadDays(List<Distribution> LeadDaysDistribution)
        {
            this.RandomLeadDays = random.Next(1, 100);
            //If this doesn't work, Go to default apraoach
            // If..else
            for (int i = 0; i < LeadDaysDistribution.Count; i++)
            {
                if (this.RandomLeadDays >= LeadDaysDistribution[i].MinRange && this.RandomLeadDays <= LeadDaysDistribution[i].MaxRange)
                {
                    this.LeadDays = LeadDaysDistribution[i].Value;
                    DaysUntilOrderArrives = this.LeadDays;
                    this.DaysUntilArrive = DaysUntilOrderArrives;
                }

            }
            return this;
        }

        public void costructCaseRow(SimulationSystem Sys, int Cycle, int DayWithinCycle)
        {
            Sys.CalculateDemandCummulativeProbability();
            Sys.CalculateLeadDaysCummulativeProbability();
            
            this.Day = NoOfDay;
            this.Cycle = Cycle;
            this.DayWithinCycle = DayWithinCycle;
            MapRandomDemand(Sys.DemandDistribution);
            this.BeginningInventory = _BeginningInventory(Sys.StartInventoryQuantity);
            EndingInventoryAndShortage();
            CheckIfOrder(Sys.OrderUpTo, Sys.LeadDaysDistribution, Sys.StartLeadDays, Sys.StartOrderQuantity);
            HandleDaysUntilOrderArrives();
            NoOfDay++;
        }

        private void CheckIfOrder(int UpToLevel, List<Distribution> DaysUntilOrderArrivesDistribution, int StartLeadDays, int StartOrderQuantity)
        {
            if (this.DayWithinCycle == 5)
            {
                OderQuantity = UpToLevel - this.EndingInventory + this.ShortageQuantity;
                this.OrderQuantity = OderQuantity;
                MapRandomLeadDays(DaysUntilOrderArrivesDistribution);

            }
            else if (this.Day == 1)
            {
                OderQuantity = StartOrderQuantity;
                this.OrderQuantity = 0;
                DaysUntilOrderArrives = StartLeadDays;
                this.DaysUntilArrive = DaysUntilOrderArrives;
            }
        }

        private void EndingInventoryAndShortage()
        {
            var DemandResult = this.BeginningInventory - this.Demand;
            if (DemandResult > 0)
            {
                this.EndingInventory = DemandResult - this.ShortageQuantity;
                this.ShortageQuantity = 0;
            }
            else
            {
                this.EndingInventory = 0;
                Shortage += Math.Abs(DemandResult);
                this.ShortageQuantity = Shortage;
            }
            OldBegginingInventory = this.EndingInventory;
        }

        private int _BeginningInventory(int StartInventoryQuantity)
        {
            if (DaysUntilOrderArrives == -1)
            {
                return OderQuantity;
            }
            else if (this.Day != 1)
            {
                return OldBegginingInventory;
            }
            else
            {
                return StartInventoryQuantity;
            }
        }
        
        private void HandleDaysUntilOrderArrives()
        {
            //if (DaysUntilOrderArrives >= 0)
            //{
            //    this.DaysUntilArrive = DaysUntilOrderArrives--;
            //}
            //else if (DaysUntilOrderArrives == -1)
            //{
            //    this.BeginningInventory = OderQuantity;
            //    this.LeadDays = 0;
            //    DaysUntilOrderArrives = 0;
            //    this.DaysUntilArrive = DaysUntilOrderArrives;
            //    OderQuantity = 0;
            //}
            if (DaysUntilOrderArrives > 0)
            {
                this.DaysUntilArrive = DaysUntilOrderArrives--;
            }
            else if (DaysUntilOrderArrives == 0)
            {
                this.BeginningInventory = OderQuantity;
                this.LeadDays = 0;
                DaysUntilOrderArrives = 0;
                this.DaysUntilArrive = DaysUntilOrderArrives;
                OderQuantity = 0;
                if (Shortage > 0)
                {
                    this.EndingInventory -= Shortage;
                    Shortage = 0;
                }
            }
        }

    }
}
