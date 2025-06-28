using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05.UserInterface
{
    public partial class FormColorPicker : Form
    {
        private Button[] m_ColorButtons;
        private Color[] m_AvailableColors = new Color[]
        {
            Color.Purple,
            Color.Red,
            Color.Lime,
            Color.Cyan,
            Color.Blue,
            Color.Yellow,
            Color.Maroon,
            Color.White
        };

        private Color m_SelectedColor;

        public FormColorPicker()
        {
            InitializeComponent();
            initializeColors();
        }

        private void initializeColors()
        {
            m_ColorButtons = new Button[8];
            int buttonSize = 50;
            int spacing = 5;
            int startX = 10;
            int startY = 10;

            for (int i = 0; i < 8; i++)
            {
                m_ColorButtons[i] = new Button();
                m_ColorButtons[i].Size = new Size(buttonSize, buttonSize);
                m_ColorButtons[i].BackColor = m_AvailableColors[i];
                m_ColorButtons[i].FlatStyle = FlatStyle.Flat;
                m_ColorButtons[i].Tag = m_AvailableColors[i];
                m_ColorButtons[i].Click += new EventHandler(colorButton_Click);

                int row = i / 4;
                int col = i % 4;
                m_ColorButtons[i].Location = new System.Drawing.Point(
                    startX + col * (buttonSize + spacing),
                    startY + row * (buttonSize + spacing)
                );
                Controls.Add(m_ColorButtons[i]);
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                m_SelectedColor = (Color)clickedButton.Tag;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public Color SelectedColor
        {
            get
            { 
                return m_SelectedColor;
            }
        }
    }
}
