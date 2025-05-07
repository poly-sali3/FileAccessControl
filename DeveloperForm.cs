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
    public partial class DeveloperForm : Form
    {
        public DeveloperForm()
        {
            InitializeComponent();
            // Здесь можно загружать и отображать данные, специфичные для разработчика
            txtDeveloperInfo.Text = "Developer: Здесь должна быть информация, доступная только разработчику.";
        }

        private void DeveloperForm_Load(object sender, EventArgs e)
        {

        }
    }
}
