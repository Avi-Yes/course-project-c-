using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace C18_Ex05
{
    public partial class FormGameSettings : Form
    {
        private Player m_Player1;
        private Player m_Player2;
        private int m_BoardLength;
        private int m_BoardWidth;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
            if (checkBoxPlayer2.Checked)
            {
                textBoxPlayer2.Text = string.Empty;
            }
            else
            {
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer1.Text == string.Empty || textBoxPlayer2.Text == string.Empty)
            {
                MessageBox.Show("Please fill the players names!");
            }
            else
            {
                setPlayers();
                setBoardSize();
                this.Hide();
                FormGame formGame = new FormGame(new GameLogic(m_BoardLength, m_BoardWidth, m_Player1, m_Player2));
                formGame.ShowDialog();
                this.Dispose();
            }
            
            
        }

        private void setPlayers()
        {
             m_Player1 = new Player(ePlayerType.humen);
             m_Player1.Name = textBoxPlayer1.Text;
             m_Player1.BoardSign = eBoardSign.X;
             m_Player2 = new Player();
             m_Player2.BoardSign = eBoardSign.O;

             if (checkBoxPlayer2.Checked)
             {
                 m_Player2.Type = ePlayerType.humen;
                 m_Player2.Name = textBoxPlayer2.Text;
             }
             else
             {
                 m_Player2.Name = "Computer";
                 m_Player2.Type = ePlayerType.computer;
             }
        }
        private void setBoardSize()
        {
            m_BoardLength = (int)numericUpDownRows.Value;
            m_BoardWidth = (int)numericUpDownCols.Value;
        }
    }
}
