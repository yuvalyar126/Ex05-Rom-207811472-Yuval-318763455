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
        public event Action<eGuessOption, int, int> OptionAdded;
        public event Action OptionFailed;
        public event Action<int> GuessCompleted;
        private readonly List<eGuessOption> r_ComputerSequence;
        private eGuessOption?[] m_CurrentGuessOptions;
        private readonly int r_MaxGuesses;
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

        protected virtual void OnGuessOptionAdded(eGuessOption i_GuessOption, int i_GuessColumn)
        {
            OptionAdded?.Invoke(i_GuessOption, m_CurrentGuessIndex, i_GuessColumn);
        }

        protected virtual void OnGuessOptionFailed()
        {
            OptionFailed?.Invoke();
        }
        
        protected virtual void OnGuessCompleted()
        {
            GuessCompleted?.Invoke(m_CurrentGuessIndex);
        }

        public Game(int i_MaxGuesses)
        {
            m_IsWin = false;
            m_IsGameOver = false;
            r_MaxGuesses = i_MaxGuesses;
            m_CurrentGuessIndex = 0;
            m_Random = new Random();
            m_CurrentGuessOptions = new eGuessOption?[i_MaxGuesses];
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

        public int CurrentGuessIndex
        {
            get
            {
                return m_CurrentGuessIndex;
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

            return result;
        }

        private bool isGuessOptionValid(eGuessOption i_GuessOption, int i_GuessColumn)
        {
            bool isValid = m_CurrentGuessOptions[i_GuessColumn] == i_GuessOption || !m_CurrentGuessOptions.Contains(i_GuessOption);
            return isValid;
        }

        public void AddGuessOption(eGuessOption i_GuessOption, int i_GuessColumn)
        {
            if (isGuessOptionValid(i_GuessOption, i_GuessColumn))
            {
                m_CurrentGuessOptions[i_GuessColumn] = i_GuessOption;
                OnGuessOptionAdded(i_GuessOption, i_GuessColumn);
            }
            else
            {
                OnGuessOptionFailed();
            }
        }

        public void AnalyzeGuess()
        {
            int bulls = 0;
            int hits = 0;
            List<eFeedbackOption> feedback = new List<eFeedbackOption>();

            for (int i = 0; i < k_SequenceLength; i++)
            {
                if (m_CurrentGuessOptions[i] == r_ComputerSequence[i])
                {
                    bulls++;
                    feedback.Insert(0, eFeedbackOption.Bull);
                }
                else if (r_ComputerSequence.Contains(m_CurrentGuessOptions[i].Value))
                {
                    hits++;
                    feedback.Add(eFeedbackOption.Hit);
                }
            }

            OnGuessAnalyzed(feedback);

            if (bulls == k_SequenceLength)
            {
                m_IsWin = true;
                OnGameWon();
            }
            else
            {
                m_CurrentGuessIndex++;
                m_CurrentGuessOptions = new eGuessOption?[k_SequenceLength];

                if (m_CurrentGuessIndex >= r_MaxGuesses)
                {
                    m_IsGameOver = true;
                    OnGameLost();
                }
            }
        }

        public void CheckIfRowComplete()
        {
            for (int i = 0; i < k_SequenceLength; i++)
            {
                if (!m_CurrentGuessOptions[i].HasValue)
                {
                    return;
                }
            }

            OnGuessCompleted();
        }

        public List<eGuessOption> GetComputerSequence()
        {
            return r_ComputerSequence;
        }
    }
}