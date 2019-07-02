using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    public class FourInARowUI
    {
        private const int k_MinSizeBoard = 4;
        private const int k_MaxSizeBoard = 8;
        private int m_BoardLength;
        private int m_BoardWidth;
        private FourInARowLogic m_GameLogic;
        private Player m_CurrentPlayer;

        public FourInARowUI()
        {
            DispalyGameInstruction();
            getUserBoardSizeChoice();
            m_GameLogic = new FourInARowLogic(m_BoardLength, m_BoardWidth, ePlayerType.humen, getUserOpponentType());
            StartCompetition();
        }

        private void getUserBoardSizeChoice()
        {
            string lengthInput;
            string widthInput;
            bool validInput = true;

            do
            {
                System.Console.WriteLine("{0}Insert the length of the board(number of rows), must be between {1} - {2} :", Environment.NewLine, k_MinSizeBoard, k_MaxSizeBoard);
                lengthInput = System.Console.ReadLine();
                System.Console.WriteLine("{0}Insert the width of the board(number of columns), must be between {1} - {2} :", Environment.NewLine, k_MinSizeBoard, k_MaxSizeBoard);
                widthInput = System.Console.ReadLine();
                validInput = validateUserBoardSizeChoice(lengthInput, widthInput);
                if (!validInput)
                {
                    System.Console.WriteLine("{0}Sorry your input is Illegal, please try again", Environment.NewLine);
                }
            }
            while (!validInput);

            m_BoardLength = int.Parse(lengthInput);
            m_BoardWidth = int.Parse(widthInput);
        }

        private bool validateUserBoardSizeChoice(string i_LengthStr, string i_WidthStr)
        {
            bool isLengthInputNum = false;
            bool isWidthInputNum = false;
            int lengthInt;
            int widthInt;
            bool validLengthInput = false;
            bool validWidthInput = false;

            isLengthInputNum = int.TryParse(i_LengthStr, out lengthInt);
            isWidthInputNum = int.TryParse(i_WidthStr, out widthInt);
            if (isLengthInputNum && isWidthInputNum)
            {
                validLengthInput = lengthInt >= k_MinSizeBoard && lengthInt <= k_MaxSizeBoard;
                validWidthInput = widthInt >= k_MinSizeBoard && widthInt <= k_MaxSizeBoard;
            }

            return validLengthInput && validWidthInput;
        }

        private ePlayerType getUserOpponentType()
        {
            string opponentInput;
            bool vaildInput = true;

            do
            {
                System.Console.WriteLine("{0}Choose your opponent:{0}0 - humen player{0}1 - computer player", Environment.NewLine);
                opponentInput = System.Console.ReadLine();
                vaildInput = validateUserOpponentChoice(opponentInput);
                if (!vaildInput)
                {
                    System.Console.WriteLine("{0}Sorry your input is Illegal, please try again", Environment.NewLine);
                }
            }
            while (!vaildInput);

            return opponentInput.Equals("0") ? ePlayerType.humen : ePlayerType.computer;
        }

        private bool validateUserOpponentChoice(string i_OpponentStrInput)
        {
            bool isValidInput = false;

            if (i_OpponentStrInput.Equals("0") || i_OpponentStrInput.Equals("1"))
            {
                isValidInput = true;
            }

            return isValidInput;
        }

        public void StartCompetition()
        {
            bool continueGame = true;
            while (continueGame)
            {
                bool continueRound = true;

                m_CurrentPlayer = m_GameLogic.Player1;
                m_GameLogic.Player1.BoardSign = eBoardSign.X;
                m_GameLogic.Player2.BoardSign = eBoardSign.O;
                m_GameLogic.ResetAllCells();
                displayBoardState();

                while (continueRound)
                {
                    string validateInput;
                    int validateColNumber;
                    bool isBoardFull;

                    System.Console.WriteLine("++ player {0} turn ++", m_CurrentPlayer.Id);

                    if (m_CurrentPlayer.Type == ePlayerType.humen)
                    {
                        validateInput = getValidHumenChoice();
                        if (validateInput.Equals("Q"))
                        {
                            switchPlayer().Score++;
                            continueRound = false;
                            continueGame = nextRound();
                            break;
                        }
                        else
                        {
                            validateColNumber = int.Parse(validateInput);
                        }
                    }
                    else
                    {
                        validateColNumber = getValidComputerChoice();
                    }

                    m_GameLogic.PlaceChoice(m_CurrentPlayer.BoardSign, validateColNumber);
                    Ex02.ConsoleUtils.Screen.Clear();
                    displayBoardState();
                    if (m_GameLogic.CheckForSequenceOfFour() == true)
                    {
                        System.Console.WriteLine("Player {0} Wins !!", m_CurrentPlayer.Id);
                        m_CurrentPlayer.Score++;
                        System.Console.WriteLine("{0}player 1 : {1}{0}player 2 : {2}", Environment.NewLine, m_GameLogic.Player1.Score, m_GameLogic.Player2.Score);
                        continueRound = false;
                        continueGame = nextRound();
                        break;
                    }

                    isBoardFull = m_GameLogic.CheckIfBoardIsFull();
                    if (isBoardFull == true)
                    {
                        System.Console.WriteLine("Tie Round !!{0}player 1 : {1}{0}player 2 : {2}", Environment.NewLine, m_GameLogic.Player1.Score, m_GameLogic.Player2.Score);
                        continueRound = false;
                        continueGame = nextRound();
                    }

                    m_CurrentPlayer = switchPlayer();
                }
            }

            System.Console.WriteLine("{0}GAME OVER", Environment.NewLine);
        }

        private string getValidHumenChoice()
        {
            bool validInput = false;
            bool isColAvilable = false;
            bool isColNumber = false;
            bool isColInRange = false;
            int colInputInt;
            string colInputStr;

            do
            {
                System.Console.WriteLine("Enter a column number:");
                colInputStr = System.Console.ReadLine();
                if (colInputStr.Equals("Q"))
                {
                    validInput = true;
                    break;
                }

                isColNumber = int.TryParse(colInputStr, out colInputInt);
                if (isColNumber)
                {
                    isColInRange = colInputInt >= 1 && colInputInt <= m_BoardWidth ? true : false;
                    if (isColInRange)
                    {
                        isColAvilable = m_GameLogic.CheckColumnAvailability(colInputInt);
                        if (isColAvilable)
                        {
                            validInput = true;
                        }
                        else
                        {
                            System.Console.WriteLine("{0}Sorry the column is already full, please try again", Environment.NewLine);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("{0}Sorry your choice is out of range, please try again", Environment.NewLine);
                    }
                }
            }
            while (!validInput);

            return colInputStr;
        }

        private int getValidComputerChoice()
        {
            List<int> avilableColumns = m_GameLogic.GetAvailableCols();
            Random random = new Random();
            int randomNumber;
            bool validCol = true;

            do
            {
                randomNumber = random.Next(1, m_BoardWidth);
                validCol = avilableColumns.Contains(randomNumber);
            }
            while (!validCol);

            return randomNumber;
        }

        private bool validateHumenColChoice(string i_ColInput)
        {
            bool isValid = false;
            int colInputInt;

            if (i_ColInput.Equals("Q") || int.TryParse(i_ColInput, out colInputInt))
            {
                isValid = true;
            }

            return isValid;
        }

        private Player switchPlayer()
        {
            Player switchedPlayer = m_CurrentPlayer;

            if (m_CurrentPlayer == m_GameLogic.Player1)
            {
                switchedPlayer = m_GameLogic.Player2;
            }
            else
            {
                switchedPlayer = m_GameLogic.Player1;
            }

            return switchedPlayer;
        }

        private bool nextRound()
        {
            string playerCohice;
            bool validChoice = true;

            do
            {
                System.Console.WriteLine("{0}Would you like to play another round? enter Y for yes or N for No", Environment.NewLine);
                playerCohice = System.Console.ReadLine();
                validChoice = playerCohice.Equals("Y") || playerCohice.Equals("N") ? true : false;
                if (!validChoice)
                {
                    System.Console.WriteLine("{0}Sorry your choice is illegal please try again");
                }
            }
            while (!validChoice);

            return playerCohice.Equals("Y") ? true : false;
        }

        private void DispalyGameInstruction()
        {
            string msg = string.Format(
@"Welcome to Four In Row game.
You can choose the size of the board and your opponent, Your opponent can be the computer or another humen player.
The board minimum size is {0} * {0} and maximum is {1} * {1}, please notice that length and width does not have to be the same.
Leaving a game will lead to extra point for your opponent, another round will  be with the same board size and players.
If you would like to quit press Q  at your turn.", k_MinSizeBoard, k_MaxSizeBoard);

            System.Console.WriteLine(msg);
        }

        private void displayBoardState()
        {
            StringBuilder boardStateBuilder = new StringBuilder();

            boardStateBuilder = buildIndexRow();

            for (int i = 0; i < m_GameLogic.RowsNumber; i++)
            {
                boardStateBuilder.AppendLine(buildMainRow(i).ToString());
                boardStateBuilder.AppendLine(buildBoundaryLine().ToString());
            }

            System.Console.WriteLine(boardStateBuilder);
        }

        private StringBuilder buildIndexRow()
        {
            StringBuilder indexRowBuilder = new StringBuilder();

            indexRowBuilder.Append("  ");

            for (int i = 1; i <= m_BoardWidth; i++)
            {
                indexRowBuilder.Append(i.ToString());
                indexRowBuilder.Append("   ");
            }

            indexRowBuilder.AppendLine(string.Empty);

            return indexRowBuilder;
        }

        private StringBuilder buildMainRow(int i_rowNumber)
        {
            StringBuilder rowBuilder = new StringBuilder();

            rowBuilder.Append("|");

            for (int i = 0; i < m_GameLogic.ColsNumber; i++)
            {
                eBoardSign currentSign = m_GameLogic.Board[i_rowNumber, i];
                if (currentSign == eBoardSign.E)
                {
                    rowBuilder.Append("   ");
                }
                else
                {
                    rowBuilder.Append(" " + currentSign + " ");
                }

                rowBuilder.Append("|");
            }

            return rowBuilder;
        }

        private StringBuilder buildBoundaryLine()
        {
            StringBuilder boundaryBuilder = new StringBuilder();

            for (int i = 0; i < m_GameLogic.ColsNumber * 4; i++)
            {
                boundaryBuilder.Append("=");
            }

            boundaryBuilder.Append("=");

            return boundaryBuilder;
        }
    }
}
