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

    public partial class Form2 : Form
    {
        SerialPort sp = Global.sp;   
        byte[] getImage = Global.ConvertToHex("EF01FFFFFFFF010003010005");
        byte[] storeInBuffer2 = Global.ConvertToHex("EF01FFFFFFFF01000402020009");
        byte[] sendToSensorBuffer1 = Global.ConvertToHex("EF01FFFFFFFF0100040901000F");
        byte[] match = Global.ConvertToHex("EF01FFFFFFFF010003030007");
        byte[] storeInBuffer1 = Global.ConvertToHex("EF01FFFFFFFF01000402010008");//temp
        byte[] uploadFromCharBuffer1 = Global.ConvertToHex("EF01FFFFFFFF0100040801000E");//temp

        test[] x = new test[5];//5 is the number of voters + 1 organizer (having index 0)
        int index = 0;
        
        public Form2()
        {
            InitializeComponent();
        }
        private void testFingerPrint(byte[] charBuffer,int org_test) //dosen't return something but if true the form will run .. org_test for the if cond. and to get the i to put it in has_voted
        {

            if (charBuffer == null)  //no reference (first sample)
                return ;
            //MessageBox.Show ( "Acquiring fingerprint for testing...");
            sp.Write(getImage, 0, getImage.Length);
            Thread.Sleep(1000);
            byte[] result = new byte[sp.BytesToRead];
            sp.Read(result, 0, sp.BytesToRead);

            if (result[9] == 0)
            {
                MessageBox.Show ("Fingerprint Read Successfully...");

                sp.Write(storeInBuffer2, 0, storeInBuffer2.Length);
                Thread.Sleep(1000);
                result = new byte[sp.BytesToRead];
                sp.Read(result, 0, sp.BytesToRead);
                if (result[9] == 0)
                {
                    //Try to resend the packet back from CharBuffer1 [stored in a file]
                    sp.Write(sendToSensorBuffer1, 0, sendToSensorBuffer1.Length);
                    Thread.Sleep(1000);
                    result = new byte[sp.BytesToRead];
                    sp.Read(result, 0, sp.BytesToRead);

                    //12 to 150
                    sp.Write(charBuffer, 12, charBuffer.Length - 12);
                    Thread.Sleep(1000);

                    //Try to Match
                    sp.Write(match, 0, match.Length);
                    Thread.Sleep(1000);
                    result = new byte[sp.BytesToRead];
                    sp.Read(result, 0, sp.BytesToRead);

                    if (result[9] == 0)
                    {
                        MessageBox.Show("Fingerprints Match !");
                        if (org_test == -1)
                        {
                            Form4 frm4 = new Form4();
                            frm4.ShowDialog();
                        }
                        else 
                        {
                            this.Close();
                            Voter.v[org_test].hasVoted = true;
                            Form3 frm3 = new Form3();
                            frm3.ShowDialog();
                        }
                    }
                    else
                        MessageBox.Show("Fingerprints Mismatch!");
                }
                else
                {
                    MessageBox.Show ("Fingerprint cannot be registered in sensor!");
                }
            }
            else
                MessageBox.Show ("Fingerprint Error! Make sure your finger is placed on the sensor");
        }
        private void getImageButton()///only for 1 use
        {
            while (index < x.Length)
            {
                x[index] = new test();
                sp.Write(getImage, 0, getImage.Length);

                Thread.Sleep(1000);

                byte[] result = new byte[sp.BytesToRead];
                sp.Read(result, 0, sp.BytesToRead);

                if (result[9] == 0)
                {
                    sp.Write(storeInBuffer1, 0, storeInBuffer1.Length);
                    Thread.Sleep(1000);
                    result = new byte[sp.BytesToRead];
                    sp.Read(result, 0, sp.BytesToRead);
                    if (result[9] == 0)
                    {
                        //Send the upload request
                        sp.Write(uploadFromCharBuffer1, 0, uploadFromCharBuffer1.Length);
                        Thread.Sleep(1000);
                        x[index].charBuffer = new byte[sp.BytesToRead];
                        sp.Read(x[index].charBuffer, 0, sp.BytesToRead);
                        index++;
                    }
                }
            }

            //Save the fingerprint to a file
            SaveFileDialog svd = new SaveFileDialog();
            if (svd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(svd.FileName, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, x);
                fs.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //getImageButton(); //ana dh 3amlo hena bs ay kalam 34an ma3mel4 button tany ba7ot kolo fe /**/ we 2a4a8al el satr dh bs
            int findex = 0;
            string ID = textBox1.Text;
            if (Convert.ToInt64(ID) == 123)
            {
                MessageBox.Show("Hello, Organizer! Please verify your fingerprint!","Verification");

                testFingerPrint(Global.x[0].charBuffer, -1);//the first one in the x list is the organizer (index 0)

            }

            else
            {
                for (int i = 0; i < Voter.v.Count; i++)
                {
                    if ((Voter.v[i].NationalID == Convert.ToInt64(ID)) && !Voter.v[i].hasVoted)
                    {

                        findex = 1;
                        DialogResult messOk = MessageBox.Show($"Hello, {Voter.v[i].Name}! Please verify your fingerprint!", "Verification", MessageBoxButtons.OK);//Authenticity Verified!", "Verification Successful", MessageBoxButtons.OK);
                        if (messOk == DialogResult.OK)
                        {
                            testFingerPrint(Global.x[i + 1].charBuffer, i);
                            break;
                        }
                    }

                    else if ((Voter.v[i].NationalID == Convert.ToInt64(ID)) && Voter.v[i].hasVoted)
                    {
                        findex = 1;
                        DialogResult messOk = MessageBox.Show("YOU HAVE VOTED BEFORE!", "Voted Before", MessageBoxButtons.OK);
                        this.Close();
                    }
                }

                if (findex == 0)
                {
                    DialogResult messError = MessageBox.Show("Could not verify voter info! Try Again...", "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (messError == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
        }

       
    }
}
