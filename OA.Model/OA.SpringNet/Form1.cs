using OA.SpringNet;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OA.SpringNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IApplicationContext ctx = ContextRegistry.GetContext(); //创建容器.
            IUserinfoService lister = (IUserinfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(lister.ShowMessage());
        }
    }
}
