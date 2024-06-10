using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Project
{

    public partial class Form3 : Form
    {
        List<RadioButton> listRadio = new List<RadioButton>();
        string n;
        public Form3()
        {
            InitializeComponent();
            for(int i=0;i<Candidate.c.Count;i++)
            {
                RadioButton r = new RadioButton() { Text = Candidate.c[i].Name, AutoSize = true };
                flowLayoutPanel1.Controls.Add(r);
                flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
                listRadio.Add(r);
            }
           
            for(int i=0;i<Candidate.c.Count;i++)
            {
                listRadio[i].Click += (sender, e) =>
                {
                    button1.Visible = true;
                };
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult confirmation = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                int k = 0;
                string path = "Voters.txt";
                List<string> x = File.ReadLines(path).ToList();
                for (int i = 3; i < x.Count; i += 4)
                {
                    x[i] = (Voter.v[k].hasVoted).ToString();
                    k++;
                }

                File.WriteAllLines(path, x);

                foreach (var it in listRadio)
                {
                    if(it.Checked == true)
                    {
                        n = it.Text;
                        break;
                    }
                }
                for (int i = 0;i<Candidate.c.Count;i++)
                {
                    if(Candidate.c[i].Name == n)
                    {
                        Candidate.c[i].CountVotes++;
                        MessageBox.Show("Vote Submitted Successfully!");
                        break;
                    }
                }
                this.Close();
            }
        }

    
    }
}
