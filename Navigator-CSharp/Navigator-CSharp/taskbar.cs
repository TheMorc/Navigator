using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static Navigator_CSharp.taskbar_brainz;

namespace Navigator_CSharp
{

    public partial class taskbar : Form
    {
        public HelperForm taskbarColor = new HelperForm { Opacity = 0.5, ShowInTaskbar = false, FormBorderStyle = FormBorderStyle.None, BackColor = Color.Black, TopMost = true };
        public taskbar()
        {
            InitializeComponent();
            taskbar_brainz.SetWindowPos(this.Handle, (IntPtr)0, 0, SystemInformation.VirtualScreen.Height - 32, SystemInformation.VirtualScreen.Width, 32, 0x0040);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        private void taskbar_Load(object sender, EventArgs e)
        {
            BackColor = Color.Gainsboro;
            taskbarColor.ContextMenuStrip = contextMenuStrip1;
            TransparencyKey = BackColor;
            Opacity = 1;
            taskbarColor.Show();
            Visible = false;
            Owner = taskbarColor;
            Visible = true;
            Move += (o, a) => taskbarColor.Bounds = Bounds;
            Resize += (o, a) => taskbarColor.Bounds = Bounds;
            taskbarColor.Bounds = Bounds;

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                taskbarColor.Opacity = Double.Parse(toolStripTextBox1.Text);
            }
            catch (Exception)
            {

                return;
            }
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                taskbarColor.BackColor = Color.FromName(toolStripTextBox2.Text);
            }
            catch (Exception)
            {

                return;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void taskbar_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            //just to not kill it by accident. maybe will be replaced with windows xp like shutdown dialog
        }
    }

    public class HelperForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }
    }

}
