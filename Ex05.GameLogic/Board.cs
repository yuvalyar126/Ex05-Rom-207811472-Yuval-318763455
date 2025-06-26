namespace Ex02
{
    internal class Board
    {
        private readonly int r_MaxGuesses;
        private readonly string[,] r_BoardData;

        internal Board(int i_MaxGuesses)
        {
            r_MaxGuesses = i_MaxGuesses;
            r_BoardData = new string[i_MaxGuesses + 1, 2];
            initializeBoard();
        }

        internal int MaxGuesses
        {
            get
            {
                return r_MaxGuesses;
            }
        }

        internal string[,] BoardData
        {
            get
            {
                return r_BoardData;
            }
        }

        private void initializeBoard()
        {
            r_BoardData[0, 0] = new string('#', InputValidator.k_SequenceLength);
            for (int i = 1; i < r_MaxGuesses; i++)
            {
                r_BoardData[i, 0] = new string(' ', InputValidator.k_SequenceLength);
                r_BoardData[i, 1] = string.Empty;
            }
        }

        internal void UpdateBoard(int i_GuessNumber, string i_Guess, string i_Signs)
        {
            r_BoardData[i_GuessNumber, 0] = i_Guess;
            r_BoardData[i_GuessNumber, 1] = i_Signs;
        }
    }
}

