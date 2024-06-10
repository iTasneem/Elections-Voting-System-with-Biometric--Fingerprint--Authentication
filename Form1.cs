using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.IO;
using System.IO.Ports;

namespace Project
{
    public partial class Form1 : Form
    {
        SerialPort sp = Global.sp;
        byte[] getImage = Global.ConvertToHex("EF01FFFFFFFF010003010005");
        byte[] storeInBuffer2 = Global.ConvertToHex("EF01FFFFFFFF01000402020009");
        byte[] sendToSensorBuffer1 = Global.ConvertToHex("EF01FFFFFFFF0100040901000F");
        byte[] match = Global.ConvertToHex("EF01FFFFFFFF010003030007");
        byte[] storeInBuffer1 = Global.ConvertToHex("EF01FFFFFFFF01000402010008");
        byte[] uploadFromCharBuffer1 = Global.ConvertToHex("EF01FFFFFFFF0100040801000E");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.ShowDialog();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult m = MessageBox.Show("Are you sure you want to close the program?","Close Program",MessageBoxButtons.YesNo);
            if(m == DialogResult.No)
            {
                e.Cancel = true;
            }

            else //File Saving Data
            {
                int k = 0;
                string path = "Candidates.txt";
                List<string> x = File.ReadLines(path).ToList();
                for (int i=3;i<x.Count;i+=4)
                {
                    x[i] = (Candidate.c[k].CountVotes).ToString();
                    k++;
                }
                File.WriteAllLines(path, x);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            sp = new SerialPort(comPortTextBox.Text, 115200);
            try
            {
                sp.Open();
            }
            catch
            {
                MessageBox.Show("Connection Failed!");
            }
            if (!sp.IsOpen)
                MessageBox.Show("Input the correct COM port number!");
            else
            {
                MessageBox.Show("Connection OK!");
                Global.sp = sp;
                comPortTextBox.Visible = false;
                button2.Visible = false;
                button3.Visible = true;
            }
               
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                Global.x = (test[])bf.Deserialize(fs);
                fs.Close();
                button3.Visible = false;
                button2.Visible = false;
                button1.Visible = true;
                comPortTextBox.Visible = false;
            }
        }

        
    }
}
