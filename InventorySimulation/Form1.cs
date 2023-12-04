using InventoryModels;
using InventoryTesting;
using System;
using System.Windows.Forms;

namespace InventorySimulation
{
    public partial class Form1 : Form
    {
        private string FileName;
        private SimulationSystem System;

        public Form1()
        {
            InitializeComponent();
            outputDataGridView.Visible = false;
        }

        private void PickFileButton_Click(object sender, EventArgs e)
        {
            System = OpenTestCase();
            Simualte(System);
            MessageBox.Show("Simulate Finish...\nYou can test now");


        }

        private SimulationSystem OpenTestCase()
        {
            openTestCaseDialog = new OpenFileDialog();
            DialogResult result = openTestCaseDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileName = openTestCaseDialog.SafeFileName;
                Console.WriteLine(FileName);
                if (FileName == "TestCase1.txt")
                    return Parser.ReadTestCase("D:\\University\\4th year\\Modeling\\[Students]_Template\\InventorySimulation\\InventorySimulation\\TestCases\\TestCase1.txt");
                else if (FileName == "TestCase2.txt")
                    return Parser.ReadTestCase("D:\\University\\4th year\\Modeling\\[Students]_Template\\InventorySimulation\\InventorySimulation\\TestCases\\TestCase2.txt");
            }
            return null;
        }

        private void Simualte(SimulationSystem simulationSystem)
        {
            simulationSystem.BuildSimulationTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (FileName == "TestCase1.txt")
                MessageBox.Show(TestingManager.Test(System, Constants.FileNames.TestCase1));
            else if (FileName == "TestCase2.txt")
                MessageBox.Show(TestingManager.Test(System, Constants.FileNames.TestCase2));
            outputDataGridView.Visible = true;
            for (int i = 0; i < System.SimulationTable.Count; i++)
            {
                var SimulateRow = System.SimulationTable[i];
                if (SimulateRow.DaysUntilArrive < 0)
                {
                    SimulateRow.DaysUntilArrive = 0;
                }
                outputDataGridView.Rows.Add(
                    SimulateRow.Day,
                    SimulateRow.Cycle,
                    SimulateRow.DayWithinCycle,
                    SimulateRow.BeginningInventory,
                    SimulateRow.RandomDemand,
                    SimulateRow.Demand,
                    SimulateRow.EndingInventory,
                    SimulateRow.ShortageQuantity,
                    SimulateRow.OrderQuantity,
                    SimulateRow.RandomLeadDays,
                    SimulateRow.LeadDays,
                    SimulateRow.DaysUntilArrive
                    );
            }
        }


    }
}


