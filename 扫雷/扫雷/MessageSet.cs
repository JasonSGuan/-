using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 扫雷
{
    public partial class MessageSet : Form
    {
        public int row;
        public int col;
        public int boom;
        public string old;

        public MessageSet()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            row = Convert.ToInt16(txtRow_Num.Text);
            col = Convert.ToInt16(txtCol_Num.Text);
            boom = Convert.ToInt16(txtBoom_Num.Text);
            if (row > 30 || col > 30 || boom > 600 || boom > row * col)
            {
                MessageBox.Show("行数和列数不能大于30，雷数不能大于600,且雷数不能方格数！");
                return;
            }
            this.Close();
        }

        /// <summary>
        /// 取消自定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageSet_Load(object sender, EventArgs e)
        {
            txtBoom_Num.Text = boom.ToString();
            txtCol_Num.Text = col.ToString();
            txtRow_Num.Text = row.ToString();
        }

        /// <summary>
        /// 设置只能输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRow_Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtCol_Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void txtBoom_Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
