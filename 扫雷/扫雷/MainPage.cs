using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace 扫雷
{
    public partial class MainPage : Form
    {
        private UserControl1[][] UCs;       //自定义控件，实现左键单击和右键单击的事件；
        private int[][] boom;               //标记雷的位置及各个位置周围雷的个数；
        private int boom_count;             //标记游戏时雷的总数；
        private int row_num;                //标记行数
        private int col_num;                //标记列数
        private int boom_num;               //已标记的雷的个数；
        private int HC_num;                 //已扫清不是雷的数量；
        private string old_level;           //指示原游戏等级；
        private DateTime dtstart;           //记录开始时间
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPage_Load(object sender, EventArgs e)
        {
            //初始化行、列、雷的数量
            row_num = 9;
            col_num = 9;
            boom_count = 10;
            boom_num = 0;
            HC_num = 0;
            old_level = "初级";
            初级ToolStripMenuItem.Checked = true;

            Start_New_Game();

        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        private void Start_New_Game()
        {
            #region  创建自定义控件数组

            generate();

            #endregion


            #region  随机生成雷的位置

            boom_rowcol();

            #endregion


            #region  判断生成的雷的位置是否有重复

            is_Count_true();

            #endregion


            #region  设置其余位置应显示的数字（即给位置周围雷的个数）

            set_boom_num();

            #endregion


            #region  为控件数组设置各项属性，并显示在页面上

            set_Controls();

            #endregion


            #region  设置当前页面大小

            Set_Page_Size();

            #endregion
        }

        /// <summary>
        /// 设置页面大小
        /// </summary>
        private void Set_Page_Size()
        {
            #region  设置当前页面大小

            this.Size = new Size(20 * (col_num + 1) - 10, 20 * (row_num + 1) + 50 - 9 + 45);
            this.Focus();
            #endregion
        }

        /// <summary>
        /// 绘制图形
        /// </summary>
        private void Paint_Num(int num,object sender)
        {
            Panel p = (Panel)sender;
            Font font = new Font("楷体", 27);
            Graphics graphics_boom = p.CreateGraphics();
            graphics_boom.Clear(p.BackColor);
            Rectangle rect = new Rectangle(2, 4, 64, 44);  
            Brush lBrush = new LinearGradientBrush(rect, Color.Red, Color.Red, LinearGradientMode.BackwardDiagonal);
            graphics_boom.DrawString((num).ToString("D3"), font, lBrush, new PointF(0, 2));
        }

        /// <summary>
        /// 鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void use_MouseDown(object sender, MouseEventArgs e)
        {
            if (HC_num == 0)
            {
                dtstart = DateTime.Now.AddSeconds(-1);
                timer1.Enabled = true;
                timer1.Start();
            }
            Button use = (Button)sender;
            if (MouseButtons.Left == e.Button)
            {
                if (use.Text == "卍")
                {
                    return;
                }
                UserControl1 user = (UserControl1)use.Parent;
                int col = user.Location.X / 20;
                int row = user.Location.Y / 20;
                if (boom[row][col] == 9)
                {
                    timer1.Stop();
                    game_over(row, col);
                    MessageBox.Show("游戏结束");
                }
                else if (boom[row][col] == 0)
                {
                    Label lable = new Label();
                    lable.Text = "";
                    user.Controls.Add(lable);
                    lable.Location = new Point(3, 2);
                    use.Visible = false;
                    user.BorderStyle = BorderStyle.Fixed3D;
                    HC_num++;
                    if_boom_zero(row - 1, col);
                    if_boom_zero(row, col - 1);
                    if_boom_zero(row, col + 1);
                    if_boom_zero(row + 1, col); 
                    if_boom_zero(row - 1, col - 1);
                    if_boom_zero(row - 1, col + 1);
                    if_boom_zero(row + 1, col + 1);
                    if_boom_zero(row + 1, col - 1);

                    if (HC_num + boom_num == row_num * col_num || row_num * col_num - HC_num == boom_count)
                    {
                        timer1.Stop();
                        MessageBox.Show("恭喜你，找出所有雷，赢得游戏！");
                    }
                }
                else
                {
                    Label lable = new Label();
                    lable.Text = boom[row][col].ToString();
                    user.Controls.Add(lable);
                    lable.Location = new Point(3, 2);
                    use.Visible = false;
                    user.BorderStyle = BorderStyle.Fixed3D;
                    HC_num++;

                    if (HC_num + boom_num == row_num * col_num)
                    {
                        timer1.Stop();
                        MessageBox.Show("恭喜你，找出所有雷，赢得游戏！");
                    }
                }
            }
            else if (MouseButtons.Right == e.Button)
            {
                if (use.Text == "")
                {
                    use.Text = "卍";
                    boom_num++;

                    if (HC_num + boom_num == row_num * col_num || row_num * col_num - HC_num == boom_count)
                    {
                        timer1.Stop();
                        MessageBox.Show("恭喜你，找出所有雷，赢得游戏！");
                    }
                    
                    Paint_Num(boom_count - boom_num, this.panel3);
                }
                else if (use.Text == "卍")
                {
                    use.Text = "?";
                    boom_num--;
                    Paint_Num(boom_count - boom_num, this.panel3);
                }
                else if (use.Text == "?")
                {
                    use.Text = "";
                }
            }
        }

        /// <summary>
        /// 判断是否有重复的
        /// </summary>
        private void is_Count_true()
        {
            int count = 0;      //标记实际雷数；
            for (int i = 0; i < row_num; i++)
            {
                for (int j = 0; j < col_num; j++)
                {
                    if (boom[i][j] == 9)
                    {
                        count++;
                    }
                }
            }
            if (count != boom_count)
            {
                set_boom();
            }
        }

        /// <summary>
        /// 设置雷的位置（当有重复时）
        /// </summary>
        private void set_boom()
        {
            for (int i = 0; i < row_num; i++)
            {
                for (int j = 0; j < col_num; j++)
                {
                    if (boom[i][j] != 9)
                    {
                        boom[i][j] = 9;
                        is_Count_true();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 新开游戏初始化数组
        /// </summary>
        private void generate()
        {
            UCs = new UserControl1[row_num][];
            for (int i = 0; i < row_num; i++)
            {
                UCs[i] = new UserControl1[col_num];
            }

            boom = new int[row_num][];
            for (int i = 0; i < row_num; i++)
            {
                boom[i] = new int[col_num];
            }

            //初始化标记雷的数组
            for (int i = 0; i < row_num; i++)
            {
                for (int j = 0; j < col_num; j++)
                {
                    boom[i][j] = 0;
                }
            }
        }

        /// <summary>
        /// 生成雷的位置
        /// </summary>
        private void boom_rowcol()
        {
            //string filepath = System.Windows.Forms.Application.StartupPath + "\\log\\log" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
            //FileStream file = new FileStream(filepath, FileMode.Create);
            //StreamWriter sw = new StreamWriter(file);
            //生成随机数
            rowcol[] rc = new rowcol[boom_count];
            for (int i = 0; i < boom_count; i++)
            {
                int x = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                Random rdrow = new Random((i * x) * i);
                Random rdcol = new Random((i * x + i) * i);

                rc[i].row = rdrow.Next(row_num);
                rc[i].col = rdcol.Next(col_num);

                boom[rc[i].row][rc[i].col] = 9;

                //sw.WriteLine(rc[i].row + "," + rc[i].col);


            }
            ////清空缓冲区
            //sw.Flush();
            ////关闭流
            //sw.Close();
            //file.Close();
        }

        /// <summary>
        /// 设置控件属性
        /// </summary>
        private void set_Controls()
        {
            for (int i = 0; i < row_num; i++)
            {
                for (int j = 0; j < col_num; j++)
                {
                    UCs[i][j] = new UserControl1();
                    UCs[i][j].text = "";
                    UCs[i][j].size = new Size(20, 20);
                    UCs[i][j].Controls[0].MouseDown += new MouseEventHandler(use_MouseDown);
                    UCs[i][j].Location = new Point(20 * j, 20 * i);
                    panel1.Controls.Add(UCs[i][j]);
                }
            }
        }

        /// <summary>
        /// 设置其余位置应显示的数字（即各位置周围雷的个数）
        /// </summary>
        private void set_boom_num()
        {
            //string filepath = System.Windows.Forms.Application.StartupPath + "\\log\\log" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".txt";
            //FileStream file = new FileStream(filepath, FileMode.Create);
            //StreamWriter sw = new StreamWriter(file);
            for (int i = 0; i < row_num; i++)
            {
                for (int j = 0; j < col_num; j++)
                {
                    if (boom[i][j] != 9)
                    {
                        boom[i][j] = is_boom_num(i, j);
                    }
                    //sw.Write(boom[i][j] + ",");
                }
                //sw.WriteLine();
            }
            ////清空缓冲区
            //sw.Flush();
            ////关闭流
            //sw.Close();
            //file.Close();
        }

        /// <summary>
        /// 判断雷数
        /// </summary>
        private int is_boom_num(int i, int j)
        {
            int count = 0;
            if (i == 0)
            {
                if (j == 0)
                {
                    if (boom[i][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j + 1] == 9)
                    {
                        count++;
                    }
                }
                else if (j == col_num - 1)
                {
                    if (boom[i][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j - 1] == 9)
                    {
                        count++;
                    }
                }
                else
                {
                    if (boom[i][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j + 1] == 9)
                    {
                        count++;
                    }
                }
            }
            else if (i == row_num - 1)
            {
                if (j == 0)
                {
                    if (boom[i][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j + 1] == 9)
                    {
                        count++;
                    }
                }
                else if (j == col_num - 1)
                {
                    if (boom[i][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j - 1] == 9)
                    {
                        count++;
                    }
                }
                else
                {
                    if (boom[i][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j + 1] == 9)
                    {
                        count++;
                    }
                }
            }
            else
            {
                if (j == 0)
                {
                    if (boom[i - 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j + 1] == 9)
                    {
                        count++;
                    }
                }
                else if (j == col_num - 1)
                {
                    if (boom[i - 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j - 1] == 9)
                    {
                        count++;
                    }
                }
                else
                {
                    if (boom[i-1][j-1] == 9)
                    {
                        count++;
                    } 
                    if (boom[i - 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i - 1][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i][j + 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j - 1] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j] == 9)
                    {
                        count++;
                    }
                    if (boom[i + 1][j + 1] == 9)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// 判断雷数是否为0
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void if_boom_zero(int row, int col)
        {
            if (row >= 0 && col >= 0 && row <= row_num - 1 && col <= col_num - 1)
            {
                if (UCs[row][col].Controls[0].Visible)
                {
                    if (boom[row][col] == 0)
                    {
                        Label lable = new Label();
                        lable.Text = "";
                        UCs[row][col].Controls.Add(lable);
                        lable.Location = new Point(3, 2);
                        UCs[row][col].Controls[0].Visible = false;
                        UCs[row][col].BorderStyle = BorderStyle.Fixed3D;
                        HC_num++;
                        if_boom_zero(row - 1, col);
                        if_boom_zero(row, col - 1);
                        if_boom_zero(row, col + 1);
                        if_boom_zero(row + 1, col);
                        if_boom_zero(row - 1, col - 1);
                        if_boom_zero(row - 1, col + 1);
                        if_boom_zero(row + 1, col + 1);
                        if_boom_zero(row + 1, col - 1);
                    }
                    else
                    {
                        Label lable = new Label();
                        lable.Text = boom[row][col].ToString();
                        UCs[row][col].Controls.Add(lable);
                        lable.Location = new Point(3, 2);
                        UCs[row][col].Controls[0].Visible = false;
                        UCs[row][col].BorderStyle = BorderStyle.Fixed3D;
                        HC_num++;
                    }
                }
            }
        }

        /// <summary>
        /// 点到雷，游戏结束
        /// </summary>
        private void game_over(int row, int col)
        {
            UCs[row][col].Controls[0].BackColor = Color.Red;
            for (int i = 0; i < row_num; i++)
            {
                for (int j = 0; j < col_num; j++)
                {
                    if (boom[i][j] == 9)
                    {
                        if (UCs[i][j].Controls[0].Text != "卍")
                        {
                            UCs[i][j].Controls[0].Text = "雷";
                        }
                    }
                    else
                    {
                        if (UCs[i][j].Controls[0].Text == "卍")
                        {
                            UCs[i][j].Controls[0].Text = "X";
                            UCs[i][j].Controls[0].ForeColor = Color.Red;
                        }
                    }
                    UCs[i][j].Controls[0].Enabled = false;
                }
            }
        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boom_num = 0;
            HC_num = 0;
            panel1.Controls.Clear();
            Start_New_Game();
        }

        /// <summary>
        /// 初级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 初级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (old_level != "初级")
            {
                //初始化行、列、雷的数量
                row_num = 9;
                col_num = 9;
                boom_count = 10;
                boom_num = 0;
                HC_num = 0;
                panel1.Controls.Clear();
                Start_New_Game();
                中级ToolStripMenuItem.Checked = false;
                高级ToolStripMenuItem.Checked = false;
                自定义ToolStripMenuItem.Checked = false;
                old_level = "初级";
            }
        }

        /// <summary>
        /// 中级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 中级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (old_level != "中级")
            {
                //初始化行、列、雷的数量
                row_num = 16;
                col_num = 16;
                boom_count = 40;
                boom_num = 0;
                HC_num = 0;
                panel1.Controls.Clear();
                Start_New_Game();
                初级ToolStripMenuItem.Checked = false;
                高级ToolStripMenuItem.Checked = false;
                自定义ToolStripMenuItem.Checked = false;
                old_level = "中级";
            }
        }

        /// <summary>
        /// 高级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 高级ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (old_level != "高级")
            {
                //初始化行、列、雷的数量
                row_num = 16;
                col_num = 30;
                boom_count = 99;
                boom_num = 0;
                HC_num = 0;
                panel1.Controls.Clear();
                Start_New_Game();
                中级ToolStripMenuItem.Checked = false;
                初级ToolStripMenuItem.Checked = false;
                自定义ToolStripMenuItem.Checked = false;
                old_level = "高级";
            }
        }

        /// <summary>
        /// 自定义
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (old_level != "自定义")
            {
                中级ToolStripMenuItem.Checked = false;
                高级ToolStripMenuItem.Checked = false;
                初级ToolStripMenuItem.Checked = false;
            }
            MessageSet MS_row_col_boom = new MessageSet();
            MS_row_col_boom.old = old_level;
            MS_row_col_boom.row = row_num;
            MS_row_col_boom.col = col_num;
            MS_row_col_boom.boom = boom_count;
            MS_row_col_boom.ShowDialog();
            int row = MS_row_col_boom.row;
            int col = MS_row_col_boom.col;
            int boom = MS_row_col_boom.boom;
            if (row != row_num || col != col_num || boom != boom_count || boom_num != 0 || HC_num != 0)
            {
                //初始化行、列、雷的数量
                row_num = row;
                col_num = col;
                boom_count = boom;
                boom_num = 0;
                HC_num = 0;
                panel1.Controls.Clear();
                Start_New_Game();
            }
            old_level = "自定义";
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Paint_Num(boom_count - boom_num, sender);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Paint_Num(0, sender);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Paint_Num((DateTime.Now - dtstart).Seconds, this.panel4);
        }
    }

    public struct rowcol
    {
        public int row;
        public int col;
    }
}
