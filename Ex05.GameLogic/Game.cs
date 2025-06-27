using Ex05.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex05.GameLogic
{
    public class Game
    {
        public event Action<List<eFeedbackOption>> GuessAnalyzed;
        public event Action GameWon;
        public event Action GameLost;
        public event Action OptionChosen;

        private readonly List<eGuessOption> r_ComputerSequence;
        private eGuessOption?[] m_CurrentGuessOptions;
        private readonly int r_MaxGuesses;
        private Board m_Board;
        private int m_CurrentGuessIndex;
        Random m_Random;
        private const int k_SequenceLength = 4;
        private bool m_IsWin;
        private bool m_IsGameOver;

        protected virtual void OnGuessAnalyzed(List<eFeedbackOption> i_Feedback)
        {
            GuessAnalyzed?.Invoke(i_Feedback);
        }

        protected virtual void OnGameWon()
        {
            GameWon?.Invoke();
        }

        protected virtual void OnGameLost()
        {
            GameLost?.Invoke();
        }


        public Game(int i_MaxGuesses)
        {
            m_IsWin = false;
            m_IsGameOver = false;
            r_MaxGuesses = i_MaxGuesses;
            m_CurrentGuessIndex = 0;
            m_Board = new Board(i_MaxGuesses);
            m_Random = new Random();
            r_ComputerSequence = GenerateComputerMove();
        }


        public bool IsWin
        {
            get
            {
                return m_IsWin;
            }
        }

        public bool IsGameOver
        {
            get
            {
                return m_IsGameOver;
            }
        }

        public List<eGuessOption> GenerateComputerMove()
        {
            List<eGuessOption> result = new List<eGuessOption>();
            List<eGuessOption> availableOptions = new List<eGuessOption>
            {
                eGuessOption.Option1,
                eGuessOption.Option2,
                eGuessOption.Option3,
                eGuessOption.Option4,
                eGuessOption.Option5,
                eGuessOption.Option6,
                eGuessOption.Option7,
                eGuessOption.Option8
            };

            for (int i = 0; i <k_SequenceLength; i++)
            {
                int index = m_Random.Next(0, availableOptions.Count);
                result.Add(availableOptions[index]);
                availableOptions.RemoveAt(index);
            }

            int debug = 0; //delete
            return result;
        }


        public string[,] GetCurrentBoardData()
        {
            return m_Board.BoardData;
        }


        public GuessResult AnalyzeGuess(List<eGuessOption> i_Guess)
        {
            int bulls = 0;
            int hits = 0;
            List<eFeedbackOption> feedback = new List<eFeedbackOption>();

            for (int i = 0; i < k_SequenceLength; i++)
            {
                if (i_Guess[i] == r_ComputerSequence[i])
                {
                    bulls++;
                    feedback.Insert(0, eFeedbackOption.Bull);
                }
                else if (r_ComputerSequence.Contains(i_Guess[i]))
                {
                    hits++;
                    feedback.Add(eFeedbackOption.Hit);
                }
            }

            GuessResult result = new GuessResult(bulls, hits, feedback);
            OnGuessAnalyzed(feedback);

            if (result.Bulls == k_SequenceLength)
            {
                m_IsWin = true;
                OnGameWon();
            }
            else
            {
                m_CurrentGuessIndex++;

                if (m_CurrentGuessIndex >= r_MaxGuesses)
                {
                    m_IsGameOver = true;
                    OnGameLost();
                }
            }


            return result;
        }

        public List<eGuessOption> GetComputerSequence()
        {
            return r_ComputerSequence;
        }
    }
}