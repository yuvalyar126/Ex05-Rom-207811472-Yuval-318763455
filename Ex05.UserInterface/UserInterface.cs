using System;
using System.Text;

namespace Ex02
{
    internal static class UserInterface
    {
        internal static void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        internal static string GetMaxGuessesInput()
        {
            Console.WriteLine("Please enter the maximum number of guesses (4-10) or 'Q' to quit:");
            return Console.ReadLine();
        }

        internal static void DisplayInvalidMaxGuessesMessage()
        {
            Console.WriteLine("Invalid input! You must enter a number.");
        }

        internal static void DisplayMaxGuessesOutOfBoundsMessage()
        {
            Console.WriteLine("Invalid input! The number must be between 4 and 10.");
        }

        internal static string GetUserGuess()
        {
            Console.WriteLine($"Please type your next guess <A B C D> or 'Q' to quit:");
            string guess = Console.ReadLine();

            return guess;
        }

        internal static void DisplayInvalidGuessLengthMessage()
        {
            Console.WriteLine("Invalid guess! The guess must be 4 letters.");
        }

        internal static void DisplayInvalidGuessMessage()
        {
            Console.WriteLine("Invalid guess! The guess must be different uppercased letters between A-H.");
        }

        internal static void DisplayGameBoard(string[,] i_BoardData)
        {
            int rowCount = i_BoardData.GetLength(0);
            StringBuilder boardBuilder = new StringBuilder();

            boardBuilder.AppendLine("Current board status: \n");
            boardBuilder.AppendLine("|Pins:     |Result:|");
            boardBuilder.AppendLine("|==========|=======|");
            for (int i = 0; i < rowCount; i++)
            {
                string guess = formatGuess(i_BoardData[i, 0]);
                string result = formatResult(i_BoardData[i, 1]);
                boardBuilder.AppendLine($"|{guess}|{result}|");
                boardBuilder.AppendLine("|==========|=======|");
            }

            Console.WriteLine(boardBuilder.ToString());
        }

        private static string formatGuess(string i_Guess)
        {
            string output;

            if (string.IsNullOrEmpty(i_Guess))
            {
                output = "          ";
            }
            else
            {
                char[] chars = i_Guess.ToCharArray();
                output = string.Join(" ", chars).PadRight(10);
            }

            return output;
        }


        private static string formatResult(string i_Result)
        {
            string output;

            if (string.IsNullOrEmpty(i_Result))
            {
                output = "       ";
            }
            else
            {
                char[] resultChars = i_Result.ToCharArray();
                output = string.Join(" ", resultChars).PadRight(7);
            }

            return output;
        }

        internal static void DisplayWinMessage(int i_GuessCount)
        {
            Console.WriteLine($"You guessed after {i_GuessCount} steps!");
        }

        internal static void DisplayLoseMessage()
        {
            Console.WriteLine($"No more guesses allowed. You lost.");
        }

        internal static string GetNewRoundInput()
        {
            Console.WriteLine("Would you like to start a new game? <Y/N>");

            return Console.ReadLine();
        }

        internal static void DisplayInvalidYesNoInputMessage()
        {
            Console.WriteLine("Invalid input! Please enter 'Y' for yes or 'N' for no.");
        }

        internal static void DisplayGoodbyeMessage()
        {
            Console.WriteLine("Goodbye!");
        }
    }
}
