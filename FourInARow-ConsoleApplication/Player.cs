using System;
using System.Collections.Generic;
using System.Text;

namespace C18_Ex02
{
    public class Player
    {
        private int m_Id;
        private ePlayerType m_PlayerType;
        private eBoardSign m_BoardSign;
        private int m_Score = 0;
        private static int s_NumberOfPlayers = 1;

        public int Id
        {
            get { return m_Id; }
        }
        public ePlayerType Type
        {
            get { return m_PlayerType; }
            set { m_PlayerType = value; }
        }

        public eBoardSign BoardSign
        {
            get { return m_BoardSign; }
            set { m_BoardSign = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public Player(ePlayerType i_type)
        {
            m_PlayerType = i_type;
            m_Id = s_NumberOfPlayers++;
        }
    }
}
