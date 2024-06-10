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
using System.IO.Ports;

namespace Project
{
    static class Global
    {
        public static SerialPort sp;
        public static test[] x = new test[5];

        public static byte[] ConvertToHex(String hex)
        {
            byte[] result = Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
            return result;
        }
    }
}
