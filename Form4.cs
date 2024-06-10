using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Reading File Data
            string res = null;
            for(int i=0;i<Candidate.c.Count;i++)
            {
                res += $"{Candidate.c[i].Name}: {Candidate.c[i].CountVotes}\n";
            }
            MessageBox.Show(res, "Results");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int resFlag = 0, maxIndex = 0;
            string winner = Candidate.c[0].Name;

            int max = Candidate.c[0].CountVotes;

            for (int i = 1; i < Candidate.c.Count; i++) //Finding max number of votes
            {
                if (Candidate.c[i].CountVotes > max)
                {
                    winner = Candidate.c[i].Name;
                    max = Candidate.c[i].CountVotes;
                    maxIndex = i;
                }

            }

            for (int i = 1; i < Candidate.c.Count; i++)
            {
                if (Candidate.c[i].CountVotes == max && i != maxIndex)
                {
                    resFlag = 1;
                }
            }

            if (resFlag == 0)
                MessageBox.Show($"The winner is: {winner}", "Winner Announcement");
            else
                MessageBox.Show("There is a tie between more than one candidate!");

            Application.Exit();
        }
    }
}
