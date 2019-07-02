using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C18_Ex05
{
    public partial class FormGame : Form
    {
        private GameLogic m_GameLogic;
        private Button[] m_ColChoiceButtons;
        private Button[,] m_BoardMatrixDidplay;
        private Size m_FormNewSize;
        private Player m_CurrentPlayer;
        private const int k_ButtonHeight = 20;
        private const int k_ButtonWidth = 30;
        private const int k_ButtonTop = 20;
        private const int k_ButtonLeft = 20;
 
        public FormGame(GameLogic i_GameLogic)
        {
            m_GameLogic = i_GameLogic;
            m_ColChoiceButtons = new Button[i_GameLogic.ColsNumber];
            m_BoardMatrixDidplay = new Button[i_GameLogic.RowsNumber, i_GameLogic.ColsNumber];
            m_CurrentPlayer = i_GameLogic.Player1;
            m_GameLogic.CellChanged += this.ReportCellChanged;
            InitializeComponent();
 
        }

        private void setScorePlayersLabel()
        {
            const int k_AdditionOfUnits = 20; 

            labelPlayer1.Text = string.Format("{0}: {1}", m_GameLogic.Player1.Name, m_GameLogic.Player1.Score);
            labelPlayer2.Text = string.Format("{0}: {1}", m_GameLogic.Player2.Name, m_GameLogic.Player2.Score);
            m_FormNewSize.Height += labelPlayer1.Height + k_AdditionOfUnits;
        }

        private void BuildLineChoiceButtons()
        {
            const int k_AdditionOfUnits = 20; 
            
            for (int i = 0; i < m_GameLogic.ColsNumber; i++)
            {
                Button buttonToAdd = new Button();

                buttonToAdd.Height = k_ButtonHeight;
                buttonToAdd.Width = k_ButtonWidth;
                buttonToAdd.Top = k_ButtonTop;

                if (i == 0)
                {
                    buttonToAdd.Text = "1";
                    buttonToAdd.Left = k_ButtonLeft;
                }
                else
                {
                    buttonToAdd.Text = string.Format("{0}", i + 1);
                    buttonToAdd.Left = m_ColChoiceButtons[i - 1].Right + k_AdditionOfUnits;
                }

                buttonToAdd.Visible = true;
                this.Controls.Add(buttonToAdd);
                m_ColChoiceButtons[i] = buttonToAdd;
                m_ColChoiceButtons[i].Click += new EventHandler(colChoiceButton_Click);
            }

            m_FormNewSize = new Size();
            m_FormNewSize.Width = m_ColChoiceButtons[m_ColChoiceButtons.Length - 1].Right  + k_AdditionOfUnits;
        }

        private void colChoiceButton_Click(object sender, EventArgs t)
        {
            Button colChoiceButton = sender as Button;
            int colChoiceNum = int.Parse(colChoiceButton.Text);

            playTurn(colChoiceNum);
            if (m_GameLogic.Player2.Type == ePlayerType.computer)
            {
                switchPlayer();
                colChoiceNum = m_GameLogic.GetRandomChoice();
                playTurn(colChoiceNum);
            }

            switchPlayer();
           

        }

        private void playTurn(int i_ColNum)
        {
            DialogResult anotherRound;

            m_GameLogic.PlaceChoice(m_CurrentPlayer.BoardSign, i_ColNum);
            //m_BoardMatrixDidplay[m_GameLogic.RowIndexOfLastEntry, m_GameLogic.ColIndexOfLastEntry].Text = m_CurrentPlayer.BoardSign.ToString();

            if (!m_GameLogic.CheckColumnAvailability(i_ColNum))
            {
                m_ColChoiceButtons[i_ColNum -1].Enabled = false;
            }

            if (m_GameLogic.CheckForSequenceOfFour())
            {
                anotherRound = handleWinning();
                handleNextRound(anotherRound);
            }
            else if (m_GameLogic.CheckIfBoardIsFull())
            {
                anotherRound = handleTie();
                handleNextRound(anotherRound);
            }
        }

        public void ReportCellChanged(int i_RowNumber, int i_ColNumber)
        {
            m_BoardMatrixDidplay[i_RowNumber, i_ColNumber].Text = m_CurrentPlayer.BoardSign.ToString();
        }

        private void computerTurn()
        {
            int randomCol = m_GameLogic.GetRandomChoice();
            m_ColChoiceButtons[randomCol].PerformClick();
        }

        private void handleNextRound(DialogResult i_AnotherRound)
        {
            if (i_AnotherRound == DialogResult.Yes)
            {
                this.Hide();
                FormGame formGame = new FormGame(new GameLogic(m_GameLogic.RowsNumber, m_GameLogic.ColsNumber, m_GameLogic.Player1, m_GameLogic.Player2));
                formGame.ShowDialog();
                this.Dispose();
            }
            else
            {
                Application.Exit();
            }
        }

        private DialogResult handleWinning()
        {
            DialogResult result;
            m_CurrentPlayer.Score++;
            result =  MessageBox.Show(string.Format("{0} Won !!{1}Another Round?", m_CurrentPlayer.Name, Environment.NewLine), "A Win!", MessageBoxButtons.YesNo);

            return result;
        }

        private DialogResult handleTie()
        {
            DialogResult result = DialogResult.No;
            result = MessageBox.Show(string.Format("Tie !!{0}Another Round?", Environment.NewLine), "A Tie", MessageBoxButtons.YesNo);    
            
            return result;
        }


        private void switchPlayer()
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

            m_CurrentPlayer =  switchedPlayer;
        }

        private void buildBoardMatrix()
        {
            const int k_AdditionOfUnits = 10; 
            for (int i = 0; i < m_GameLogic.RowsNumber; i++)
            {
                for (int j = 0; j < m_GameLogic.ColsNumber; j++)
                {
                    Button buttonToAdd = new Button();

                    buttonToAdd.Width = m_ColChoiceButtons[0].Width;
                    buttonToAdd.Height = m_ColChoiceButtons[0].Height + k_AdditionOfUnits;
                    if (i == 0)
                    {
                        buttonToAdd.Left = m_ColChoiceButtons[j].Left;
                        buttonToAdd.Top = m_ColChoiceButtons[j].Top + m_ColChoiceButtons[j].Height + k_AdditionOfUnits;
                    }
                    else if (j == 0 && i != 0)
                    {
                        buttonToAdd.Left = m_BoardMatrixDidplay[i - 1, 0].Left;
                        buttonToAdd.Top = m_BoardMatrixDidplay[i - 1, 0].Top + m_BoardMatrixDidplay[i - 1, 0].Height + k_AdditionOfUnits;
                    }
                    else
                    {
                        buttonToAdd.Left = m_BoardMatrixDidplay[i - 1, j].Left;
                        buttonToAdd.Top = m_BoardMatrixDidplay[i - 1, j].Top + m_BoardMatrixDidplay[i - 1, j].Height + k_AdditionOfUnits;
                    }

                    buttonToAdd.Visible = true;
                    this.Controls.Add(buttonToAdd);
                    m_BoardMatrixDidplay[i, j] = buttonToAdd;
                }
            }

            m_FormNewSize.Height = m_BoardMatrixDidplay[m_GameLogic.RowsNumber - 1, 0].Bottom;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            BuildLineChoiceButtons();
            buildBoardMatrix();
            setScorePlayersLabel();
            this.ClientSize = m_FormNewSize;
        }

        private void buttonToCreate_Click(object sender, EventArgs e)
        {
            m_GameLogic.PlaceChoice(eBoardSign.X,int.Parse(((Button)sender).Text));
        }

    }
}
