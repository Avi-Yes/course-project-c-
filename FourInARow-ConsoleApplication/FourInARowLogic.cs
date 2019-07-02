using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    public class FourInARowLogic
    {
        private eBoardSign[,] m_BoardMatrix;
        private int m_RowsNumber;
        private int m_ColsNumber;
        private  int m_StoredCellsCounter = 0;
        private int m_RowIndexOfLastEntry = 0;
        private int m_ColIndexOfLastEntry = 0;
        private Player m_Player1;
        private Player m_Player2;

        public int RowsNumber
        {
            get { return m_RowsNumber; }
        }

        public int ColsNumber
        {
            get { return m_ColsNumber; }
        }

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public eBoardSign[,] Board
        {
            get { return m_BoardMatrix; }
        }

        public FourInARowLogic(int i_BoradLength, int i_BoradWidth, ePlayerType player1Type, ePlayerType player2Type)
        {
            m_RowsNumber = i_BoradLength;
            m_ColsNumber = i_BoradWidth;
            m_Player1 = new Player(player1Type);
            m_Player2 = new Player(player2Type);
            m_BoardMatrix = new eBoardSign [i_BoradLength, i_BoradWidth];
        }

        internal void ResetAllCells()
        {
            for (int i = 0; i < m_RowsNumber; i++)
            {
                for (int j = 0; j < m_ColsNumber; j++)
                {
                    m_BoardMatrix[i, j] = eBoardSign.E;
                }
            }

            m_StoredCellsCounter = 0;
        }

        internal bool PlaceChoice(eBoardSign i_Sign, int i_ColNumber)
        {
            bool isPlacedSuccessfully = CheckColumnAvailability(i_ColNumber);

            if (isPlacedSuccessfully)
            {
                for (int i = m_RowsNumber - 1; i >= 0; i--)
                {
                    if (m_BoardMatrix[i, i_ColNumber - 1] == eBoardSign.E)
                    {
                        isPlacedSuccessfully = true;
                        m_BoardMatrix[i, i_ColNumber - 1] = i_Sign;
                        m_RowIndexOfLastEntry = i;
                        m_ColIndexOfLastEntry = i_ColNumber - 1; 
                        m_StoredCellsCounter++;
                        break;
                    }
                }
            }

            return isPlacedSuccessfully;
        }

        public bool CheckColumnAvailability(int i_ColNumber)
        {
            bool isAvailable = false;
            
            if (m_BoardMatrix[0,i_ColNumber - 1] == eBoardSign.E)
            {
                isAvailable = true;
            }

            return isAvailable;
        }

        internal bool CheckIfBoardIsFull()
        {
            return m_StoredCellsCounter == m_ColsNumber * m_RowsNumber  ? true : false;
        }

        internal List<int> GetAvailableCols()
        {
            List<int> colsNumber = new List<int>(m_ColsNumber);

            for (int i = 0; i < m_ColsNumber; i++)
            {
                if (m_BoardMatrix[0, i] == eBoardSign.E)
                {
                    colsNumber.Add(i + 1);
                }
            }

            return colsNumber;
        }

        internal bool CheckForSequenceOfFour()
        {
            bool hasHorizontalSequence = checkForHorizontalSequence();
            bool hasVerticalSequence = checkForVerticalSequence();
            bool hasDownDiagonalSequence = checkForDownDiagonalSequence();
            bool hasUpDiagonalSequence = checkForUpDiagonalSequence();

            return hasHorizontalSequence || hasVerticalSequence || hasDownDiagonalSequence || hasUpDiagonalSequence;
        }

        private bool checkForHorizontalSequence()
        {
            bool hasRightSequence = false;
            bool hasLeftSequence = false;
            eBoardSign lastEntrySign = m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry];

            if (m_ColIndexOfLastEntry + 3 < m_ColsNumber)
            {
                if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry + 1] && 
                    lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry + 2] &&
                    lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry + 3])
                {
                    hasRightSequence = true;          
                }
            }

            if (m_ColIndexOfLastEntry - 3 >= 0)
            {
                if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry - 1] &&
                    lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry - 2] &&
                    lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry - 3])
                {
                    hasRightSequence = true;
                }
            }

            return hasLeftSequence || hasRightSequence;
        }

        private bool checkForVerticalSequence()
        {
            bool hasDownSequence = false;
            eBoardSign lastEntrySign = m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry];

            if (m_RowIndexOfLastEntry + 3 < m_RowsNumber)
            {
                if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 1, m_ColIndexOfLastEntry] &&
                    lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 2, m_ColIndexOfLastEntry] &&
                    lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 3, m_ColIndexOfLastEntry])
                {
                    hasDownSequence = true;
                }
            }

            return hasDownSequence;
        }

        private bool checkForDownDiagonalSequence()
        {
            bool hasRightSequence = false;
            bool hasLeftSequence = false;
            eBoardSign lastEntrySign = m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry];

            if (m_RowIndexOfLastEntry + 3 < m_RowsNumber)
            {
                if (m_ColIndexOfLastEntry + 3 < m_ColsNumber)
                {
                    if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 1, m_ColIndexOfLastEntry + 1] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 2, m_ColIndexOfLastEntry + 2] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 3, m_ColIndexOfLastEntry + 3])
                    {
                        hasRightSequence = true;
                    }
                }

                if (m_ColIndexOfLastEntry - 3 >= 0)
                {
                    if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 1, m_ColIndexOfLastEntry - 1] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 2, m_ColIndexOfLastEntry - 2] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry + 3, m_ColIndexOfLastEntry - 3])
                    {
                        hasLeftSequence = true;
                    }
                }
            }

            return hasRightSequence || hasLeftSequence;
        }

        private bool checkForUpDiagonalSequence()
        {
            bool hasRightSequence = false;
            bool hasLeftSequence = false;
            eBoardSign lastEntrySign = m_BoardMatrix[m_RowIndexOfLastEntry, m_ColIndexOfLastEntry];

            if (m_RowIndexOfLastEntry - 3 >= 0)
            {
                if (m_ColIndexOfLastEntry + 3 < m_ColsNumber)
                {
                    if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry - 1, m_ColIndexOfLastEntry + 1] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry - 2, m_ColIndexOfLastEntry + 2] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry - 3, m_ColIndexOfLastEntry + 3])
                    {
                        hasRightSequence = true;
                    }
                }

                if (m_ColIndexOfLastEntry - 3 >= 0)
                {
                    if (lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry - 1, m_ColIndexOfLastEntry - 1] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry - 2, m_ColIndexOfLastEntry - 2] &&
                        lastEntrySign == m_BoardMatrix[m_RowIndexOfLastEntry - 3, m_ColIndexOfLastEntry - 3])
                        {
                            hasLeftSequence = true;
                        }
                }
            }

            return hasRightSequence || hasLeftSequence;
        }
    }
}
