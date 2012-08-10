using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetroGraphApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();

            //打开地铁线路图
            metroGraphView1.OpenFromFile(Application.StartupPath + "\\MetroGraph.xml");
        }
    }
}