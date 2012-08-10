using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1.ServiceReference1;
using WindowsFormsApplication1.ServiceReference2;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var sc = new Service1Client();
            var msg = sc.GetData(10);
            MessageBox.Show(msg);
            sc.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sc = new Service1Client();
            sc.TestMethod("abc");
            MessageBox.Show("没有返回值！");
            sc.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.ServiceModel.InstanceContext instanceContext = new System.ServiceModel.InstanceContext(new CalculatorCallback());
            var sc = new Service2Client(instanceContext);
            sc.DoWork();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] s_ids = new string[] { "1","2" };
            int[] i_ids = Array.ConvertAll<string, int>(s_ids,int.Parse);
            MessageBox.Show(i_ids.Length+"");
        }

    }

    public class CalculatorCallback : IService2Callback
    {

        #region IService2Callback 成员

        void IService2Callback.CallBackTestMethod()
        {
            MessageBox.Show("222");
        }

        #endregion
    }

}
