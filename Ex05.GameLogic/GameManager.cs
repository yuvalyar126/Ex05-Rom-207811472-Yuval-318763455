//namespace Ex05.GameLogic
//{
//    public class GameManager
//    {
//        private bool m_IsGameRunning;

//        public GameManager()
//        {
//            m_IsGameRunning = true;
//        }

//        public void GameLoop()
//        {
//            while (m_IsGameRunning)
//            {
//                int maxGuessesCount = getMaxGuessesCount();
//                if (maxGuessesCount != 0)
//                {
//                    Game game = new Game(maxGuessesCount);
//                    playGameRound(game);
//                }

//                if (!m_IsGameRunning)
//                {
//                    UserInterface.DisplayGoodbyeMessage();
//                    break;
//                }

//                askForNewRound();
//            }
//        }

//        private void askForNewRound()
//        {
//            string userInput = UserInterface.GetNewRoundInput();

//            if (userInput == "Q" || userInput == "N")
//            {
//                m_IsGameRunning = false;
//                UserInterface.DisplayGoodbyeMessage();
//            }
//            else if (userInput == "Y")
//            {
//                m_IsGameRunning = true;
//                UserInterface.ClearScreen();
//            }
//            else
//            {
//                UserInterface.DisplayInvalidYesNoInputMessage();
//            }
//        }

//        private int getMaxGuessesCount()
//        {
//            int maxGuesses = 0;
//            bool isInvalidInput = true;

//            while (isInvalidInput)
//            {
//                string maxGuessesInput = UserInterface.GetMaxGuessesInput();
//                if (maxGuessesInput == "Q")
//                {
//                    m_IsGameRunning = false;
//                    break;
//                }
//                else if (InputValidator.IsMaxGuessesInputNumber(maxGuessesInput, out maxGuesses) == false)
//                {
//                    UserInterface.DisplayInvalidMaxGuessesMessage();
//                    continue;
//                }
//                else if (InputValidator.IsMaxGuessesInputInBounds(maxGuesses) == false)
//                {
//                    UserInterface.DisplayMaxGuessesOutOfBoundsMessage();
//                    continue;
//                }

//                isInvalidInput = false;
//            }

//            return maxGuesses;
//        }

//        private void playGameRound(Game i_Game)
//        {
//            bool isRoundOver = false;

//            while (!isRoundOver)
//            {
//                UserInterface.ClearScreen();
//                UserInterface.DisplayGameBoard(i_Game.GetCurrentBoardData());
//                string currentGuess = getCurrentGuess(out isRoundOver);
//                if (!m_IsGameRunning)
//                {
//                    break;
//                }

//                GuessResult guessResult = i_Game.ExecuteGuess(currentGuess);
//                if (guessResult.Bulls == 4)
//                {
//                    UserInterface.ClearScreen();
//                    UserInterface.DisplayGameBoard(i_Game.GetCurrentBoardData());
//                    UserInterface.DisplayWinMessage(i_Game.CurrentGuessIndex - 1);
//                    isRoundOver = true;
//                }
//                else if (i_Game.CurrentGuessIndex - 1 >= i_Game.MaxGuesses)
//                {
//                    UserInterface.ClearScreen();
//                    i_Game.RevealComputerSequence();
//                    UserInterface.DisplayGameBoard(i_Game.GetCurrentBoardData());
//                    UserInterface.DisplayLoseMessage();
//                    isRoundOver = true;
//                }
//            }
//        }

//        private string getCurrentGuess(out bool o_IsRoundOver)
//        {
//            string currentGuess = string.Empty;
//            bool isValidInput = false;

//            o_IsRoundOver = false;
//            while (!isValidInput)
//            {
//                currentGuess = UserInterface.GetUserGuess();
//                if (currentGuess == "Q")
//                {
//                    currentGuess = string.Empty;
//                    o_IsRoundOver = true;
//                    isValidInput = true;
//                    m_IsGameRunning = false;
//                }
//                else if (!InputValidator.IsUserGuessLengthValid(currentGuess))
//                {
//                    UserInterface.DisplayInvalidGuessLengthMessage();
//                }
//                else if (!InputValidator.IsUserGuessValid(currentGuess))
//                {
//                    UserInterface.DisplayInvalidGuessMessage();
//                }
//                else
//                {
//                    isValidInput = true;
//                }
//            }

//            return currentGuess;
//        }
//    }
//}