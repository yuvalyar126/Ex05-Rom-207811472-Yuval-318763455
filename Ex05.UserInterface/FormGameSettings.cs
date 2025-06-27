using System;
using System.Windows.Forms;

namespace Ex05.UserInterface
{
    public partial class FormGameSettings : Form
    {
        private const int k_MinChances = 4;
        private const int k_MaxChances = 10;
        private int m_CurrentChanceCount = k_MinChances;

        public FormGameSettings()
        {
            InitializeComponent();
            this.numberOfChancesButton.Text = $"Number Of Chances:  {k_MinChances}";
        }

        public int NumberOfChances
        {
            get
            {
                return m_CurrentChanceCount;
            }
        }

        private void numberOfChancesButton_Click(object sender, EventArgs e)
        {
            m_CurrentChanceCount++;
            if (m_CurrentChanceCount > k_MaxChances)
            {
                m_CurrentChanceCount = k_MinChances;
            }

            this.numberOfChancesButton.Text = $"Number Of Chances:  {m_CurrentChanceCount}";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //this.Close();
        }
    }
}
