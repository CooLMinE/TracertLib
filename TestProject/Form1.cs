using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fluxbytes;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Net;

namespace TestProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            Tracert rt = new Tracert();
            foreach (var en in rt.Trace("google.com"))
                Console.WriteLine(en);
        }
    }
}
