namespace Ex02
{
    internal static class InputValidator
    {
        private const int k_MinGuesses = 4;
        private const int k_MaxGuesses = 10;
        internal const int k_SequenceLength = 4;

        internal static bool IsMaxGuessesInputNumber(string i_Input, out int o_MaxGuesses)
        {
            bool isValid = int.TryParse(i_Input, out o_MaxGuesses);

            return isValid;
        }

        internal static bool IsMaxGuessesInputInBounds(int i_Input)
        {
            return i_Input >= k_MinGuesses && i_Input <= k_MaxGuesses;
        }

        internal static bool IsUserGuessLengthValid(string i_Guess)
        {
            bool isValidLength = i_Guess.Length == InputValidator.k_SequenceLength;

            return isValidLength;
        }

        internal static bool IsUserGuessValid(string i_Guess)
        {
            bool isGuessValid = true;

            for (int i = 0; i < i_Guess.Length; i++)
            {
                char currentChar = i_Guess[i];
                if (currentChar < 'A' || currentChar > 'H')
                {
                    isGuessValid = false;
                    break;
                }
            }

            for (int i = 0; i < i_Guess.Length; i++)
            {
                for (int j = i + 1; j < i_Guess.Length; j++)
                {
                    if (i_Guess[i] == i_Guess[j])
                    {
                        isGuessValid = false;
                        break;
                    }
                }
            }

            return isGuessValid;
        }
    }
}
