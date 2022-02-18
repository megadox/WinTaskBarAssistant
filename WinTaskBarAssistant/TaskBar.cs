using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinTaskBarAssistant
{
    public partial class TaskBar : Form
    {
        Timer ttimer;

        public TaskBar()
        {
            InitializeComponent();

            
            this.Size = new Size(1, 1);
            this.TopMost = false;
            //this.TopLevel = false;

            this.Load += Form1_Load;

            taskbarAssistant1.ProgressMaximumValue = 100;
            taskbarAssistant1.ProgressMode = DevExpress.Utils.Taskbar.Core.TaskbarButtonProgressMode.Paused;

            ttimer = new Timer();
            ttimer.Interval = 40;
            ttimer.Tick += T_Tick;
            ttimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            this.WindowState = FormWindowState.Minimized;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            if (taskbarAssistant1.ProgressCurrentValue < 100)
                taskbarAssistant1.ProgressCurrentValue++;
            else
                taskbarAssistant1.ProgressCurrentValue = 0;
        }

        public void Stop()
        {
            ttimer.Stop();
            this.Close();            
            //Application.Exit();
        }
    }
}
