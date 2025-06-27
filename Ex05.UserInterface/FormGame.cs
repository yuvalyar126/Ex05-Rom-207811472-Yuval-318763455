using Ex05.GameLogic;
using Ex05.Enums;
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
    public partial class FormGame : Form
    {
        private const int k_SequenceLength = 4;
        private const int k_ButtonSize = 50;
        private const int k_SmallButtonSize = 15;
        private const int k_Spaces = 5;
        private const int k_FormWidth = 350;

        private readonly FormGameSettings r_FormGameSettings = new FormGameSettings();

        private Button[] m_ComputerSequenceButtons;
        private Button[,] m_GuessButtons;
        private Button[] m_SubmitButtons;
        private Button[,] m_ResultButtons;
        private int m_MaxGuesses;
        private int m_CurrentRow = 0;
        private eGuessOption?[] m_CurrentGuessOptions;
        private HashSet<eGuessOption> m_UsedOptionsInCurrentGuess;
        private Game m_Game;
        private Dictionary<eGuessOption, Color> m_OptionsToColors;
        private Dictionary<Color, eGuessOption> m_ColorsToOptions;

        public FormGame()
        {
            InitializeComponent();
            m_CurrentGuessOptions = new eGuessOption?[k_SequenceLength];
            m_UsedOptionsInCurrentGuess = new HashSet<eGuessOption>();
            initializeDictionaries();
        }


        public void RunGame()
        {
            if (r_FormGameSettings.ShowDialog() == DialogResult.OK)
            {
                initializeStartingGame();
                initializeGameBoard();
                enableCurrentRow();
                base.ShowDialog();
            }
        }


        private void initializeStartingGame()
        {
            m_MaxGuesses = r_FormGameSettings.NumberOfChances;
            m_Game = new Game(m_MaxGuesses);
            m_Game.GuessAnalyzed += Game_GuessAnalyzed;
            m_Game.GameWon += Game_GameWon;
            m_Game.GameLost += Game_GameLost;
        }


        private void initializeGameBoard()
        {
            int formHeight = 100 + (m_MaxGuesses * (k_ButtonSize + k_Spaces));
            this.ClientSize = new Size(k_FormWidth, formHeight);

            initializeComputerSequenceButtons();
            initializeGuessButtons();
            initializeSubmitButtons();
            initializeResultButtons();
        }

        private void initializeDictionaries()
        {
            Color[] colors = 
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

            m_OptionsToColors = new Dictionary<eGuessOption, Color>();

            for (int i = 0; i < colors.Length; i++)
            {
                m_OptionsToColors[(eGuessOption)i] = colors[i];
            }


            m_ColorsToOptions = new Dictionary<Color, eGuessOption>();
            foreach (KeyValuePair<eGuessOption, Color> pair in m_OptionsToColors)
            {
                m_ColorsToOptions.Add(pair.Value, pair.Key);
            }
        }

        private void initializeComputerSequenceButtons()
        {
            m_ComputerSequenceButtons = new Button[k_SequenceLength];
            int startX = 10;
            int startY = 10;

            for (int i = 0; i < k_SequenceLength; i++)
            {
                m_ComputerSequenceButtons[i] = new Button();
                m_ComputerSequenceButtons[i].Size = new Size(k_ButtonSize, k_ButtonSize);
                m_ComputerSequenceButtons[i].Location = new Point(
                    startX + i * (k_ButtonSize + k_Spaces), startY);
                m_ComputerSequenceButtons[i].BackColor = Color.Black;
                m_ComputerSequenceButtons[i].Enabled = false;
                m_ComputerSequenceButtons[i].FlatStyle = FlatStyle.Flat;
                this.Controls.Add(m_ComputerSequenceButtons[i]);
            }
        }

        private void initializeGuessButtons()
        {
            m_GuessButtons = new Button[m_MaxGuesses, k_SequenceLength];
            int startX = 10;
            int startY = 70;

            for (int row = 0; row < m_MaxGuesses; row++)
            {
                for (int col = 0; col < k_SequenceLength; col++)
                {
                    m_GuessButtons[row, col] = new Button();
                    m_GuessButtons[row, col].Size = new Size(k_ButtonSize, k_ButtonSize);
                    m_GuessButtons[row, col].Location = new Point(
                        startX + col * (k_ButtonSize + k_Spaces),
                        startY + row * (k_ButtonSize + k_Spaces));
                    m_GuessButtons[row, col].Enabled = false;
                    m_GuessButtons[row, col].Tag = new int[] { row, col };
                    m_GuessButtons[row, col].Click += new EventHandler(guessButton_Click);
                    this.Controls.Add(m_GuessButtons[row, col]);
                }
            }
        }

        private void initializeSubmitButtons()
        {
            m_SubmitButtons = new Button[m_MaxGuesses];
            int startX = 230;
            int startY = 70;

            for (int i = 0; i < m_MaxGuesses; i++)
            {
                m_SubmitButtons[i] = new Button();
                m_SubmitButtons[i].Size = new Size(50, 25);
                m_SubmitButtons[i].Location = new Point(startX,
                    startY + i * (k_ButtonSize + k_Spaces) + 12);
                m_SubmitButtons[i].Text = "-->>";
                m_SubmitButtons[i].Enabled = false;
                m_SubmitButtons[i].Tag = i;
                m_SubmitButtons[i].Click += new EventHandler(submitButton_Click);
                this.Controls.Add(m_SubmitButtons[i]);
            }
        }

        private void initializeResultButtons()
        {
            m_ResultButtons = new Button[m_MaxGuesses, k_SequenceLength];
            int startX = 290;
            int startY = 70;

            for (int row = 0; row < m_MaxGuesses; row++)
            {
                for (int col = 0; col < k_SequenceLength; col++)
                {
                    m_ResultButtons[row, col] = new Button();
                    m_ResultButtons[row, col].Size = new Size(k_SmallButtonSize, k_SmallButtonSize);

                    int x = startX + (col % 2) * (k_SmallButtonSize + 2);
                    int y = startY + row * (k_ButtonSize + k_Spaces) + (col / 2) * (k_SmallButtonSize + 2) + 10;

                    m_ResultButtons[row, col].Location = new Point(x, y);
                    m_ResultButtons[row, col].Enabled = false;
                    this.Controls.Add(m_ResultButtons[row, col]);
                }
            }
        }

        private void guessButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton == null)
            {
                return;
            }

            int[] position = (int[])clickedButton.Tag;
            int row = position[0];
            int col = position[1];

            FormColorPicker colorPicker = new FormColorPicker();
            if (colorPicker.ShowDialog() == DialogResult.OK)
            {
                Color selectedColor = colorPicker.SelectedColor;
                eGuessOption selectedOption = m_ColorsToOptions[selectedColor];
                if(m_UsedOptionsInCurrentGuess.Contains(selectedOption) &&
                            (!m_CurrentGuessOptions[col].HasValue || m_CurrentGuessOptions[col].Value != selectedOption))
                {
                    MessageBox.Show("You can't choose this color again, please select a different color!", "Invalid Color"
                        ,MessageBoxButtons.OK);
                    return;
                }
                

                if(m_CurrentGuessOptions[col].HasValue)
                {
                    m_UsedOptionsInCurrentGuess.Remove(m_CurrentGuessOptions[col].Value);
                }

                clickedButton.BackColor = selectedColor;
                m_CurrentGuessOptions[col] = selectedOption;

                if (!m_UsedOptionsInCurrentGuess.Contains(selectedOption))
                {
                    m_UsedOptionsInCurrentGuess.Add(selectedOption);
                }

                checkIfRowComplete();
            }
        }


        private void checkIfRowComplete()
        {
            bool isComplete = true;
            for (int i = 0; i < k_SequenceLength; i++)
            {
                if (!m_CurrentGuessOptions[i].HasValue)
                {
                    isComplete = false;
                    break;
                }
            }

            m_SubmitButtons[m_CurrentRow].Enabled = isComplete;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            List<eGuessOption> guess = new List<eGuessOption>();
            for (int i = 0; i < k_SequenceLength; i++)
            {
                guess.Add(m_CurrentGuessOptions[i].Value);
            }

            m_Game.AnalyzeGuess(guess);

  
            m_SubmitButtons[m_CurrentRow].Enabled = false;
            disableCurrentRow();

            if (m_Game.IsWin || m_Game.IsGameOver)
            {
                return;
            }

            m_CurrentRow++;
            if (m_CurrentRow < m_MaxGuesses)
            {
                m_CurrentGuessOptions = new eGuessOption?[k_SequenceLength];
                m_UsedOptionsInCurrentGuess.Clear();
                enableCurrentRow();
            }
        }

        private void revealComputerSequence()
        {
            List<eGuessOption> sequence = m_Game.GetComputerSequence();

            for (int i = 0; i < sequence.Count; i++)
            {
                eGuessOption option = sequence[i];
                Color color = m_OptionsToColors[option];
                m_ComputerSequenceButtons[i].BackColor = color;
            }
        }

        private void Game_GuessAnalyzed(List<eFeedbackOption> i_Feedback)
        {
            int buttonIndex = 0;

            foreach (eFeedbackOption feedback in i_Feedback)
            {
                if (feedback == eFeedbackOption.Bull)
                {
                    m_ResultButtons[m_CurrentRow, buttonIndex].BackColor = Color.Black;
                }
                else if (feedback == eFeedbackOption.Hit)
                {
                    m_ResultButtons[m_CurrentRow, buttonIndex].BackColor = Color.Yellow;
                }

                buttonIndex++;
            }
        }


        private void enableCurrentRow()
        {
            for (int i = 0; i < k_SequenceLength; i++)
            {
                m_GuessButtons[m_CurrentRow, i].Enabled = true;
            }
        }

        private void disableCurrentRow()
        {
            for (int i = 0; i < k_SequenceLength; i++)
            {
                m_GuessButtons[m_CurrentRow, i].Enabled = false;
            }
        }

        private void disableAllButtons()
        {
            for (int row = 0; row < m_MaxGuesses; row++)
            {
                for (int col = 0; col < k_SequenceLength; col++)
                {
                    m_GuessButtons[row, col].Enabled = false;
                }
                m_SubmitButtons[row].Enabled = false;
            }
        }


        private void Game_GameWon()
        {
            revealComputerSequence();
            MessageBox.Show($"Congratulations! You guessed after {m_CurrentRow + 1} steps!",
                "You Win!", MessageBoxButtons.OK);
            disableAllButtons();
        }

        private void Game_GameLost()
        {
            revealComputerSequence();
            MessageBox.Show("Game Over! no more guesses allowed. you lost.",
                "Game Over", MessageBoxButtons.OK);
            disableAllButtons();
        }

    }
}

