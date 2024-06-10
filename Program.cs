using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Project
{
    [Serializable]
    class test
    {
        public byte[] charBuffer = null;
    }
    abstract class Human
    {
        public string Name { get; set; }
        public long NationalID { get; set; }
        abstract public void CreateList();
    }

    class Voter:Human
    {
        public bool hasVoted = false;
        public static List<Voter> v = new List<Voter>();
        public override void CreateList()
        {
            var path = "Voters.txt";
            List<string> x = File.ReadLines(path).ToList();  //List of lines in voters' file

            int k = 1;
            for (int i = 0; i < x.Count; i += 4)
            {
                if (x[i] == $"Voter {k}:")
                {
                    Voter person = new Voter();
                    person.Name= x[i + 1];
                    person.NationalID = Convert.ToInt64(x[i + 2]);
                    person.hasVoted = Convert.ToBoolean(x[i + 3]);
                    v.Add(person);
                    k++;
                }
            }
        }
    }

    class Candidate : Human
    {
        public int CountVotes;

        public static List<Candidate> c = new List<Candidate>();
        public override void CreateList()
        {
            var path = "Candidates.txt";
            List<string> x = File.ReadLines(path).ToList();  //List of lines in candidates' file

            int k = 1;
            for (int i = 0; i < x.Count; i += 4)
            {
                if (x[i] == $"Candidate {k}:")
                {
                    Candidate person = new Candidate();
                    person.Name = x[i + 1];
                    person.NationalID = Convert.ToInt64(x[i + 2]);
                    person.CountVotes = Convert.ToInt32(x[i + 3]);
                    c.Add(person);
                    k++;
                }
            }
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Voter x = new Voter();
            x.CreateList();
            Candidate y = new Candidate();
            y.CreateList();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
