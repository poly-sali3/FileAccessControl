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
    public partial class TesterForm : Form
    {
        public TesterForm()
        {
            InitializeComponent();
            // Здесь можно загружать и отображать данные, специфичные для тестировщика
            txtTesterInfo.Text = "Tester: Здесь должна быть информация, доступная только тестировщику.";
        }

        private void TesterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
