using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace 扫雷
{
    public partial class graphicsTest : Form
    {
        public graphicsTest()
        {
            InitializeComponent();
        }

        private void graphicsTest_Load(object sender, EventArgs e)
        {
            Graphics graphics_boom = this.CreateGraphics();
            Rectangle rect = new Rectangle(20, 20, 640, 410);
            Brush lBrush = new LinearGradientBrush(new Point(10, 10), new Point(100, 100), Color.Red, Color.Red);
            graphics_boom.FillRectangle(lBrush, rect); 
        }

        private void graphicsTest_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics_boom = this.CreateGraphics();
            Rectangle rect = new Rectangle(20, 20, 640, 410);
            Brush lBrush = new LinearGradientBrush(new Point(10, 10), new Point(100, 100), Color.Red, Color.Red);
            graphics_boom.FillRectangle(lBrush, rect); 
        }
    }
}
