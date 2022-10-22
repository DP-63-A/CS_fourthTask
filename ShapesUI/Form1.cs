using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapesLib;

namespace ShapesUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetDemoObject_Click(object sender, EventArgs e)
        {
            var obj = new DemoClass();
            textAllocator.Text = obj.ToString();
        }
    }
}
