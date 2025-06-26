using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    internal class Game
    {
        private readonly string r_ComputerSequence;
        private Board m_Board;
        private int m_CurrentGuessIndex;

        internal Game(int i_MaxGuesses)
        {
            m_Board = new Board(i_MaxGuesses);
            m_CurrentGuessIndex = 1;
            r_ComputerSequence = GenerateComputerMove();
        }

        internal int MaxGuesses
        {
            get
            {
                return m_Board.MaxGuesses;
            }
        }

        internal int CurrentGuessIndex
        {
            get
            {
                return m_CurrentGuessIndex;
            }
        }

        internal string GenerateComputerMove()
        {
            Random random = new Random();
            char[] result = new char[InputValidator.k_SequenceLength];
            List<char> availableLetters = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

            for (int i = 0; i < InputValidator.k_SequenceLength; i++)
            {
                int index = random.Next(0, availableLetters.Count);
                result[i] = availableLetters[index];
                availableLetters.RemoveAt(index);
            }

            string computerSequence = new string(result);

            return computerSequence;
        }


        internal string[,] GetCurrentBoardData()
        {
            return m_Board.BoardData;
        }

        internal GuessResult ExecuteGuess(string i_Guess)
        {
            GuessResult result = analyzeGuess(i_Guess);

            m_Board.UpdateBoard(m_CurrentGuessIndex, i_Guess, result.Result);
            m_CurrentGuessIndex++;

            return result;
        }

        private GuessResult analyzeGuess(string i_Guess)
        {
            int bulls = 0;
            int hits = 0;
            StringBuilder resultSignsBuilder = new StringBuilder();

            for (int i = 0; i < InputValidator.k_SequenceLength; i++)
            {
                if (i_Guess[i] == r_ComputerSequence[i])
                {
                    bulls++;
                    resultSignsBuilder.Insert(0, 'V');
                }
                else if (r_ComputerSequence.Contains(i_Guess[i]))
                {
                    hits++;
                    resultSignsBuilder.Append('X');
                }
            }

            GuessResult guessResult = new GuessResult(bulls, hits, resultSignsBuilder.ToString());

            return guessResult;
        }

        internal void RevealComputerSequence()
        {
            m_Board.UpdateBoard(0, r_ComputerSequence, string.Empty);
        }
    }
}