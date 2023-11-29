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
        }

        private void PickFileButton_Click(object sender, EventArgs e)
        {
            System = OpenTestCase();
            Simualte(System);
            MessageBox.Show(TestingManager.Test(System, FileName));

        }

        private SimulationSystem OpenTestCase()
        {
            openTestCaseDialog = new OpenFileDialog();
            DialogResult result = openTestCaseDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FileName = openTestCaseDialog.SafeFileName;
                return Parser.ReadTestCase(openTestCaseDialog.FileName);
            }
            return null;    
        } 

        private void Simualte(SimulationSystem simulationSystem)
        {
            simulationSystem.BuildSimulationTable();
        }

    }
}


