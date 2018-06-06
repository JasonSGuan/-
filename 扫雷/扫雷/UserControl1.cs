using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 扫雷
{
    public partial class UserControl1 : UserControl
    {
        public string text;
        public Size size;
        

        public UserControl1()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            button1.Text = text;
            this.Size = size;
        }
    }
}
