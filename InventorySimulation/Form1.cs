using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryModels;
using InventoryTesting;

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
                return Parser.ReadTestCase("D:\\University\\4th year\\Modeling\\[Students]_Template\\InventorySimulation\\InventorySimulation\\TestCases\\TestCase1.txt");
            }
            return null;    
        } 

        private void Simualte(SimulationSystem simulationSystem)
        {
            simulationSystem.BuildSimulationTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TestingManager.Test(System, Constants.FileNames.TestCase1));
            outputDataGridView.Visible = true;
            for (int i = 0; i < System.SimulationTable.Count; i++)
            {
                var SimulateRow = System.SimulationTable[i];    
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


