using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAccessControl
{
    public partial class HRManagerForm : Form
    {
        public HRManagerForm()
        {
            InitializeComponent();
            // Здесь можно загружать и отображать данные, специфичные для HR-менеджера
            txtHRManagerInfo.Text = "HR Manager: Здесь должна быть информация, доступная только HR-менеджеру.";
        }
        

        private void HRManagerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
